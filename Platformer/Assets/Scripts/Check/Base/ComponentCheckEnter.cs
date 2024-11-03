using System;
using UnityEngine;

namespace CustomCheck
{
    public abstract class ComponentCheckEnter<T> : MonoBehaviour, IComponentCheckEnter<T>
    {
        Action<T> IComponentCheckEnter<T>.EnterComponent { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            (this as IComponentCheckEnter<T>).EnterHandler(collision);
        }
    }
}
