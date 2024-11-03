using UnityEngine;

public class PursuingStatus : AttackedStatus
{
    bool _canMove;
    float timeBlindness;

    public PursuingStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
    }
     
    public override void Enter()
    {
        base.Enter();
        eyes.SetViewingDate(10, Mathf.PI);
        _this.animator.SetFloat("MovingBlend", 1f);
        speechWindow.text = "Преследование";
        timeBlindness = 0f;
    }

    public override bool LogicUpdate()
    {
        base.LogicUpdate();
        _canMove = _this.GroundArea.InLayer;

        if (!eyes.isVisible)
        {
            timeBlindness += Time.deltaTime;
            if (timeBlindness > 0.5f)
                ChangeState(_this.failureStatus);
        }
        else
            timeBlindness = 0;

        RotateX(eyes.targetPosition.x - position.x);

        if (!_canMove) return false;

        if (Vector2.Distance(position, eyes.targetPosition) < 1f) 
            ChangeState(_this.attacking);
        else
        {
            var dx = Mathf.MoveTowards(position.x, eyes.targetPosition.x, patrollerSettings.pursuingSpeed * Time.deltaTime);
            SetAndRotateX(dx);
        }
            
        return false;
    }



}

