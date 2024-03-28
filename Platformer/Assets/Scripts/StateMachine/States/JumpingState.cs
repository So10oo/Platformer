using System.Linq;
using UnityEngine;

public class JumpingState : MovementDashPossibleState
{
    float buttonPressedTime;
    float jumpTime;
    bool jumping;


    public JumpingState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpTime = 0;
        jumping = true;
        buttonPressedTime = physicsSettings.jumpSpeedCurve.keys.Last().time;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!inputService.GetButtonJump())
        {
            jumping = false;
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (jumpTime > buttonPressedTime)
        {
            jumping = false;
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
        }
        else if (rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(character.states["freeFall"]);
            return;
        }

        if (character.IsCeiling.Value)
        {
            stateMachine.ChangeState(character.states["freeFall"]);
            return;
        }

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (jumping)
        {
            var currentJumpSpeed = physicsSettings.jumpSpeedCurve.Evaluate(jumpTime);
            rb.velocity = new Vector2(rb.velocity.x, currentJumpSpeed);
        }
    }



}
