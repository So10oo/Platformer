using UnityEngine;

public abstract class RotatableState : BaseCharacterState
{
    protected float horizontalInput;
    private RotateView rotateView;

    public RotatableState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
        rotateView = character.rotateView;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        rotateView.ViewData(horizontalInput);
    }
}

