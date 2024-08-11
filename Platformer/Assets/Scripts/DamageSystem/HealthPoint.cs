using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    [SerializeField] int _maxValue;

    public event Action OnDeath;
    public event Action<int, int> OnHealthChange;

    bool _isDead;
    int _value;

    private void Start()
    {
        ResetHealthPoint();
    }

    public void ResetHealthPoint()
    {
        _value = _maxValue;
        _isDead = false;
    }

    public int MaxValue => _maxValue;
    public int Value
    {
        get => _value;
        set
        {
            var newHp = Math.Clamp(value, 0, _maxValue);
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

