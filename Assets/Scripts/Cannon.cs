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

    bool        stateChanged;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Mkae sure to call this. It verifies the textures.
        base.Start();       
    }

    // Update is called once per frame
    void Execute()
    {
        // TODO
        switch (state)
        {
            case CANNON_STATE.IDLE:     ExecuteIDLE();   break;
            case CANNON_STATE.SHOOT:    ExecuteSHOOT (); break;
            case CANNON_STATE.SMOKE1:   ExecuteSMOKE1(); break;
            case CANNON_STATE.SMOKE2:   ExecuteSMOKE2(); break;
        }
    }

    // ***************************
    // State machine operators
    // ***************************
    void ChangeState(CANNON_STATE newstate)
    {
        state = newstate;
        stateChanged = true;
    }

    bool DidStateChange()
    {
        bool retVal = stateChanged;

        stateChanged = false;
        return retVal; 
    }

    // ***************************
    // State machine implementation
    // ***************************
    private void ExecuteIDLE()
    {
        if (DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            ChangeState(CANNON_STATE.SHOOT);
    }
    private void ExecuteSHOOT ()
    {
        if (DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            ChangeState(CANNON_STATE.SMOKE1);
    }

    private void ExecuteSMOKE1()
    {
        if (DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }
        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            ChangeState(CANNON_STATE.SMOKE2);
    }
    private void ExecuteSMOKE2()
    {
        if (DidStateChange())
        {
            ChangeFrame((int)state);
            stateTimer = frameHoldTime;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            ChangeState(CANNON_STATE.IDLE);
    }

}
