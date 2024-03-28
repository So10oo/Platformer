using UnityEngine;

public class AttackableState : RotatableState
{
    public AttackableState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {
    }

    //int _attack = Animator.StringToHash("attack");
    public override void HandleInput()
    {
        base.HandleInput();
        if (inputService.GetButtonAttackDown())
        {
            //animator.SetTrigger(_attack);
            character.Attack();
        }
    }
}

