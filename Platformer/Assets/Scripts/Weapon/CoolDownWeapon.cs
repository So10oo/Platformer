using UnityEngine;

public abstract class CoolDownWeapon : Weapon
{
    [SerializeField] float _timeCooldown;

    public bool isCooldown { get; private set; }

    protected override void OnStartMonoBehaviour()
    {
        base.OnStartMonoBehaviour();
        OnDealingDamage += CooldownWeaponDealingDamage;
    }

    float _currentTimeCooldown;
    private void CooldownWeaponDealingDamage()
    {
        isCooldown = true;
        _currentTimeCooldown = 0;
    }

    protected override void OnUpdateMonoBehaviour()
    {
        base.OnUpdateMonoBehaviour();
        if (isCooldown)
        {
            _currentTimeCooldown += Time.deltaTime;
            if (_currentTimeCooldown > _timeCooldown)
                isCooldown = false;
        }
    }
 
    protected bool BeforeDealingDamage()
    {
        if (!isCooldown)
        {
            base.DealingDamage();
            return false;
        }
        else
            return true;
    }
}
