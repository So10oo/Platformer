using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public event Action OnAttack;
    public event Action OnUpdate;

    public virtual void Attack()
    {
        OnAttack?.Invoke();
    }

    public virtual void OnStart()
    {
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }
}
