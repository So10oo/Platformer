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
        character.Eyes.isVisibleChange += VisibleChange;
        character.Eyes.SetViewingDate(5, Mathf.PI / 4);
        _isLeftDirectionMovement = character.transform.localScale.x < 0 ? true : false;

        character.speechWindow.text = "и где же он...";
    }

    public override void Exit()
    {
        base.Exit();
        character.Eyes.isVisibleChange -= VisibleChange;
    }

    void VisibleChange(bool b)
    {
        if (b)
        {
            stateMachine.ChangeState(character.detectingStatue);
        }
    }

    public override void FixedUpdate()
    {
        base.LogicUpdate();
        if (!character.GroundArea.Value)
            _isLeftDirectionMovement = !_isLeftDirectionMovement;

        float dir = _isLeftDirectionMovement ? -1 : 1;
        var x = character.transform.position.x + dir * patrollerSettings.patrollingSpeed * Time.deltaTime;
        SetAndRotateX(x);
    }


}

 