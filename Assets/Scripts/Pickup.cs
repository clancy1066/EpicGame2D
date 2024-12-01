using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("open", true);
    }
}
