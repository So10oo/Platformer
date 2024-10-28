using System;
using TMPro;
using UnityEngine;

public abstract class BaseCharacterState : State<Character>
{
    public event Action OnEnter;
    public event Action OnExit;

    protected InputService inputService;
    protected Rigidbody2D rb;
    protected PlayerSettings settings;
    
    protected float timeToEnter { get; private set; }
    public BaseCharacterState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine)
    {
        this.inputService = inputService;
        rb = character.GetComponent<Rigidbody2D>();
        settings = character.playerSettings;
    }

    public override void Enter()
    {
        timeToEnter = 0;
        OnEnter?.Invoke();
    }

    public override bool LogicUpdate()
    {
        timeToEnter += Time.deltaTime;
        return false;
    }

    public override void Exit() 
    {
        OnExit?.Invoke();
    }
}

