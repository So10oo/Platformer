using System.Collections;
using UnityEngine;

public class ClimbingDetectionSystem : MonoBehaviour
{
    [SerializeField] LayerMask _groundCheckLayerMask;
    [SerializeField] float _distance;
  
    public RaycastHit2D hit;

    private void Start()
    {
        StartCoroutine(Reycast());
    }

    IEnumerator Reycast()
    {
        var time = new WaitForSeconds(0.1f);
        while (true)
        {
            yield return time;
            var origin = transform.position;
            UpadateRaycastHit(origin, Vector2.down, _distance);
        }
    }

    private void UpadateRaycastHit(Vector2 origin, Vector2 direction, float distance)
    {
        RaycastHit2D raycastHit = new RaycastHit2D()
        {
            point = origin + direction * distance
        };

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance, _groundCheckLayerMask);
        var hit = Physics2D.Raycast(origin, direction, distance, _groundCheckLayerMask);
        if (hit.point != origin)
            this.hit = hit;
        else
            this.hit = default;
        
    }

    #region Debug
    //Vector2 origin;
    private void OnDrawGizmos()
    {
        Debug.Log("OnDrawGizmos");
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        
        if (hit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hit.point, 0.1f);
        }

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * _distance);
    }
    #endregion
}

