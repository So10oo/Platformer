using System.Security.Cryptography;
using UnityEngine;

public class JumpingState : MovementDashPossibleState
{
    bool jumpKey;

    public JumpingState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.SetVelocityY(0);
        rb.AddForce(Vector2.up * settings.forceJump, ForceMode2D.Impulse);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        jumpKey = inputService.GamePlay.Jump.IsPressed();
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        if (rb.velocity.y <= 0 || _this.isCeiling)
        {
            ChangeState(_this["freeFall"]);
            return true;
        }

        if (!jumpKey)
        {
            var forse = rb.velocity.y / 3f;
            rb.AddForce(new Vector2(0, -forse), ForceMode2D.Impulse);
            ChangeState(_this["freeFall"]);
            return true;
        }

        return false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (jumpKey)
        {
            var force = settings.curveForceJump.Evaluate(timeToEnter) - rb.velocity.y;
            rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }
        
    }

}
