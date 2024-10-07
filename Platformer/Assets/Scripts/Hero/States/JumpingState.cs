using System.Linq;
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (rb.velocity.y <= 0)
            ChangeState(_this["freeFall"]);//баг возникает при нажатии деша в данном состоянии
        else if (_this.isCeiling)
            stateMachine.ChangeState(_this["freeFall"]);
    }

}
