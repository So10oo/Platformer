
public abstract class MovementDashPossibleState : MovementPossibleState
{

    protected MovementDashPossibleState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (inputService.GetButtonDashDown())
        {
            stateMachine.ChangeState(character.states["dash"]);
            return;
        }
    }

}

