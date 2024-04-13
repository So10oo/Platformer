using System.Collections;
using System.Linq;
using UnityEngine;

public class DashState : LockableState
{
    float gravityScale;
    float _timeToEnter;
    float direction;
    float dashTime;

    public DashState(Character character, StateMachine<Character> stateMachine, IInputService inputService) : base(character, stateMachine, inputService)
    {
    }

    public override void Enter()
    {
        base.Enter();
        gravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        _timeToEnter = 0;
        direction = character.gameObject.transform.localScale.x;
        dashTime = physicsSettings.dashSpeedCurve.keys.Last().time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timeToEnter += Time.deltaTime;
        if (_timeToEnter >= dashTime)
        {
            stateMachine.ChangeState(character["freeFall"]);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var currentDashSpeed = direction * physicsSettings.dashSpeedCurve.Evaluate(_timeToEnter);
        rb.velocity = new Vector2(currentDashSpeed, 0);

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
            if (timeEnter > physicsSettings.delayedDash)
                timeEnd = true;
            if (character.IsGround)
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

