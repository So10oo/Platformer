using UnityEngine;

public class Staff : CoolDownWeapon
{
    [SerializeField] GameObject p;

    //Transform t;
    //[Inject]
    //void Construct(Character character)
    //{
    //    t = character.transform;
    //}

    //private void Update()
    //{
    //    Debug.Log(gameObject.transform.lossyScale);
    //}

    public override void Attack()
    {
        if (BeforeAttack())
            return;
        var ball = Instantiate(p, gameObject.transform.position, Quaternion.identity);
        ball.GetComponent<MagicBall>().Fire(gameObject.transform.lossyScale.x > 0 ? 1 : -1);
    }
}

