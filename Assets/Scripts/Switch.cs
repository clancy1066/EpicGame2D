using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : SpriteAnim
{
    Animator animator;

    [SerializeField]
    Pickup pickupToDrop;
    [SerializeField]
    int numToDrop = 1;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//
        animator.SetBool("open", true);

        JukeBox.PlayClip(AUDIO_LOOKUP.CHEST_OPEN);

        if (pickupToDrop != null)
        {
            for (int i = 0; i < numToDrop; i++) 
                Director.DropPickup(transform.position, pickupToDrop);
        }
    }
}
