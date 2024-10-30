using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public event Action OnDealingDamage;

    public virtual void DealingDamage() => OnDealingDamage?.Invoke();

    protected virtual void OnStartMonoBehaviour() { }

    protected virtual void OnUpdateMonoBehaviour() { }

    private void Start() => OnStartMonoBehaviour();

    private void Update() => OnUpdateMonoBehaviour();
    
}
