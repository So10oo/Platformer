public abstract class MovementDashPossibleState : MovementPossibleState
{

    protected MovementDashPossibleState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
        
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        //проверка находится ли импут и положение точки системы с одной стороны
        if (_this.climbingHit && (_this.climbingHit.point.x - _this.transform.position.x) * horizontalInput > 0)
        {
            ChangeState(_this["climbing"]);
            return true;
        }

        if (inputService.GamePlay.Dash.IsPressed()) 
        {
            ChangeState(_this["dash"]);
            return true;
        }
        return false;
    }


}

