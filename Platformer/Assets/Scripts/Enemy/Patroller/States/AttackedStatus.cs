

public abstract class AttackedStatus : MovePatrollerStatus//атакуемый
{
    HealthPoint _healthPoint;

    public AttackedStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings, RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
        _healthPoint = patroller.GetComponent<HealthPoint>();
    }

    public override void Enter()
    {
        base.Enter();
        _healthPoint.OnHealthChange += OnHealthChange;
    }

    public override void Exit()
    {
        base.Exit();
        _healthPoint.OnHealthChange += OnHealthChange;
    }


    private void OnHealthChange(int prHp, int newHp)
    {
        stateMachine.ChangeState(character.pursuing);
    }

}

