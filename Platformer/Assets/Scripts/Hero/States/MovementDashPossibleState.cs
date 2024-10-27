
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
            /*stateMachine.*/ChangeState(_this["dash"]);
            return;
        }
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_this.climbingHit && (_this.climbingHit.point.x - _this.transform.position.x) * horizontalInput > 0) //проверка находится ли импут и положение точки системы с одной стороны
        {
            ChangeState(_this["climbing"]);
        }
    }
}

