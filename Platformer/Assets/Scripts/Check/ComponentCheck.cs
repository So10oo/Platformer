using System;
using UnityEngine;

public class ComponentCheck<T> : MonoBehaviour where T : Component
{
    public Action<T> EnterComponent;

    public Action<T> ExitComponent;
    
    private void OnTriggerEnter2D(Collider2D collision) => EnterHundler(collision);

    private void OnTriggerExit2D(Collider2D collision) => ExitHundler(collision);

    protected virtual void EnterHundler(Collider2D collision)
    {
        T component = collision.gameObject.GetComponent<T>();
        if (component!=null)
            EnterComponent?.Invoke(component);
         
    }

    protected virtual void ExitHundler(Collider2D collision)
    {
        T component = collision.gameObject.GetComponent<T>();
        if (component!=null)
            ExitComponent?.Invoke(component);
    }
}

