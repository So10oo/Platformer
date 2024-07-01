using System;

public class StateMachineEvents<TypeObject> : StateMachine<TypeObject>
{
    public event Func<State<TypeObject>, State<TypeObject>, bool?> WhenAttemptingChangeState;
    public event Action<State<TypeObject>, State<TypeObject>> OnChangeState;

    public override void ChangeState(State<TypeObject> newState)
    {
        var _return = WhenAttemptingChangeState?.Invoke(CurrentState, newState) ?? false;
        if (_return || newState is null)
            return;

        base.ChangeState(newState);

        OnChangeState?.Invoke(PreviousState, CurrentState);
    }

 
}

