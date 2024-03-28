using System;
using UnityEngine;

public abstract class Check : MonoBehaviour
{
    public abstract bool CheckA(Collider2D collision);//есть ли в проверяемом колайдере данный колойдер с нужными свойствами.

    public abstract void CheckB();//есть ли в данный момент колайдер с нужными нам свойствами 

    public event Action<bool> ValueChandge;
    bool _value;
    public bool Value
    {
        get
        {
            return _value;
        }
        private set
        {
            if (_value != value)
            {
                _value = value;
                ValueChandge?.Invoke(value);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckA(collision))
            Value = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckA(collision))
            CheckB();

    }
}

