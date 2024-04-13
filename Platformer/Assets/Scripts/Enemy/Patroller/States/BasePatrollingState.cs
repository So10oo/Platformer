public class BasePatrollingState : State<Patroller>
{
    protected PatrollerSettings patrollerSettings;
    public BasePatrollingState(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings) : base(patroller, stateMachine)
    {
        this.patrollerSettings = patrollerSettings;
    }
}


