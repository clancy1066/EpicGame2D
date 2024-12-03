using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DIRECTOR_STATE
{
    BEGIN_PLAY
,   EXECUTE
,   END_PLAY
}

public class Director : MonoBehaviour
{
    [SerializeField]
    Player player;

    SpriteAnim[] npcs;

    [SerializeField]
    Canvas uiCanvas;

    [SerializeField]
    TextMeshProUGUI levelTimerText;

    [SerializeField]
    Room currentRoom;

    DIRECTOR_STATE state = DIRECTOR_STATE.BEGIN_PLAY;
    bool stateChanged;

    StateMachineLite<DIRECTOR_STATE> sml;

    // Start is called before the first frame update
    void Start()
    {
        sml = new StateMachineLite<DIRECTOR_STATE>();

        if (currentRoom != null)
            npcs = currentRoom.GetComponentsInChildren<SpriteAnim>();

        if (levelTimerText != null)
            levelTimerText.text = "42";

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case DIRECTOR_STATE.BEGIN_PLAY: UpdateBEGIN_PLAY(); break;
            case DIRECTOR_STATE.EXECUTE:    UpdateEXECUTE(); break;
            case DIRECTOR_STATE.END_PLAY:   UpdateEND_PLAY(); break;

        }
       
    }

    // ****************************
    // State Machine implementation
    // ****************************
    void UpdateBEGIN_PLAY()
    {
        if (player != null)
            player.transform.position = currentRoom.GetPlayerStartPosition();

        sml.ChangeState(ref state,DIRECTOR_STATE.EXECUTE);
    }
    void UpdateEXECUTE()
    {
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
}
