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
        speechWindow.text = ":C";
        _timeToEnter = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;
        if (_timeToEnter > patrollerSettings.timeFailureStatus)  
        {
            stateMachine.ChangeState(character.patrolling);
        }
    }

}

