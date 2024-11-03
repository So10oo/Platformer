using CustomCheck;
using System.Collections;
using UnityEngine;

public class Bullet : ElementPool
{
    [SerializeField] float _lifeTime;
    [SerializeField] HealthPointCheck _hitCheck;

    Damaging _damaging = new Damaging(1);

    private void HitCheck(IHealth healthPoint)
    {
        StopCoroutine(_life);
        if (healthPoint != null)
            healthPoint.TakeDamage(_damaging);
        this.Release();
    }

    Coroutine _life;
    void OnEnable()
    {
        //_hitCheck.EnterComponent += HitCheck;
        _hitCheck.SubscribeEnter(HitCheck);
        _life = StartCoroutine(Life());
    }

    private void OnDisable()
    {
        _hitCheck.UnsubscribeEnter(HitCheck);
        //_hitCheck.EnterComponent -= HitCheck;
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(_lifeTime);
        this.Release();
    }

 

}
