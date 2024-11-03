using System;
using UnityEngine;

namespace Check
{
    public abstract class ComponentCheck<T> : MonoBehaviour, IComponentCheckEnter<T>, IComponentCheckExit<T>  //where T : Component
    {
        public abstract Action<T> ExitComponent { get; }
        public abstract Action<T> EnterComponent { get; }
    }
}


