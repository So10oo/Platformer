using UnityEngine;

public abstract class View<T> : MonoBehaviour, IView<T>
{
    protected T _data;
    public abstract void ViewData(T data);
}

