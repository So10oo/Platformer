using UnityEngine;

public class PursuingStatus : AttackedStatus
{
    public PursuingStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
    }

    bool _canMove;

    public override void Enter()
    {
        base.Enter();
        eyes.SetViewingDate(10, Mathf.PI);
        _this.animator.SetFloat("MovingBlend", 1f);
        speechWindow.text = "Преследование";
        if (_this.WeaponSlot?.weapon is Weapon weapon)
            weapon.OnDealingDamage += AnimationAttack;
    }

    public override void Exit()
    {
        base.Exit();
        if (_this.WeaponSlot?.weapon is Weapon weapon)
            weapon.OnDealingDamage -= AnimationAttack;
    }

    public void AnimationAttack() => _this.animator.SetTrigger("Attack");

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        _canMove = _this.GroundArea.Value;
        if (!eyes.isVisible)
            ChangeState(_this.failureStatus);
        RotateX(eyes.targetPosition.x - position.x);
        if (!_canMove) return false;

        var x = Mathf.MoveTowards(position.x, eyes.targetPosition.x, patrollerSettings.pursuingSpeed * Time.deltaTime);
        if (Vector2.Distance(position, eyes.targetPosition) < 1)
        {
            _this.WeaponSlot?.weapon?.DealingDamage();
        }
        else
            SetAndRotateX(x);
        return false;
    }

    //public override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //    _canMove = _this.GroundArea.Value;
    //    if (!eyes.isVisible)
    //        ChangeState(_this.failureStatus);
    //    RotateX(eyes.targetPosition.x - position.x);
    //    if (!_canMove) return;
    //    var x = Mathf.MoveTowards(position.x, eyes.targetPosition.x, patrollerSettings.pursuingSpeed * Time.fixedDeltaTime);
    //    if (Vector2.Distance(position, eyes.targetPosition) < 1) 
    //    {
    //        _this.WeaponSlot?.weapon?.DealingDamage();
    //    }
    //    else
    //        SetAndRotateX(x);
    //}

}

