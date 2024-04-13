using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<int, int> OnHealthChange;

    [SerializeField] int _maxValue;

    bool _isDead;
    int _value;

    private void Start()
    {
        _value = _maxValue;
    }

    public int MaxValue => _maxValue;
    public int Value
    {
        get => _value;
        set
        {
            var newHp = Math.Clamp(value, 0, _maxValue);
            //if (_hp == newHp)
            //    return;
            OnHealthChange?.Invoke(_value, newHp);
            _value = newHp;
            if (_value == 0 && !_isDead)
            {
                OnDeath?.Invoke();
                _isDead = true;
            }
        }
    }
}

