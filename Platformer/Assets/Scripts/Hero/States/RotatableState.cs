using UnityEngine;

public abstract class RotatableState : BaseCharacterState
{
    protected float horizontalInput;
    private RotateView rotateView;

    public RotatableState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {
        rotateView = new RotateView(character.transform);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        rotateView.ViewData(horizontalInput);
    }
}

