public abstract class State<TypeCharacter>
{
    protected TypeCharacter _this;
    protected StateMachine<TypeCharacter> stateMachine;

    protected State(TypeCharacter _this, StateMachine<TypeCharacter> stateMachine)
    {
        this._this = _this;
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

    public void ChangeState(State<TypeCharacter> newState)
    {
        if (isActiveState)
            stateMachine.ChangeState(newState);
    }
}
