using UnityEngine;

public class FreeFallState : MovementDashPossibleState, ITrackingDelayedJump
{
    float _timeToEnter;
    float _gravity;

    public (bool, float) DelayedPressing { get; set; }

    public FreeFallState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _gravity = rb.gravityScale;
        rb.gravityScale = settings.gravityScaleDown;
        _timeToEnter = 0;
        this.SetDelayedJump(false);
    }

    public override bool LogicUpdate()
    {
        if (base.LogicUpdate()) return true;

        _timeToEnter += Time.deltaTime;
        if (inputService.GamePlay.Jump.WasPressedThisFrame())
            this.SetDelayedJump(true);
        if (rb.velocity.y < -settings.maxSpeedY)
            rb.velocity = new Vector2(rb.velocity.x, -settings.maxSpeedY);
        if (rb.velocity.y < 0)
            rb.gravityScale = settings.gravityScaleDown;
        else
            rb.gravityScale = settings.gravityScaleUp;

        if (_this.isGround)
        {
            if (Mathf.Abs(rb.velocity.x) <= float.Epsilon)
                stateMachine.ChangeState(_this["standing"]);
            else
                stateMachine.ChangeState(_this["moving"]);
            return true;
        }
        else
        {
            if (_timeToEnter < settings.delayedJumpTime && stateMachine.PreviousState is GroundedState && inputService.GamePlay.Jump.WasPressedThisFrame())
            {
                stateMachine.ChangeState(_this["jumping"]);
                return true;
            }
        }
        return false;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = _gravity;
    }
}

