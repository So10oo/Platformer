using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Move")]
    [SerializeField] float _speed = 15f;
    public float speed => _speed;

    [Header("Jump")]
    [SerializeField] AnimationCurve _jumpSpeedCurve;
    [SerializeField] float _delayedJumpTime = 0.12f;
    [SerializeField] float _timeDelayedPressing = 0.1f;
    public AnimationCurve jumpSpeedCurve => _jumpSpeedCurve;
    public float delayedJumpTime => _delayedJumpTime;//врем€ при котором если персонаж начал падать(freeFall состо€ние) можно еще сделать прыжок
    public float timeDelayedPressin => _timeDelayedPressing;//врем€ при котором можно отложенно прыгать(после состо€ни€ freeFall) 

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
