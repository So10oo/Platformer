using System;
using UnityEngine;

public abstract class Check : MonoBehaviour
{
    protected abstract bool CheckObject(Collider2D collision);//обладает ли коллайдер нужными нам свойствами 
    protected abstract bool CheckAllObject();//есть ли в данный момент колайдер с нужными нам свойствами в нашем коллайдере 

    public event Action<bool> ValueChandge;
    bool _value;
    public bool Value
    {
        get
        {
            return _value;
        }
        protected set
        {
            if (_value != value)
            {
                _value = value;
                ValueChandge?.Invoke(value);
            }
        }
    }

    public bool isCheckEnter = true;
    public bool isCheckExit = true;

    protected virtual void EnterHundler(Collider2D collision)
    {
        if (!isCheckEnter)
            return;

        if (CheckObject(collision))
            Value = true;
    }

    protected virtual void ExitHundler(Collider2D collision)
    {
        if (!isCheckExit)
            return;

        if (CheckObject(collision))
            Value = CheckAllObject();
    }

    protected virtual void AwakeHundler()
    {

    }

    private void Awake() => AwakeHundler();

    private void OnTriggerEnter2D(Collider2D collision) => EnterHundler(collision);

    private void OnTriggerExit2D(Collider2D collision) => ExitHundler(collision);
    
}

