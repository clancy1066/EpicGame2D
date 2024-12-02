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

    // Start is called before the first frame update
    void Start()
    {
        if (currentRoom != null)
            npcs = currentRoom.GetComponents<SpriteAnim>();

        if (levelTimerText != null)
            levelTimerText.text = "42";

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


    // ****************************
    // State Machine helpers
    // ****************************
    void ChangeState(DIRECTOR_STATE newstate)
    {
        state = newstate;
        stateChanged = true;
    }

    bool DidStateChange()
    {
        bool retVal = stateChanged;

        stateChanged = false;
        return retVal;
    }

    // ****************************
    // State Machine implementation
    // ****************************
    void UpdateBEGIN_PLAY()
    {
        if (player != null)
            player.transform.position = currentRoom.GetPlayerStartPosition();

        ChangeState(DIRECTOR_STATE.EXECUTE);
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
