using UnityEngine;

public class Hand : CoolDownWeapon
{
    [SerializeField] LayerMask _layerMask;

    Damaging _damaging;

    protected override void OnStartMonoBehaviour()
    {
        base.OnStartMonoBehaviour();
        _damaging = new Damaging(5, new RepulsiveEffect(new Vector2(15,10), transform.parent));
    }
    public override void DealingDamage()
    {
        if (BeforeDealingDamage())
            return;
        TakeDamage();
    }

    private void TakeDamage()
    {
        var collider = Physics2D.OverlapBox(transform.position, transform.localScale, 0, _layerMask);
        //collider?.attachedRigidbody.AddForce(new Vector2(7, 0), ForceMode2D.Impulse);
        collider?.gameObject.GetComponent<IHealth>()?.TakeDamage(_damaging);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}

