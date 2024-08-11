using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyEyes : MonoBehaviour
{
    float _viewingRange;
    float _viewingAngle;

    [SerializeField] LayerMask _trackedLayers;

    public Action<bool> isVisibleChange;
    bool _isVisible;
    public bool isVisible
    {
        get 
        { 
            return _isVisible; 
        }
        private set 
        {
            if (_isVisible == value) return;

            _isVisible = value;
            isVisibleChange?.Invoke(value);
        }
    }

    Transform _targetTransform;
    Rigidbody2D _targetRigidbody;
    ContactFilter2D _contactFilter;
    public Vector3 targetPosition => _targetTransform.position;

    [Inject]
    void Construct(Character character)
    {
        _targetTransform = character.transform;
        _targetRigidbody = character.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_trackedLayers);
    }

    private void Start()
    {
        StartCoroutine(Look());
    }

    IEnumerator Look()
    {
        var time = new WaitForSeconds(0.1f);

        while (true)
        {
            var raycastHit = new RaycastHit2D[1];

            Vector2 dir = new Vector2(transform.parent.transform.localScale.x, 0);

            Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(_viewingAngle), Mathf.Sin(_viewingAngle)) * dir.x, Color.green, 0.1f);
            Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(_viewingAngle), -Mathf.Sin(_viewingAngle)) * dir.x, Color.green, 0.1f);
            Debug.DrawRay(transform.position, (targetPosition - transform.position).normalized * _viewingRange, Color.red, 0.1f);

            if (Vector2.Angle(dir,targetPosition - transform.position)<_viewingAngle*Mathf.Rad2Deg)
            {
                Physics2D.Raycast(transform.position, (targetPosition - transform.position).normalized, _contactFilter, raycastHit, _viewingRange);
            }

             
            isVisible = raycastHit[0].rigidbody == _targetRigidbody;
            yield return time;
        }

    }

    public void SetViewingDate(float distant)
    {
        _viewingRange = distant;
    }
    public void SetViewingDate(float distant, float angle)
    {
        _viewingRange = distant;
        _viewingAngle = angle;
    }

}

