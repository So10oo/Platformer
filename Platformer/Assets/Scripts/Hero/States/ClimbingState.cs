using UnityEngine;

public class ClimbingState : BaseCharacterState, ITrackingDelayedJump
{
    float timeExit;
    Vector2 targetPoint;
    float saveGravityScale;
    float distantClimbing;
    float targetHorizontalInput;

    public (bool, float) DelayedPressing { get; set; }

    public ClimbingState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        targetPoint = _this.climbingHit.point;

        targetHorizontalInput = inputService.GamePlay.Move.ReadValue<Vector2>().x;

        var dx = _this.gameObject.transform.position.x - targetPoint.x;
        var dy = _this.gameObject.transform.position.y - targetPoint.y;
        distantClimbing = Mathf.Abs(dx) + Mathf.Abs(dy);
        timeExit = distantClimbing / 2f;

        this.SetDelayedJump(false);

        saveGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        var ledgeHeight = (_this.climbingHit.point).y - _this.transform.position.y;
        _this.animator.SetFloat("LedgeHeight", ledgeHeight - 0.3f);
        _this.animator.SetBool("IsClimbingLedge", true);
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = saveGravityScale;
        _this.animator.SetBool("IsClimbingLedge", false);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (inputService.GamePlay.Jump.WasPressedThisFrame())
            this.SetDelayedJump(true);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var horizontalInput = inputService.GamePlay.Move.ReadValue<Vector2>().x;
        if (horizontalInput == targetHorizontalInput || timeToEnter > (timeExit / 2f))   
        {
            if (targetPoint.y - _this.transform.position.y > 0)
                rb.position += new Vector2(0, distantClimbing / timeExit) * Time.fixedDeltaTime;
            else
                rb.position += new Vector2(targetHorizontalInput * distantClimbing / timeExit, 0) * Time.fixedDeltaTime;

            if (timeToEnter > timeExit)
                ChangeState(_this["moving"]);
        }
        else
            ChangeState(_this["freeFall"]);
        
    }
}

