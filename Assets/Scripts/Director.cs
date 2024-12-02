using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Director : MonoBehaviour
{
    [SerializeField]
    Player player;

    SpriteAnim[]  npcs;

    [SerializeField]
    Canvas          uiCanvas;
   
    [SerializeField]
    Room currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        if (currentRoom!=null)
            npcs = currentRoom.GetComponents<SpriteAnim>();

        if (uiCanvas != null)
        {
            Transform tmp = uiCanvas.gameObject.transform.Find("timerText");

            if (tmp != null)
            {
                TMPro.TextMeshPro theTimer = tmp.gameObject.GetComponent<TMPro.TextMeshPro>();

                theTimer.text = "42";
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            player.Execute();

        if (npcs != null)
        {
            foreach (var npc in npcs)
                npc.Execute();
        }
        
    }
}
