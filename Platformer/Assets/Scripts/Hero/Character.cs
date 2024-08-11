using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [Header("PhysicsSettings")]
    public PlayerSettings playerSettings;

    [Header("Obstacle")]
    [SerializeField] LayerCheck _isCeiling;
    [SerializeField] LayerCheck _isGround;

    [Header("Animator")]
    [SerializeField] Animator _animator;

    StateMachineEvents<Character> stateMachine;
    DictionaryStates states;
    InputService inputService;

    public RotateView rotateView; 
    public bool isCeiling => _isCeiling.Value;
    public bool isGround => _isGround.Value;
    public Animator animator => _animator;
    public BaseCharacterState this[string key]
    {
        get => states[key];
        set => states[key] = value;
    }

    public IActionCharacter action;

    [Inject]
    void Construct(InputService _inputService, StateMachineEvents<Character> _stateMachine)
    {
        stateMachine = _stateMachine;
        rotateView = new RotateView(_animator.transform, RotateView.RotateMode.Z);
        states = new DictionaryStates(this, _inputService, stateMachine);
        inputService = _inputService;
    }

    private bool? OnChangeLockableState(State<Character> previousState, State<Character> newState)
    {
        if (newState is LockableState lockableState)
            return lockableState.LockState;
        return null;
    }

    #region MonoBehaviourMetod
    private void Awake()
    {
        stateMachine.WhenAttemptingChangeState += OnChangeLockableState;
        //GetComponent<HealthPoint>().OnDeath += () => gameObject.SetActive(false);
        r = GetComponent<Rigidbody2D>();

        _isGround.ValueChandge += (b) => _animator.SetBool("IsGrounded", b);
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

    private void OnEnable()
    {
        inputService.Enable();
    }

    private void OnDisable()
    {
        inputService.Disable();
    }
    #endregion

    #region Weapon
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
    #endregion

    #region OnGUI
    Rigidbody2D r;
    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 200, 20), $"State : {stateMachine.CurrentState}");
        GUI.Box(new Rect(10, 30, 200, 20), $"Speed : {r.velocity}");
        GUI.Box(new Rect(10, 50, 200, 20), $"input : {inputService.GamePlay.Move.ReadValue<Vector2>().x}");
    }
    #endregion
}


