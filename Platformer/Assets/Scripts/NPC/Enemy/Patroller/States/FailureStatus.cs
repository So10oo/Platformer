using UnityEngine;


public class FailureStatus : AttackedStatus
{
    public FailureStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
    }

    float _timeToEnter;
    public override void Enter()
    {
        base.Enter();
        _timeToEnter = 0;
        _this.animator.SetFloat("MovingBlend", 0f);
        speechWindow.text = "Потерян";
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;
        if (_timeToEnter > patrollerSettings.timeFailureStatus)  
        {
            stateMachine.ChangeState(_this.patrolling);
            return true;
        }
        return false;
    }

}

