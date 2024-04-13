using System;
using UnityEngine;


public class Patroller : Enemy
{
    [SerializeField] PatrollerSettings settings;

    StateMachine<Patroller> stateMachine;

    public AttackStatus attack;
    public PursuingStatus pursuing;
    public PatrollingStatus patrolling;

    
    public Vector2 SpawnPoint { get; private set; }

    private void Awake()
    {
        SpawnPoint = (Vector2)transform.position;

        stateMachine = new StateMachine<Patroller>();
        attack = new AttackStatus(this, stateMachine, settings);
        pursuing = new PursuingStatus(this, stateMachine, settings);
        patrolling = new PatrollingStatus(this, stateMachine, settings);
        stateMachine.Initialize(patrolling);
    }

    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
}

