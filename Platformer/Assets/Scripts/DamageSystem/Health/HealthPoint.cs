using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IHealth
{
    [SerializeField] int _maxValue;

    public event Action OnDeath;
    public event Action<int, int> OnHealthChange;

    bool _isDead;
    int _currentValue;

    public int MaxValue => _maxValue;
    public int CurrentValue
    {
        get => _currentValue;
        set
        {
            var newHp = Math.Clamp(value, 0, _maxValue);
            OnHealthChange?.Invoke(_currentValue, newHp);
            _currentValue = newHp;
            if (_currentValue == 0 && !_isDead)
            {
                OnDeath?.Invoke();
                _isDead = true;
            }
        }
    }
    public bool isDead => _isDead;

    private void Start() => ResetHealthPoint();
    
    public void ResetHealthPoint()
    {
        _currentValue = _maxValue;
        _isDead = false;
    }

    public void TakeDamage(int damage) => CurrentValue -= damage;
    
}

