

using UnityEngine;

public class BasePatrollingState : State<Patroller>
{
    protected PatrollerSettings patrollerSettings;
    protected SpeechWindow speechWindow;
    protected EnemyEyes eyes;

    protected Vector3 position => character.transform.position;

    public BasePatrollingState(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings) : base(patroller, stateMachine)
    {
        this.patrollerSettings = patrollerSettings;
        speechWindow = character.speechWindow;
        eyes = character.Eyes;
    }

   
}


