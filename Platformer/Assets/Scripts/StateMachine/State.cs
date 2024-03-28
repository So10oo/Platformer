public abstract class State<TypeCharacter>
{
    protected TypeCharacter character;
    protected StateMachine<TypeCharacter> stateMachine;

    protected State(TypeCharacter character, StateMachine<TypeCharacter> stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void Exit()
    {

    }

    public bool isActiveState => this == stateMachine.CurrentState;
}
