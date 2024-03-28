using UnityEngine;

public abstract class CoolDownWeapon : Weapon
{
    [SerializeField] float _timeCoolDown;

    public bool isCoolDown { get; private set; }

    public override void OnStart()
    {
        base.OnStart();
        OnUpdate += CoolDownWeaponOnUpdate;
        OnAttack += CoolDownWeaponOnAttack;
    }

    float _currentTimeCoolDown;
    private void CoolDownWeaponOnAttack()
    {
        isCoolDown = true;
        _currentTimeCoolDown = 0;
    }

    private void CoolDownWeaponOnUpdate()
    {
        if (isCoolDown)
        {
            _currentTimeCoolDown += Time.deltaTime;
            if (_currentTimeCoolDown > _timeCoolDown)
                isCoolDown = false;
        }
    }

    public bool BeforeAttack()
    {
        if (!isCoolDown)
        {
            base.Attack();
            return false;
        }
        else
            return true;
    }
}
