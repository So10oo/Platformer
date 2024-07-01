using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    readonly List<ElementPool> _listReceived;

    readonly List<ElementPool> _listAllElements;

    readonly Func<ElementPool, ElementPool> _create;

    readonly ElementPool _objectPool;

    public Pool(ElementPool objectPool, Func<ElementPool, ElementPool> create = null)
    {
        if (create == null)
            _create = GameObject.Instantiate;
        else
            _create = create;

        _objectPool = objectPool;
        _listReceived = new List<ElementPool>();
        _listAllElements = new List<ElementPool>();
    }

    public ElementPool Get()
    {
        ElementPool val;
        if (_listReceived.Count == 0)
        {
            val = Create();
        }
        else
        {
            int index = _listReceived.Count - 1;
            val = _listReceived[index];
            _listReceived.RemoveAt(index);
        }
        val.gameObject.SetActive(true);
        return val;
    }

    public ElementPool Get(Vector3 position)
    {
        var elem = Get();
        elem.gameObject.transform.position = position;
        return elem;
    }

    public ElementPool Get(Vector3 position, Quaternion quaternion)
    {
        var elem = Get(position);
        elem.gameObject.transform.rotation = quaternion;
        return elem;
    }

    public ElementPool Get(Vector3 position, Quaternion quaternion, Transform parent, bool worldPositionStays = true)
    {
        var elem = Get(position, quaternion);
        elem.gameObject.transform.SetParent(parent, worldPositionStays);
        return elem;
    }

    public void Release(ElementPool obj)
    {
        if (obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            _listReceived.Add(obj);
        }
    }

    public void Destroy(ElementPool obj)
    {
        GameObject.Destroy(obj.gameObject);
    }

    public void ReleaseAll()
    {
        _listAllElements.ForEach(x => Release(x));
    }

    ElementPool Create()
    {
        var item = _create.Invoke(_objectPool);
        item.Init(this);
        _listAllElements.Add(item);
        return item;
    }

}
