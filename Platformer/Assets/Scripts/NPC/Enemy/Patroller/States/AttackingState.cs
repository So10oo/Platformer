public class AttackingState : AttackedStatus
{
    public AttackingState(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
    }
}

