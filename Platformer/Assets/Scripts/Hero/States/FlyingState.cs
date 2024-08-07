﻿using UnityEngine;

public class FlyingState : AttackableState
{
    protected float verticalInput;
    float gravityScale;

    public FlyingState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        gravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        horizontalInput = rb.velocity.x / physicsSettings.horizontalFlyingSpeed;
        horizontalInput = rb.velocity.y / physicsSettings.verticalFlyingSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = gravityScale;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        verticalInput = inputService.GetVerticalMove();
        horizontalInput = inputService.GetHorizontalMove();

        if (inputService.GetButtonInteraction())
        {
            stateMachine.ChangeState(character["freeFall"]);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var velocity = rb.velocity;
        velocity.y = verticalInput * physicsSettings.verticalFlyingSpeed;
        velocity.x = horizontalInput * physicsSettings.horizontalFlyingSpeed;
        rb.velocity = velocity;
    }
}

