using UnityEngine;

public abstract class MovementPossibleState : AttackableState
{
    public MovementPossibleState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {

    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = inputService.GamePlay.Move.ReadValue<Vector2>().x;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = inputService.GamePlay.Move.ReadValue<Vector2>().x;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
        character.animator.SetFloat("MovingBlend", Mathf.Abs(rb.velocity.x) / 12.0f);
        character.animator.SetFloat("SpeedVertical", rb.velocity.y);
    }

    private void Move()
    {
        var velocityX = rb.velocity.x;
        if (horizontalInput * velocityX < -0.01 || horizontalInput == 0) // -0.01 тк rb.SetVelocityX(0)  rb.velocity.x==0 - false
        {
            rb.SetVelocityX(Mathf.MoveTowards(rb.velocity.x, 0.0f, settings.reverseAcceleration * Time.fixedDeltaTime));
        }
        else
        {
            if (horizontalInput != 0 /*&& Mathf.Abs(velocityX) < settings.maxSpeedX*/)
            {
                var power = Vector2.zero;

                if (Mathf.Abs(velocityX) < settings.maxSpeedX)
                {
                    var adjustedAcceleration = settings.maxSpeedX - Mathf.Abs(velocityX);
                    if (adjustedAcceleration > settings.directAcceleration)
                        power.x = horizontalInput * settings.directAcceleration;
                    else
                        power.x = horizontalInput * adjustedAcceleration;
                }
                //else
                    //power.x = -Mathf.Sign(velocityX) * settings.directAcceleration;



                rb.AddForce(power, ForceMode2D.Impulse);
                Debug.Log(power.x);
            }
             
            //Debug.Log(power.x);
        }


    }


}

