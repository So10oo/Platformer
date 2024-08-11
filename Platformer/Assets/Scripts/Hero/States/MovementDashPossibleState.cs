
public abstract class MovementDashPossibleState : MovementPossibleState
{

    protected MovementDashPossibleState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (inputService.GamePlay.Dash.IsPressed()) 
        {
            /*stateMachine.*/ChangeState(character["dash"]);
            return;
        }
    }

}

