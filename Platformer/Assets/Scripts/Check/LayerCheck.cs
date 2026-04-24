using CustomCheck;
using System;
using UnityEngine;

public class LayerCheck : MonoBehaviour , ILayerCheck
{
    [SerializeField] LayerMask _layerCheck;

    public int CountCollision
    {
        get { return _countCollision; }
        protected set
        {
            if (_countCollision != value)
            {
                _countCollision = value;
                InLayer = value != 0;
                CountCollisionChange?.Invoke(_countCollision);
            }
        }
    }
    public event Action<int> CountCollisionChange;
    int _countCollision;

    public bool InLayer
    {
        get
        {
            return _inLayer;
        }
        protected set
        {
            if (_inLayer != value)
            {
                _inLayer = value;
                InLayerChange?.Invoke(value);
            }
        }
    }
    public event Action<bool> InLayerChange;
    bool _inLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_layerCheck.value & (1 << collision.gameObject.layer)) != 0)
            CountCollision++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_layerCheck.value & (1 << collision.gameObject.layer)) != 0)
            CountCollision--;
    }

}
