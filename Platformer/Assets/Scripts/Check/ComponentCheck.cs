using System;
using UnityEngine;

public class ComponentCheck<T> : MonoBehaviour where T : Component
{
    public event Action<T> EnterComponent;

    public event Action<T> ExitComponent;
    
    private void OnTriggerEnter2D(Collider2D collision) => EnterHandler(collision);

    private void OnTriggerExit2D(Collider2D collision) => ExitHandler(collision);

    protected virtual void EnterHandler(Collider2D collision)
    {
        T component = collision.gameObject.GetComponent<T>();
        if (component!=null)
            EnterComponent?.Invoke(component);
    }

    protected virtual void ExitHandler(Collider2D collision)
    {
        T component = collision.gameObject.GetComponent<T>();
        if (component!=null)
            ExitComponent?.Invoke(component);
    }
}

