using System;

namespace CustomCheck
{
    public interface ILayerCheck
    {
        public event Action<bool> InLayerChange;
        public bool InLayer { get; }

        public int CountCollision { get; }

        public event Action<int> CountCollisionChange;
    }
}