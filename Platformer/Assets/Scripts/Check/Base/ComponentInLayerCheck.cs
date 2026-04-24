using System;
using UnityEngine;

namespace CustomCheck
{
    public class ComponentInLayerCheck<T> : ComponentCheck<T>, ILayerCheck
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

        void EnterHandler(Collider2D collision)
        {
            if ((_layerCheck.value & (1 << collision.gameObject.layer)) != 0)
            {
                CountCollision++;
                Debug.Log("ВЫФФ");
                (this as IComponentCheckEnter<T>).EnterHandler(collision);
            }
        }
    }
}
