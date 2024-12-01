using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField]
    Player player;

    SpriteAnim[]  npcs;  

    // Start is called before the first frame update
    void Start()
    {
        npcs = GetComponents<SpriteAnim>();   
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
