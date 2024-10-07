using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [field: Header("Move")]
    [field: SerializeField] public float maxSpeedX { private set; get; } = 15f;
    [field: SerializeField] public float reverseAcceleration { private set; get; } = 50f;
    [field: SerializeField] public float directAcceleration { private set; get; } = 1f;
    [field: SerializeField] public float startDirectAcceleration { private set; get; } = 8f;

    [field: Header("Jump")]
    [field: SerializeField] public float forceJump { private set; get; } = 17;
    [field: SerializeField] public float timeDelayedPressin { private set; get; } = 0.1f;
    [field: SerializeField] public float delayedJumpTime { private set; get; } = 0.12f;

    [field: Header("Dash")]
    [field: SerializeField] public AnimationCurve dashSpeedCurve { private set; get; }
    [field: SerializeField] public float delayedDash { private set; get; } = 0.2f;

    [field: Header("Flying")]
    [field: SerializeField] public float horizontalFlyingSpeed { private set; get; } = 10f;
    [field: SerializeField] public float verticalFlyingSpeed { private set; get; } = 10f;

    [field: Header("Gravity")]
    [field: SerializeField] public float gravityScaleDown { private set; get; } = 6.5f;
    [field: SerializeField] public float gravityScaleUp { private set; get; } = 4f;

    [field: Header("Speed limits")]
    [field: SerializeField] public float maxSpeedY { private set; get; } = 25f;

}
