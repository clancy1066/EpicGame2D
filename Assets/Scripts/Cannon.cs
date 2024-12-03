using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CANNON_STATE
{ 
    IDLE    = 0
,   SHOOT   
,   SMOKE1
,   SMOKE2
}




public class Cannon : SpriteAnim
{
    CANNON_STATE state = CANNON_STATE.IDLE;

    bool stateChanged;

    StateMachineLite<CANNON_STATE> sml;



    // Start is called before the first frame update
    protected override void Start()
    {
        // Mkae sure to call this. It verifies the textures.
        base.Start();

        sml = new StateMachineLite<CANNON_STATE>();

        sml.ChangeState(ref state, CANNON_STATE.IDLE);
    }

    // Update is called once per frame
    override public void Execute()
    {
        // TODO
        switch (state)
        {
            // TODO 2
            case CANNON_STATE.IDLE: ExecuteIDLE(); break;
            case CANNON_STATE.SHOOT: ExecuteSHOOT(); break;
            case CANNON_STATE.SMOKE1: ExecuteSMOKE1(); break;
            case CANNON_STATE.SMOKE2: ExecuteSMOKE2(); break;
        }
    }

    // ***************************
    // State machine implementation
    // ***************************
    private void ExecuteIDLE()
    {
        if (sml.DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            sml.ChangeState(ref state, CANNON_STATE.SHOOT);
    }
    private void ExecuteSHOOT()
    {
        if (sml.DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            sml.ChangeState(ref state, CANNON_STATE.SMOKE1);
    }

    private void ExecuteSMOKE1()
    {
        if (sml.DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }
        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            sml.ChangeState(ref state, CANNON_STATE.SMOKE2);
    }
    private void ExecuteSMOKE2()
    {
        if (sml.DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            sml.ChangeState(ref state, CANNON_STATE.IDLE);

    }
}
