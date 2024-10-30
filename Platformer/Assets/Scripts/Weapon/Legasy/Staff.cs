using UnityEngine;

public class Staff : CoolDownWeapon
{
    [SerializeField] GameObject p;

    public override void DealingDamage()
    {
        if (BeforeDealingDamage())
            return;
        var ball = Instantiate(p, gameObject.transform.position, Quaternion.identity);
        ball.GetComponent<MagicBall>().Fire(gameObject.transform.lossyScale.x > 0 ? 1 : -1);
    }
}

