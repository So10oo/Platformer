using UnityEngine;

public class MovingState : GroundedState
{
    public MovingState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        if (Mathf.Abs(rb.velocity.x) <= 0.1)
        {
            stateMachine.ChangeState(_this["standing"]);
            return true;
        }
        return false;
    }
}
