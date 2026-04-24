
using UnityEngine;

public abstract class MovementDashPossibleState : MovementPossibleState
{
    const float threshold = 0.5f;
    protected MovementDashPossibleState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {

    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        //проверка находится ли импут и положение точки системы с одной стороны
        var dx = _this.climbingHit.point.x - _this.transform.position.x;
        var dy = _this.climbingHit.point.y - _this.transform.position.y;
        if (_this.climbingHit
            && dx * horizontalInput > 0
            && dy > threshold)
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

    
    //public override void FixedUpdate()
    //{
    //    base.FixedUpdate();

    //    var dx = _this.climbingHit.point.x - _this.transform.position.x;
    //    var dy = _this.climbingHit.point.y - _this.transform.position.y;
    //    if (_this.climbingHit
    //        && dx * horizontalInput > 0
    //        && dy <= treh)
    //    {
    //        var necessaryVel = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y * rb.gravityScale) * dy);
    //        var velY = rb.velocity.y;
    //        if (necessaryVel > velY /*&& velY > -1*/)
    //        {
    //            rb.AddForce(new Vector2(0, (necessaryVel - velY) * 1.2f), ForceMode2D.Impulse);
    //        }
             
    //    }
    //}


}

