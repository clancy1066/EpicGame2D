using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DIRECTOR_STATE
{
    BEGIN_PLAY
,   EXECUTE
, END_PLAY
}

public class Director : MonoBehaviour
{
    static Director instance;

    [SerializeField]
    Player player;

    SpriteAnim[] npcs;

    Canvas uiCanvas;

    [SerializeField]
    Canvas wsCanvas;

    [SerializeField]
    TextMeshProUGUI levelTimerText;

    [SerializeField]
    TextMeshProUGUI floatingTextTemplate;


    [SerializeField]
    float floatingTextFadeTime = 1.0f;

    [SerializeField]
    float floatingTextSpeed = 1.0f;

    [SerializeField]
    Room currentRoom;

    DIRECTOR_STATE state = DIRECTOR_STATE.BEGIN_PLAY;
    bool stateChanged;

    float completionTime;

    StateMachineLite<DIRECTOR_STATE> sml;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if (floatingTextTemplate != null)
            floatingTextTemplate.gameObject.SetActive(false);

        sml = new StateMachineLite<DIRECTOR_STATE>();

        if (currentRoom != null)
        {
            npcs = currentRoom.GetComponentsInChildren<SpriteAnim>();

            completionTime = currentRoom.GetCompletionTime();
            wsCanvas = currentRoom.GetCanvas();
        }
        UpdateTime();

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case DIRECTOR_STATE.BEGIN_PLAY: UpdateBEGIN_PLAY(); break;
            case DIRECTOR_STATE.EXECUTE: UpdateEXECUTE(); break;
            case DIRECTOR_STATE.END_PLAY: UpdateEND_PLAY(); break;

        }
    }

    void UpdateTime()
    {
        if (levelTimerText != null)
        {
            int hours = Mathf.FloorToInt(completionTime / 3600); // Get total hours
            int minutes = Mathf.FloorToInt((completionTime % 3600) / 60); // Get remaining minutes
            int seconds = Mathf.FloorToInt(completionTime % 60); // Get remaining seconds

            string timeString = null;

            if (hours > 0)
                timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
            else
                timeString = string.Format("{0:D2}:{1:D2}", minutes, seconds);

            levelTimerText.text = timeString;
        }
    }


    // ****************************
    // State Machine implementation
    // ****************************
    void UpdateBEGIN_PLAY()
    {
        if (player != null)
            player.transform.position = currentRoom.GetPlayerStartPosition();

        sml.ChangeState(ref state, DIRECTOR_STATE.EXECUTE);
    }
    void UpdateEXECUTE()
    {
        completionTime -= Time.deltaTime;

        UpdateTime();

        if (player != null)
            player.Execute();

        if (npcs != null)
        {
            foreach (var npc in npcs)
                npc.Execute();
        }
    }

    void UpdateEND_PLAY()
    {

    }

    // ************************************
    // statics for external use    
    // ************************************
    static public void DropPickup(Vector2 AtPos,Pickup pickup)
    {
        if (pickup != null)
        {
            Pickup toDrop = Instantiate(pickup) as Pickup;

            toDrop.transform.position = AtPos;
            toDrop.gameObject.SetActive(true);
            
            toDrop.transform.SetParent(instance.currentRoom.transform);
            toDrop.transform.localScale = Vector3.one;
        }
    }

    static public void ShowFloatingText(Vector2 pos,string textToFloat)
    {
        TextMeshProUGUI newText = Instantiate(instance.floatingTextTemplate);

        newText.transform.position = pos;
        newText.gameObject.SetActive(true);
        newText.text = textToFloat;

        newText.transform.SetParent(instance.wsCanvas.transform);

        instance.StartCoroutine(instance.FloatAndFadeText(newText));
    }

    public IEnumerator FloatAndFadeText(TextMeshProUGUI textMeshPro)
    {
        Vector3 startPosition = textMeshPro.transform.position;
        Color originalColor = textMeshPro.color;

        float timer = 0f;

        while (timer < 2)
        {
            // Move the text upwards
            textMeshPro.transform.position = startPosition + Vector3.up * instance.floatingTextSpeed * timer;

            // Fade the text out
            float alpha = Mathf.Lerp(1f, 0f, timer / instance.floatingTextFadeTime);
            textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the text is fully transparent at the end
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Optionally destroy the GameObject after fading out
        Destroy(textMeshPro.gameObject);
    }
}
