using UnityEngine;

public class FreeFallState : MovementDashPossibleState
{
    float _timeToEnter;
    float _gravity;
    public (bool,float) DelayedPressing { get; private set; }

    public FreeFallState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _gravity = rb.gravityScale;
        rb.gravityScale = settings.gravityScaleDown;

        _timeToEnter = 0;
        DelayedPressing = (false, Time.time);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;
        if (inputService.GamePlay.Jump.WasPressedThisFrame())
            DelayedPressing = (true, Time.time);
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
        }
        else
        {
            if (_timeToEnter < settings.delayedJumpTime && stateMachine.PreviousState is GroundedState && inputService.GamePlay.Jump.WasPressedThisFrame()) 
            {
                stateMachine.ChangeState(_this["jumping"]);
                return;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = _gravity;
    }
}

