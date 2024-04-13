using System.Collections;
using Unity.VisualScripting;
using UnityEngine; 

public class PatrollingStatus : BasePatrollingState
{
    public PatrollingStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings) : base(patroller, stateMachine, patrollerSettings)
    {
    }

    Coroutine _takeLook;

    public override void Enter()
    {
        base.Enter();
        _takeLook = character.StartCoroutine(TakeLook());
    }
     
    public override void Exit() 
    {
        base.Exit();
        character.StopCoroutine(_takeLook);
    }

    IEnumerator TakeLook()
    {
        var time = new WaitForSeconds(0.1f);
        while (true)
        {
            Debug.DrawRay(character.gameObject.transform.position, character.gameObject.transform.TransformDirection(Vector3.up) * 30f, Color.yellow , 0.1f);
            //Debug.DrawRay(character.gameObject.transform.position, character.gameObject.transform Vector3.forward * patrollerSettings.rangeVision, Color.yellow, 0.1f );
            //if (Physics2D.Raycast(character.gameObject.transform.position, Vector3.forward, patrollerSettings.rangeVision, patrollerSettings.detectionMask) is RaycastHit2D hit) 
            //{
            //    Debug.Log(hit);
            //}
            yield return time;
        } 
    }

    bool _isLeft;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector2 targetpos;
        if (_isLeft)
            targetpos = new Vector2(character.SpawnPoint.x - patrollerSettings.patrollerRadios, character.SpawnPoint.y);
        else
            targetpos = new Vector2(character.SpawnPoint.x + patrollerSettings.patrollerRadios, character.SpawnPoint.y);

        character.transform.position = Vector3.MoveTowards(character.transform.position, targetpos, patrollerSettings.speed * Time.deltaTime);
        if (targetpos == (Vector2)character.transform.position)
            _isLeft = !_isLeft;
        
    }
}

