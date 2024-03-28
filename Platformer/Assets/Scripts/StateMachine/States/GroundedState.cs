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
            && Mathf.Abs(starte.DelayedPressing.Item2 - Time.time) < character.physicsSettings.timeDelayedPressin) 
        {
            stateMachine.ChangeState(character.states["jumping"]);
        }

    }

    public override void HandleInput()
    {
        base.HandleInput();
        var jampKey = inputService.GetButtonJumpDown();
        if (jampKey)
        {
            stateMachine.ChangeState(character.states["jumping"]);
            return;
        }
             
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (rb.velocity.y < 0f && !character.IsGround.Value)
        {
            stateMachine.ChangeState(character.states["freeFall"]);
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

