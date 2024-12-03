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
    static Pickup instance;

    CircleCollider2D[]  circleColliders;
    Rigidbody2D         theRB;

    public PICKUP_STATE initialState;
    
    PICKUP_STATE state;

    StateMachineLite<PICKUP_STATE> sml;

    [SerializeField]
    int value = 1;

    [SerializeField]
    float  presentPushForce= 100;

    [SerializeField]
    float presentPeriod = 1;

    private PICKUP_STATE stateMachine;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        instance = this;

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

    public void Present()
    {
        sml.ChangeState(ref state, PICKUP_STATE.PRESENTING);
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
    {
        if (sml.DidStateChange())
        {
            theRB.WakeUp();

            theRB.AddForceY(presentPushForce);

            stateTimer = presentPeriod;

            return;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer<0)
            sml.ChangeState(ref state, PICKUP_STATE.AWAITING_PICKUP);
    }

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

        string textToShow = "+" + value.ToString(); 

        Director.ShowFloatingText(transform.position, textToShow);

        Destroy(gameObject);
    }
}
