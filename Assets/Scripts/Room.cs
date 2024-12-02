using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    GameObject  playerStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetPlayerStartPosition()
    {
        if (playerStart == null)
            return transform.position;

        return playerStart.transform.position;
    }
}
