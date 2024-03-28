using System;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [Header("PhysicsSettings")]
    public PhysicsSettings physicsSettings;

    [Header("Obstacle")]
    public LayerCheck IsCeiling;
    public LayerCheck IsGround; 

    public DictionaryStates states;

    StateMachineEvents<Character> stateMachine;
    [Inject]
    void Construct(IInputService _inputService, StateMachineEvents<Character> _stateMachine)
    {
        stateMachine = _stateMachine;
        states = new DictionaryStates(this, _inputService, stateMachine);
    }

    Func<bool?> _interaction;
    public event Func<bool?> Interaction
    {
        add
        {
            _interaction = value;
        }
        remove
        {
            _interaction = null;
        }
    }
    public bool InteractionInvoke() => _interaction?.Invoke() ?? false;

    private bool? OnChangeLockableState(State<Character> previousState, State<Character> newState)
    {
        if (newState is LockableState lockableState)
        {
            return lockableState.LockState;
        }
        return null;
    }

    private void Awake()
    {
        stateMachine.WhenAttemptingChangeState += OnChangeLockableState;
    }

    void Start()
    {
        stateMachine.Initialize(states["freeFall"]);
    }

    void Update()
    {
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();
    }

    private void LateUpdate()
    {
        stateMachine.CurrentState.LateUpdate();
    }

    [Header("Weapon")]
    [SerializeField] Weapon _weapon;
    [SerializeField] Transform _transformWeapon;

    public void GetWeapon(Weapon weapon)
    {
        if (_weapon != null)
        {
            Destroy(_weapon.gameObject);
        }
        _weapon = weapon;
        _weapon.transform.SetParent(_transformWeapon, false);
    }

    public void Attack()
    {
        _weapon?.Attack();
    }
}


