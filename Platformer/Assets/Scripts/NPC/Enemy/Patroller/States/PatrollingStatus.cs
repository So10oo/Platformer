using UnityEngine;

public class PatrollingStatus : AttackedStatus
{
    bool _isLeftDirectionMovement;

    public PatrollingStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings,RotateView rotateView) : base(patroller, stateMachine, patrollerSettings, rotateView)
    {
         
    }

    public override void Enter()
    {
        base.Enter();
        _this.Eyes.isVisibleChange += VisibleChange;
        _this.Eyes.SetViewingDate(5, Mathf.PI / 4);
        _isLeftDirectionMovement = _this.transform.localScale.x < 0 ? true : false;
        _this.animator.SetFloat("MovingBlend", 0.5f);
        _this.speechWindow.text = "Патрулирование";
    }

    public override void Exit()
    {
        base.Exit();
        _this.Eyes.isVisibleChange -= VisibleChange;
    }

    void VisibleChange(bool isVisible)
    {
        if (isVisible)
            stateMachine.ChangeState(_this.detectingStatue);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!_this.GroundArea.Value)
            _isLeftDirectionMovement = !_isLeftDirectionMovement;

        float dir = _isLeftDirectionMovement ? -1 : 1;
        var x = _this.transform.position.x + dir * patrollerSettings.patrollingSpeed * Time.fixedDeltaTime;
        SetAndRotateX(x);
    }


}

 