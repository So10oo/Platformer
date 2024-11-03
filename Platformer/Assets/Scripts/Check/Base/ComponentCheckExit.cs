using System;
using UnityEngine;

namespace CustomCheck
{
    public abstract class ComponentCheckExit<T> : MonoBehaviour, IComponentCheckExit<T>
    {
        Action<T> IComponentCheckExit<T>.ExitComponent { get; set; }

        private void OnTriggerExit2D(Collider2D collision)
        {
            (this as IComponentCheckExit<T>).ExitHandler(collision);
        }
    }
}