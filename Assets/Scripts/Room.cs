using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    GameObject  playerStart;


    [SerializeField]
    float completionTime = 120.0f;

    public Vector2 GetPlayerStartPosition()
    {
        if (playerStart == null)
            return transform.position;

        return playerStart.transform.position;
    }

    public float GetCompletionTime()
    {
       

        return completionTime;
    }
}
