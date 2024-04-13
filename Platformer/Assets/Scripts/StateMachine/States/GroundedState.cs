using UnityEngine;

public abstract class GroundedState : MovementDashPossibleState
{
    protected GroundedState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
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
        var jampKey = inputService.GetButtonJumpDown();
        if (jampKey)
        {
            stateMachine.ChangeState(character["jumping"]);
            return;
        }
             
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (rb.velocity.y < 0f && !character.IsGround)
        {
            stateMachine.ChangeState(character["freeFall"]);
            return;
        }
        if (inputService.GetButtonInteraction())
        {
            var _return = character.InteractionInvoke();
            if (_return)
                return;
        }
    }
}

