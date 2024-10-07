using UnityEngine;

public abstract class MovePatrollerStatus : BasePatrollingState
{
    RotateView rotateView; 

    public MovePatrollerStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings)
    {
        this.rotateView = rotateView; 
    }

    protected void SetAndRotateX(float x)
    {
        rotateView.ViewData(x-position.x);

        var pos = _this.transform.position;
        pos.x = x;
        _this.transform.position = pos;
    }

    protected void RotateX(float dx)
    {
        rotateView.ViewData(dx);
    }

}