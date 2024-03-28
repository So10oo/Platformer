using System;
using UnityEngine;

public abstract class BaseCharacterState : State<Character>
{
    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnLogicUpdate;

    protected IInputService inputService;
    protected Rigidbody2D rb;
    protected PhysicsSettings physicsSettings;

    public BaseCharacterState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine)
    {
        this.inputService = inputService;
        rb = character.gameObject.GetComponent<Rigidbody2D>();
        physicsSettings = character.physicsSettings;
    }

    public override void LateUpdate()
    {
        OnLogicUpdate?.Invoke();
        base.LateUpdate();
    }

    public override void Enter()
    {
        OnEnter?.Invoke();
    }
     
    public override void Exit() 
    {
        OnExit?.Invoke();
    }
}

