using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PICKUP_STATE
{
    DORMANT
,   PRESENTING
,   AWAITING_PICKUP
};



public class Pickup : SpriteAnim
{ 
    CircleCollider2D[]  circleColliders;
    Rigidbody2D         theRB;

    public PICKUP_STATE initialState;
    
    PICKUP_STATE state;

    StateMachineLite<PICKUP_STATE> sml;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        sml = new StateMachineLite<PICKUP_STATE>();

        sml.ChangeState(ref state, initialState);

        circleColliders = GetComponentsInChildren<CircleCollider2D>();

        theRB = GetComponent<Rigidbody2D>();

        ActivateCollision(false);
    }


    void ActivateCollision(bool onOrOff)
    {
        for (int i=0;i<circleColliders.Length;++i)
            circleColliders[i].enabled = onOrOff;

        if (theRB != null)
        {
            if (onOrOff)
                theRB.WakeUp();
            else
                theRB.Sleep();
        }
    }
    // Update is called once per frame
     public override void Execute()
    {
        switch (state)
        {
            case PICKUP_STATE.DORMANT:          break;
            case PICKUP_STATE.PRESENTING:       UpdatePRESENTING();         break;
            case PICKUP_STATE.AWAITING_PICKUP:  UpdateAWAITING_PICKUP();    break;

        }
    }

    void UpdatePRESENTING()
    {}

    void UpdateAWAITING_PICKUP()
    {
        if (sml.DidStateChange())
        {
            ActivateCollision(true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Pickup  Collision2D");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pickup TriggerEnter2D");

        JukeBox.PlayClip(AUDIO_LOOKUP.PICKUP_SKULL);

        Director.ShowFloatingText(transform.position, "+10");

        Destroy(gameObject);
    }
}
