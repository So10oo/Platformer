using UnityEngine;

public class AnimationInitialize : MonoBehaviour
{
    [SerializeField] Character chatacter;

    int _run = Animator.StringToHash("run");
    int _isfall = Animator.StringToHash("fall");
    int _isJump = Animator.StringToHash("jump");
    int _idle = Animator.StringToHash("idle");

    private void Awake()
    {
        var anim = gameObject.GetComponent<Animator>();
        chatacter["standing"].OnEnter += () => anim.SetTrigger(_idle);
        chatacter["jumping"].OnEnter += () => anim.SetTrigger(_isJump);
        chatacter["freeFall"].OnEnter += () => anim.SetTrigger(_isfall);
        chatacter["moving"].OnEnter += () => anim.SetTrigger(_run);
    }
}

