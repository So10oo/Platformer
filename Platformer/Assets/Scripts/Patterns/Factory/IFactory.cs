using UnityEngine;

public interface IFactory<T>
{
    T Create(T gameObject);
}
