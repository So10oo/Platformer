using UnityEngine;
using Zenject;

public class FactoryWithDiContainer : IFactory
{
    DiContainer _diContainer;

    public FactoryWithDiContainer(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public T Create<T>(T gameObject, Vector2 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
    {
        return _diContainer.InstantiatePrefabForComponent<T>(gameObject, position, rotation, parent);
    }
}

