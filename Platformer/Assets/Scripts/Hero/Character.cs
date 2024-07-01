using System;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour 
{
    [Header("PhysicsSettings")]
    public PlayerSettings playerSettings;

    [Header("Obstacle")]
    [SerializeField] LayerCheck _isCeiling;
    [SerializeField] LayerCheck _isGround;
    public bool IsCeiling => _isCeiling.Value;
    public bool IsGround => _isGround.Value;

    StateMachineEvents<Character> stateMachine;
    DictionaryStates states;
    public BaseCharacterState this[string key]
    {
        get => states[key];
        set => states[key] = value;
    }

    public IActionCharacter action;

    [Inject]
    void Construct(IInputService _inputService, StateMachineEvents<Character> _stateMachine)
    {
        stateMachine = _stateMachine;
        states = new DictionaryStates(this, _inputService, stateMachine);
    }
    private bool? OnChangeLockableState(State<Character> previousState, State<Character> newState)
    {
        if (newState is LockableState lockableState)
        {
            return lockableState.LockState;
        }
        return null;
    }

    #region MonoBehaviourMetod
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
    #endregion

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


