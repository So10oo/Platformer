public class StateMachine<TypeObject>
{
    public State<TypeObject> PreviousState { get; private set; }
    public State<TypeObject> CurrentState { get; private set; }

    public void Initialize(State<TypeObject> startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public virtual void ChangeState(State<TypeObject> newState)
    {
        CurrentState.Exit();
        PreviousState = CurrentState;
        CurrentState = newState;
        newState.Enter();
    }

}
