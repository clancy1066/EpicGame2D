using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : SpriteAnim
{
    Animator animator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//
        animator.SetBool("open", true);
    }
}
