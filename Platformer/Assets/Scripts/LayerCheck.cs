using System;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] LayerMask Layer;

    Collider2D colliderCheck;
    ContactFilter2D contactFilter;
    private void Awake()
    {
        colliderCheck = GetComponent<Collider2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(Layer);
    }

    public event Action<bool> ValueChandge;
    bool _value;
    public bool Value
    {
        get
        {
            return _value;
        }
        private set
        {
            if (_value != value)
            {
                _value = value;
                ValueChandge?.Invoke(value);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((Layer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Value = true;
        }
    }

    Collider2D[] result = new Collider2D[1];

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((Layer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Value = colliderCheck.OverlapCollider(contactFilter, result) > 0;
        }
    }
}
