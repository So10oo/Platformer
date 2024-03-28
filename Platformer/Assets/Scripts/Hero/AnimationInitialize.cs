using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using Zenject;

public class AnimationInitialize : MonoBehaviour
{
    int _run = Animator.StringToHash("run");
    int _isfall = Animator.StringToHash("fall");
    int _isJump = Animator.StringToHash("jump");
    int _idle = Animator.StringToHash("idle");

    Animator anim;
    [SerializeField]Character a;
    

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        a.states["standing"].OnEnter += () => anim.SetTrigger(_idle);
        a.states["jumping"].OnEnter += () => anim.SetTrigger(_isJump);
        a.states["freeFall"].OnEnter += () => anim.SetTrigger(_isfall);
        a.states["moving"].OnEnter += () => anim.SetTrigger(_run);
    }


    //Add("standing"
    // Add("jumping",
    //Add("freeFall"
    //Add("moving",

}

