using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject targetObj;
    public float distance;
   

    // Update is called once per frame
    void Update()
    {
        if (targetObj != null)
        {
            Vector3 targetPos = targetObj.transform.position;

            targetPos.z = distance;

            transform.position = targetPos;// + Vector3.up * distance;

            //transform.LookAt(targetPos,Vector3.up);
        }
    }
}
