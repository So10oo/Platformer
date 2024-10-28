using UnityEngine;

public abstract class GroundedState : MovementDashPossibleState
{
    bool jumpKey;
    protected GroundedState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (stateMachine.PreviousState is ITrackingDelayedJump state
            && state.DelayedPressing.Item1
            && Mathf.Abs(state.DelayedPressing.Item2 - Time.time) < _this.playerSettings.timeDelayedPressin)
        {
            stateMachine.ChangeState(_this["jumping"]);
        }

    }

    public override void HandleInput()
    {
        base.HandleInput();
        jumpKey = inputService.GamePlay.Jump.WasPressedThisFrame();
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        if (rb.velocity.y != 0f && !_this.isGround)
        {
            stateMachine.ChangeState(_this["freeFall"]);
            return true;
        }  
        else if (jumpKey)
        {
            stateMachine.ChangeState(_this["jumping"]);
            return true;
        }
        if (inputService.GamePlay.Interactive.IsPressed())
        {
            _this.action?.Interaction();
        }
        return false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _this.animator.SetFloat("MoveBlend", Mathf.Abs(rb.velocity.x) * 3f / settings.maxSpeedX);
        _this.animator.SetFloat("VelocityY", rb.velocity.y);
    }
}
