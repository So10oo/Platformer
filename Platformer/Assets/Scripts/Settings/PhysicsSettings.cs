using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsSettings", menuName = "ScriptableObjects/PhysicsSettings")]
public class PhysicsSettings : ScriptableObject
{
    [Header("Move")]
    [SerializeField] float _speed = 15f;
    public float speed => _speed;

    [Header("Jump")]
    [SerializeField] AnimationCurve _jumpSpeedCurve;
    [SerializeField] float _delayedJumpTime = 0.12f;
    //[SerializeField] float _buttonPressedTime = 0.3f;
    //[SerializeField] float _jumpStartSpeed = 20;
    //[SerializeField] float _jumpEndSpeed = 15;
    [SerializeField] float _timeDelayedPressing = 0.1f;
    public AnimationCurve jumpSpeedCurve => _jumpSpeedCurve;
    //public float buttonPressedTime => _buttonPressedTime;
    //public float jumpStartSpeed => _jumpStartSpeed;
    //public float jumpEndSpeed => _jumpEndSpeed;
    public float delayedJumpTime => _delayedJumpTime;//врем€ при котором если персонаж начал падать(freeFall состо€ние) можно еще сделать прыжок
    public float timeDelayedPressin => _timeDelayedPressing;//врем€ при котором можно отложенно прыгать(после состо€ни€ freeFall) 

    [Header("Dash")]
    //[SerializeField] float _dashStartSpeed = 50;
    //[SerializeField] float _dashEndSpeed = 0;
    //[SerializeField] float _dashTime = 0.2f;
    [SerializeField] AnimationCurve _dashSpeedCurve;
    [SerializeField] float _delayedDash = 0.2f;
    //public float dashStartSpeed => _dashStartSpeed;
    //public float dashEndSpeed => _dashEndSpeed;
    //public float dashTime => _dashTime;
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
