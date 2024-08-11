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

        speechWindow.text = "нашел!";
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;
        if (_timeToEnter > patrollerSettings.timeDetectingStatue) 
        {
            /*stateMachine.*/ChangeState(character.pursuing);
        }
    }
}

