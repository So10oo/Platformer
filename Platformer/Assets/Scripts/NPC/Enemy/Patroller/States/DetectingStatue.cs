using UnityEngine;

public class DetectingStatue : AttackedStatus
{
    public DetectingStatue(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings,RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
    }

    float _timeToEnter;

    public override void Enter()
    {
        base.Enter();   
        _timeToEnter = 0;
        _this.animator.SetFloat("MovingBlend", 0f);
        speechWindow.text = "Найден";
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;
        if (_timeToEnter > patrollerSettings.timeDetectingStatue) 
        {
            ChangeState(_this.pursuing);
            return true;
        }
        return false;   
    }
}

