using UnityEngine;

public abstract class MovePatrollerStatus : BasePatrollingState
{
    RotateView rotateView; 

    public MovePatrollerStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings)
    {
        this.rotateView = rotateView; 
    }

    protected void SetX(float x)
    {
        rotateView.ViewData(x-character.transform.position.x);

        var pos = character.transform.position;
        pos.x = x;
        character.transform.position = pos;
    }

}