using System.Collections;
using System.Linq;
using UnityEngine;

public class DashState : LockableState
{
    RotateView rotateView;
    float gravityScale;
    float _timeToEnter;
    float direction;
    float dashTime;

    public DashState(Character character, StateMachine<Character> stateMachine, InputService inputService) : base(character, stateMachine, inputService)
    {
        rotateView = character.rotateView;
    }

    public override void Enter()
    {
        base.Enter();
        gravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        _timeToEnter = 0;
        rb.SetVelocityY(0);
        direction = rotateView.GetRotate();//character.gameObject.transform.localScale.x;
        dashTime = settings.dashSpeedCurve.keys.Last().time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;
        if (_timeToEnter >= dashTime)
        {
            ChangeState(character["freeFall"]);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var currentDashSpeed = direction * settings.dashSpeedCurve.Evaluate(_timeToEnter);
        rb.SetVelocityX(currentDashSpeed);
    }

    IEnumerator CoolDownDash()
    {
        bool timeEnd = false;
        bool ground = false;
        float timeEnter = 0;
        _lockState = true;
        while (true)
        {
            yield return null; 
            timeEnter += Time.deltaTime;
            if (timeEnter > settings.delayedDash)
                timeEnd = true;
            if (character.isGround)
                ground = true;
            if (ground && timeEnd)
            {
                _lockState = false;
                break;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = gravityScale;
        character.StartCoroutine(CoolDownDash());
    }
}

