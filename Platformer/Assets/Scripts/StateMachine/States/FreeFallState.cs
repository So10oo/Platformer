using UnityEngine;

public class FreeFallState : MovementDashPossibleState
{
    float _timeToEnter;

    public (bool,float) DelayedPressing { get; private set; }

    public FreeFallState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
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

        if (inputService.GetButtonJumpDown())
        {
            DelayedPressing = (true, Time.time);
        }


        if (character.IsGround.Value /*|| Mathf.Abs(rb.velocity.y) <= float.Epsilon*/) 
        {
            if (Mathf.Abs(rb.velocity.x) <= float.Epsilon)
                stateMachine.ChangeState(character.states["standing"]);
            else
                stateMachine.ChangeState(character.states["moving"]);
        }
        else
        {
            if (_timeToEnter < physicsSettings.delayedJumpTime && stateMachine.PreviousState is GroundedState && inputService.GetButtonJumpDown()/*Input.GetKeyDown(KeyCode.Space)*/)
            {
                stateMachine.ChangeState(character.states["jumping"]);
                return;
            }
        }
    }
}

