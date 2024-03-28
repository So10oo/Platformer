using System;
using UnityEngine;

public class StateMachineEvents<TypeCharacter> : StateMachine<TypeCharacter>
{
    public event Func<State<TypeCharacter>, State<TypeCharacter>, bool?> WhenAttemptingChangeState;
    public event Action<State<TypeCharacter>, State<TypeCharacter>> OnChangeState;

    public override void ChangeState(State<TypeCharacter> newState)
    {
        var _return = WhenAttemptingChangeState?.Invoke(CurrentState, newState) ?? false;
        if (_return || newState is null)
            return;

        base.ChangeState(newState);

        OnChangeState?.Invoke(PreviousState, CurrentState);

        Debug.Log(PreviousState + "->" + CurrentState);
        TextGlobal.info.text = CurrentState.ToString();
    }

 
}

