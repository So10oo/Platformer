using UnityEngine;

public class MovingState : GroundedState
{
    public MovingState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Mathf.Abs(rb.velocity.x) <= 0.1)
        {
            stateMachine.ChangeState(character["standing"]);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        character.animator.SetFloat("MovingBlend", Mathf.Abs(rb.velocity.x) / settings.maxSpeedX);
        character.animator.SetFloat("SpeedVertical", rb.velocity.y);
    }
}
