using UnityEngine;

public class Hand : CoolDownWeapon
{
    //[SerializeField] HealthPointCheck _hitCheck;
    //[SerializeField] Animator _animator;

    [SerializeField] LayerMask _layerMask;

    [SerializeField] Transform _handArea;

    Damaging _damaging = new Damaging(5/*, new List<IHealthEffect>() { new BleedingEffect(5f, 1f) }*/);

    public override void DealingDamage()
    {
        if (BeforeDealingDamage())
            return;
        //_animator.SetTrigger("Attack");
        TakeDamage();
    }

    private void TakeDamage()
    {
        var collider = Physics2D.OverlapBox(_handArea.position, _handArea.localScale, 0, _layerMask);
        collider?.gameObject.GetComponent<IHealth>()?.TakeDamage(_damaging);
    }

}

