using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Move")]

    [SerializeField] float _maxSpeedX = 15f;
    [SerializeField] float _reverseAcceleration = 50f;
    [SerializeField] float _directAcceleration = 1f;
    public float maxSpeedX => _maxSpeedX;
    public float reverseAcceleration => _reverseAcceleration;
    public float directAcceleration => _directAcceleration;



    [Header("Jump")]

    [SerializeField] float _forceJump = 30;
    [SerializeField] float _timeDelayedPressin = 0.1f;

    [SerializeField] float _delayedJumpTime = 0.12f;


    public float forceJump => _forceJump;
    public float timeDelayedPressin => _timeDelayedPressin;
    public float delayedJumpTime => _delayedJumpTime;



    [Header("Dash")]
    [SerializeField] AnimationCurve _dashSpeedCurve;
    [SerializeField] float _delayedDash = 0.2f;
    public float delayedDash => _delayedDash;
    public AnimationCurve dashSpeedCurve => _dashSpeedCurve;


    [Header("Flying")]
    [SerializeField] float _horizontalFlyingSpeed = 10f;
    [SerializeField] float _verticalFlyingSpeed = 10f;
    public float horizontalFlyingSpeed => _horizontalFlyingSpeed;
    public float verticalFlyingSpeed => _verticalFlyingSpeed;


    [Header("Gravity")]
    [SerializeField] float _gravityScaleUp = 5;
    [SerializeField] float _gravityScaleDown = 8;
    public float gravityScaleUp => _gravityScaleUp;
    public float gravityScaleDown => _gravityScaleDown;

    [Header("Speed limits")]
    [SerializeField] float _maxSpeedY = 30f;
    public float maxSpeedY => _maxSpeedY;

}
