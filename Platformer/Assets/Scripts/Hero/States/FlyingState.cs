using UnityEngine;

public class FlyingState : AttackableState
{
    protected float verticalInput;
    float gravityScale;

    public FlyingState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        gravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        horizontalInput = rb.velocity.x / settings.horizontalFlyingSpeed;
        horizontalInput = rb.velocity.y / settings.verticalFlyingSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = gravityScale;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        var move = inputService.GamePlay.Move.ReadValue<Vector2>();
        verticalInput = move.x;
        horizontalInput = move.y;

        if (inputService.GamePlay.Interactive.IsPressed())
        {
            stateMachine.ChangeState(_this["freeFall"]);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //var velocity = rb.velocity;
        //velocity.y = verticalInput * physicsSettings.verticalFlyingSpeed;
        //velocity.x = horizontalInput * physicsSettings.horizontalFlyingSpeed;
        //rb.velocity = velocity;
    }
}

