using System;

public interface IDeathEvent
{
    public bool isDead { get; }

    public event Action OnDeath;
}

