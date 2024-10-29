using System;
using System.Collections.Generic;

public interface IHealth : IDeathEvent
{
    public int MaxValue { get; }
    public int CurrentValue { get; }

    public event Action<int, int> OnHealthChange;

    public void TakeDamage(IDamaging damaging)
    {
        TakeDamage(damaging.Value);
        SetEffects(damaging.Effects);
    }

    public void TakeDamage(int damage);

    public void SetEffects(List<IHealthEffect> effects)
    {
        if (effects == null) return; 
        foreach (var effect in effects)
            effect.SetEffect(this);
    }
}

