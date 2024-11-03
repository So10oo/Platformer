using System;
using UnityEngine;

namespace CustomCheck
{
    public abstract class ComponentCheck<T> : MonoBehaviour, IComponentCheckEnter<T>, IComponentCheckExit<T>
    {
        Action<T> IComponentCheckEnter<T>.EnterComponent { get ; set ; }
        Action<T> IComponentCheckExit<T>.ExitComponent { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            (this as IComponentCheckEnter<T>).EnterHandler(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            (this as IComponentCheckExit<T>).ExitHandler(collision);
        }
    }

}
 

