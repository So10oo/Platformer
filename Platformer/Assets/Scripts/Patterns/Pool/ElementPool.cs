using UnityEngine;

public abstract class ElementPool : MonoBehaviour
{
    Pool _pool;

    public void Init(Pool p)
    {
        _pool = p;
    }

    public void Release()
    {
        _pool?.Release(this);
    }


    private void OnEnable()
    {
        OnGetInPool();
    }

    private void OnDisable()
    {
        OnReleaseInPool();
    }

    protected virtual void OnGetInPool()
    {

    }

    protected virtual void OnReleaseInPool()
    {

    }

}

