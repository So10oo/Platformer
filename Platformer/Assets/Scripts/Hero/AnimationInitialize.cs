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
        var anim = gameObject.GetComponent<Animator>();//Animator.SetTrigger
        chatacter["standing"].OnEnter += () => anim.SetTrigger(_idle);
        chatacter["standing"].OnExit += () => anim.ResetTrigger(_idle);
        
        chatacter["jumping"].OnEnter += () => anim.SetTrigger(_isJump);
        chatacter["jumping"].OnExit += () => anim.ResetTrigger(_isJump);
        
        chatacter["freeFall"].OnEnter += () => anim.SetTrigger(_isfall);
        chatacter["freeFall"].OnExit += () => anim.ResetTrigger(_isfall);
        
        chatacter["moving"].OnEnter += () => anim.SetTrigger(_run);
        chatacter["moving"].OnExit += () => anim.ResetTrigger(_run);
        
    }
}

