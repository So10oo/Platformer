using UnityEngine;

public class AnimationInitialize : MonoBehaviour
{
    [SerializeField] Character chatacter;

    [Header("Obstacle")]
    [SerializeField] LayerCheck _isCeiling;
    [SerializeField] LayerCheck _isGround;

    int _run = Animator.StringToHash("run");
    int _isfall = Animator.StringToHash("fall");
    int _isJump = Animator.StringToHash("jump");
    int _idle = Animator.StringToHash("idle");
    int _dash = Animator.StringToHash("dash");


    Animator animator;
    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();//Animator.SetTrigger

        _isGround.ValueChandge += (b) => animator.SetBool("IsGrounded", b);

    }

    
    private void FixedUpdate()
    {
        animator.SetFloat("MovingBlend", Mathf.Abs(rb.velocity.x) / 12.0f);
        animator.SetFloat("SpeedVertical", rb.velocity.y);
    }
}

