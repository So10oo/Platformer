using UnityEngine;

public abstract class MovementPossibleState : AttackableState
{
    public MovementPossibleState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {

    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = rb.velocity.x / physicsSettings.speed;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = inputService.GetHorizontalMove();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var velocity = rb.velocity;
        velocity.x = horizontalInput * physicsSettings.speed;
        rb.velocity = velocity;
    }

}

