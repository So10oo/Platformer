using UnityEngine;

public class AttackableState : RotatableState
{
    public AttackableState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (inputService.GetButtonAttackDown())
        {
            character.Attack();
        }
    }
}

