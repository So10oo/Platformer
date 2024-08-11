using System;
using UnityEngine;

public abstract class BaseCharacterState : State<Character>
{
    public event Action OnEnter;
    public event Action OnExit;

    protected InputService inputService;
    protected Rigidbody2D rb;
    protected PlayerSettings settings;


    public BaseCharacterState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine)
    {
        this.inputService = inputService;
        rb = character.GetComponent<Rigidbody2D>();
        settings = character.playerSettings;
    }

    public override void LateUpdate()
    {
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

