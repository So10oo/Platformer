public class StateMachine<TypeCharacter>
{
    public State<TypeCharacter> PreviousState { get; private set; }
    public State<TypeCharacter> CurrentState { get; private set; }

    public void Initialize(State<TypeCharacter> startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public virtual void ChangeState(State<TypeCharacter> newState)
    {
        CurrentState.Exit();
        PreviousState = CurrentState;
        CurrentState = newState;
        newState.Enter();
    }

}
