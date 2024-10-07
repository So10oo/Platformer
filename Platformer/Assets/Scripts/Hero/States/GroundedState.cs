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
        if (stateMachine.PreviousState is FreeFallState starte
            && starte.DelayedPressing.Item1
            && Mathf.Abs(starte.DelayedPressing.Item2 - Time.time) < _this.playerSettings.timeDelayedPressin)
        {
            stateMachine.ChangeState(_this["jumping"]);
        }

    }

    public override void HandleInput()
    {
        base.HandleInput();
        jumpKey = inputService.GamePlay.Jump.WasPressedThisFrame();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (rb.velocity.y != 0f && !_this.isGround)
        {
            stateMachine.ChangeState(_this["freeFall"]);
            return;
        }  
        else if (jumpKey)
        {
            stateMachine.ChangeState(_this["jumping"]);
            return;
        }
        if (inputService.GamePlay.Interactive.IsPressed())
        {
            _this.action?.Interaction();
        }
    }
}
