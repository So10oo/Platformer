using UnityEngine;

public class StandingState : GroundedState
{
    public StandingState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Mathf.Abs(rb.velocity.x) >= 0.1)
        {
            stateMachine.ChangeState(_this["moving"]);
        }
    }
}
