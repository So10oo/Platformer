using UnityEngine;

public abstract class ElementPool : MonoBehaviour
{
    Pool _pool;

    public void Init(Pool p)
    {
        _pool = p;
    }

    public void Destroy()
    {
        _pool?.Destroy(this);
    }

}

