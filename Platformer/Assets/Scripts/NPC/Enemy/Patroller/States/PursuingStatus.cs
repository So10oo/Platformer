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

        speechWindow.text = "щас получишь...";
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _canMove = _this.GroundArea.Value;
        if (!eyes.isVisible)
            /*stateMachine.*/ChangeState(_this.failureStatus);
        RotateX(eyes.targetPosition.x - position.x);
        if (!_canMove) return;
        var x = Mathf.MoveTowards(position.x, eyes.targetPosition.x, patrollerSettings.pursuingSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(position, eyes.targetPosition) < 1) 
        {
            _this.WeaponSlot?.weapon?.Attack();
        }
        else
            SetAndRotateX(x);

    }

}

