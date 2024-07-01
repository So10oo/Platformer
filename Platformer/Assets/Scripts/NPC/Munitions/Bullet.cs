using System.Collections;
using UnityEngine;

public class Bullet : ElementPool
{
    [SerializeField] float _lifeTime;
    [SerializeField] HealthPointCheck _hitCheck;

    Damaging dmaging = new Damaging(1);

    private void HitCheck(HealthPoint healthPoint)
    {
        StopCoroutine(_life);
        if (healthPoint != null)
            healthPoint.Value -= dmaging.Value;
        this.Release();
    }

    Coroutine _life;
    void OnEnable()
    {
        _hitCheck.EnterComponent += HitCheck;
        _life = StartCoroutine(Life());
    }

    private void OnDisable()
    {
        _hitCheck.EnterComponent -= HitCheck;
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(_lifeTime);
        this.Release();
    }

 

}
