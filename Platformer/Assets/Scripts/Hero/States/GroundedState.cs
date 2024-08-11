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
            && Mathf.Abs(starte.DelayedPressing.Item2 - Time.time) < character.playerSettings.timeDelayedPressin)
        {
            stateMachine.ChangeState(character["jumping"]);
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
        if (rb.velocity.y != 0f && !character.isGround)
        {
            stateMachine.ChangeState(character["freeFall"]);
            return;
        }  
        else if (jumpKey)
        {
            stateMachine.ChangeState(character["jumping"]);
            return;
        }
        if (inputService.GamePlay.Interactive.IsPressed())
        {
            character.action?.Interaction();
        }
    }
}
