using UnityEngine;

public class ClimbingState : BaseCharacterState
{
    float EnterTime = 1f;
    Vector2 targetPoint;
    float timeToEnter;
    float horizontalInput;
    float saveGravityScale;
    float distantClimbing;

    public ClimbingState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timeToEnter = 0;
        targetPoint = _this.climbingHit.point;
        rb.velocity = Vector2.zero;
        horizontalInput = inputService.GamePlay.Move.ReadValue<Vector2>().x;
        var dx  = _this.transform.position.x - targetPoint.x;
        var dy = _this.transform.position.y - targetPoint.y;
        distantClimbing = Mathf.Abs(dx) + Mathf.Abs(dy);//Vector2.Distance(_this.transform.position, targetPoint) ;
        saveGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        rb.gravityScale = saveGravityScale;
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        horizontalInput = inputService.GamePlay.Move.ReadValue<Vector2>().x;
        if (horizontalInput != 0)
        {
            if (targetPoint.y - _this.transform.position.y > 0)
                rb.position += new Vector2(0, distantClimbing / EnterTime) * Time.fixedDeltaTime;
            else
                rb.position += new Vector2(horizontalInput * distantClimbing / EnterTime, 0) * Time.fixedDeltaTime;

            if (timeToEnter > EnterTime)
                ChangeState(_this["moving"]); //("moving");
            timeToEnter += Time.fixedDeltaTime;
        }
        else
        {
            ChangeState(_this["freeFall"]);
        }
    }
}

