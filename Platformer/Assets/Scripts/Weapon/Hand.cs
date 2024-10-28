using System.Collections.Generic;
using UnityEngine;


public class Hand : CoolDownWeapon
{
    [SerializeField] HealthPointCheck _hitCheck;

    Damaging _damaging = new Damaging(5/*, new List<IHealthEffect>() { new BleedingEffect(100, 0.1f) }*/);

    public Animator anim;

    public override void Attack()
    {
        if (BeforeAttack())
            return;
        anim.SetTrigger("Attack");
    }

    public override void OnStart()
    {
        base.OnStart();
        _hitCheck.EnterComponent += HitCheck;
    }

    private void HitCheck(IHealth healthPoint)
    {
        if (healthPoint != null)
            healthPoint.TakeDamage(_damaging);
    }

    private void OnDestroy()
    {
        if (_hitCheck!=null)
            _hitCheck.EnterComponent-= HitCheck;
    }

}

