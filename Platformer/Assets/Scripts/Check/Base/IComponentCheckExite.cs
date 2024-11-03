using System;
using UnityEngine;

namespace CustomCheck
{
    public interface IComponentCheckExit<T> 
    {
        protected abstract Action<T> ExitComponent {  get; set; }

        private void OnTriggerExit2D(Collider2D collision) => ExitHandler(collision);

         void ExitHandler(Collider2D collision)
        {
            T component = collision.gameObject.GetComponent<T>();
            if (component != null)
                ExitComponent?.Invoke(component);
        }

        public void SubscribeExit(Action<T> action) => ExitComponent += action;
        public void UnsubscribeExit(Action<T> action) => ExitComponent -= action;
    }

    public static class IComponentCheckExitExtensions
    {
        public static void SubscribeExit<T>(this IComponentCheckExit<T> componentCheckExit, Action<T> action)
        {
            componentCheckExit.SubscribeExit(action);
        }

        public static void UnsubscribeExit<T>(this IComponentCheckExit<T> componentCheckExit, Action<T> action)
        {
            componentCheckExit.UnsubscribeExit(action);
        }
    }
}
