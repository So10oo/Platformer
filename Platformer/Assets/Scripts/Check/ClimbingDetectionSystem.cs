using System.Collections;
using UnityEngine;

public class ClimbingDetectionSystem : MonoBehaviour
{
    [SerializeField] LayerMask _groundCheckLayerMask;
    [SerializeField] float _distance;
  
    RaycastHit2D _hit;
    public RaycastHit2D hit => _hit;

    private void Start()
    {
        StartCoroutine(Reycast());
    }

    IEnumerator Reycast()
    {
        var time = /*new WaitForFixedUpdate();*/ new WaitForSeconds(0.1f);
        while (true)
        {
            yield return time;
            var origin = transform.position;
            UpadateRaycastHit(origin, Vector2.down, _distance);
        }
    }

    private void UpadateRaycastHit(Vector2 origin, Vector2 direction, float distance)
    {
        var hit = Physics2D.Raycast(origin, direction, distance, _groundCheckLayerMask);
        if (hit.point != origin)
            this._hit = hit;
        else
            this._hit = default;
    }

    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.1f);

        if (_hit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_hit.point, 0.1f);
        }

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * _distance);
    }
    #endregion
}

