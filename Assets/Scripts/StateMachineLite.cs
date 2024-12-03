using System;

public class StateMachineLite<TState> where TState : Enum
{
    private TState currentState;
    private bool stateChanged = false;

    public TState CurrentState => currentState;

    public void ChangeState(ref TState curState, TState newState)
    {
        curState = newState;
        stateChanged = true;
    }

    public bool DidStateChange()
    {
        bool retVal = stateChanged;
        stateChanged = false;
        return retVal;
    }
}
