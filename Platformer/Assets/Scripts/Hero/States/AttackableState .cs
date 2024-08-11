using UnityEngine;

public class AttackableState : RotatableState
{
    public AttackableState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (inputService.GamePlay.Attack.WasPressedThisFrame()) 
        {
            character.Attack();
        }
    }
}

