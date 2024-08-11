public abstract class LockableState : BaseCharacterState
{
    protected bool _lockState = false;
    public bool LockState => _lockState;

    public LockableState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }
}

