using System;
using UnityEngine;

namespace CustomCheck
{
    public interface IComponentCheckEnter<T>
    {
        protected Action<T> EnterComponent { get; set; }

        public void EnterHandler(Collider2D collision)
        {
            T component = collision.gameObject.GetComponent<T>();
            if (component != null)
                EnterComponent?.Invoke(component);
        }

        public void SubscribeEnter(Action<T> action) => EnterComponent += action;
        public void UnsubscribeEnter(Action<T> action) => EnterComponent -= action;

    }

    public static class IComponentCheckEnterExtensions
    {
        public static void SubscribeEnter<T>(this IComponentCheckEnter<T> componentCheckEnter, Action<T> action)
        {
            componentCheckEnter.SubscribeEnter(action);
        }

        public static void UnsubscribeEnter<T>(this IComponentCheckEnter<T> componentCheckEnter, Action<T> action)
        {
            componentCheckEnter.UnsubscribeEnter(action);
        }
    }
}
