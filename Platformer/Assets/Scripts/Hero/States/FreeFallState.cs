using UnityEngine;

public class FreeFallState : MovementDashPossibleState
{
    float _timeToEnter;

    public (bool,float) DelayedPressing { get; private set; }

    public FreeFallState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _timeToEnter = 0;
        DelayedPressing = (false, Time.time);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;

        if (inputService.GamePlay.Jump.WasPressedThisFrame()) 
        {
            DelayedPressing = (true, Time.time);
        }


        if (character.isGround /*|| Mathf.Abs(rb.velocity.y) <= float.Epsilon*/) 
        {
            if (Mathf.Abs(rb.velocity.x) <= float.Epsilon)
                stateMachine.ChangeState(character["standing"]);
            else
                stateMachine.ChangeState(character["moving"]);
        }
        else
        {
            if (_timeToEnter < settings.delayedJumpTime && stateMachine.PreviousState is GroundedState && inputService.GamePlay.Jump.WasPressedThisFrame()) 
            {
                stateMachine.ChangeState(character["jumping"]);
                return;
            }
        }
    }
}

