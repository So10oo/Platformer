using UnityEngine;

public abstract class View<T> : MonoBehaviour
{
    protected T _data;
    public abstract void ViewData(T data);
}

