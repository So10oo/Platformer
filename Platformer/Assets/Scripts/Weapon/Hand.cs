using UnityEngine;


public class Hand : CoolDownWeapon
{
    [SerializeField] HealthPointCheck _hitCheck;

    Damaging dmaging = new Damaging(1);

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

    private void HitCheck(HealthPoint healthPoint)
    {
        if (healthPoint != null)
            healthPoint.Value -= dmaging.Value;
    }

    private void OnDestroy()
    {
        if (_hitCheck!=null)
            _hitCheck.EnterComponent-= HitCheck;
    }

}

