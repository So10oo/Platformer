using UnityEngine;

public class AttackingStatus : AttackedStatus
{
    float timeToEnter;
    bool isDealingDamage;
    public AttackingStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
    }

    public override void Enter()
    {
        base.Enter(); 
        _this.animator.SetFloat("MovingBlend", 0f);
        _this.animator.SetTrigger("Attack");
        timeToEnter = 0;
        isDealingDamage = false;
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        timeToEnter += Time.deltaTime;
        if (timeToEnter > 1f)
        {
            stateMachine.ChangeState(_this.pursuing);
            return true;
        }
        if (timeToEnter > 0.3f && !isDealingDamage)
        {
            isDealingDamage = true;
            _this.WeaponSlot?.weapon?.DealingDamage();
        }
        return false;
    }

}

