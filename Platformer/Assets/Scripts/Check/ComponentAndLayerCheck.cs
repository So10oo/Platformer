using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentAndLayerCheck<T> : LayerCheck where T : Component
{
    public Action<T> EnterComponent;

    public Action<T> ExitComponent;

    protected override bool CheckAllObject()
    {
        var listCollider = new List<Collider2D>();
        if (colliderCheck.OverlapCollider(contactFilter, listCollider) > 0)
        {
            foreach (Collider2D collider in listCollider)
                if (collider.gameObject.GetComponent<T>() != null)
                    return true;
        }
        return false;
    }

    protected bool CheckObject(Collider2D collision,ref T component)
    {
        if (base.CheckObject(collision))
        {
            component = collision.gameObject.GetComponent<T>();
            return true;
        }
        return false;
    }

    protected override void EnterHundler(Collider2D collision)
    {
        if (!isCheckEnter)
            return;

        T component = null;
        if (CheckObject(collision, ref component))
        {
            Value = true;
            EnterComponent?.Invoke(component);
        }
            
    }

    protected override void ExitHundler(Collider2D collision)
    {
        if (!isCheckExit)
            return;

        T component = null;
        if (CheckObject(collision, ref component))
        {
            Value = CheckAllObject();
            ExitComponent?.Invoke(component);
        }
             
    }
}

