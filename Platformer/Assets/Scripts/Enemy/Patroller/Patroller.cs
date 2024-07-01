using UnityEngine;


public class Patroller : Enemy
{
    [SerializeField] PatrollerSettings _settings;

    public SpeechWindow speechWindow;

    [SerializeField] EnemyEyes _eyes;
    public EnemyEyes Eyes => _eyes;

    [SerializeField] LayerCheck _groundArea;
    public LayerCheck GroundArea => _groundArea;

    [SerializeField] WeaponSlot _weaponSlot;
    public WeaponSlot WeaponSlot => _weaponSlot;

    StateMachine<Patroller> _stateMachine;
    public PursuingStatus pursuing;
    public PatrollingStatus patrolling;
    //public AttackingStatus attacking;
    public DetectingStatue detectingStatue;
    public FailureStatus failureStatus;

    private void Awake()
    {
        _stateMachine = new StateMachine<Patroller>();
        var rotate = new RotateView(transform);
        speechWindow.SetRotateView(rotate);
        pursuing = new PursuingStatus(this, _stateMachine, _settings, rotate);
        patrolling = new PatrollingStatus(this, _stateMachine, _settings, rotate);
        //attacking = new AttackingStatus(this, _stateMachine, _settings, rotate);
        detectingStatue = new DetectingStatue(this, _stateMachine, _settings, rotate);
        failureStatus = new FailureStatus(this, _stateMachine, _settings, rotate);
        _stateMachine.Initialize(patrolling);
    }

    private void Update()
    {
        _stateMachine.CurrentState.HandleInput();
        _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedUpdate();
    }
}

