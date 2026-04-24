using CustomCheck;
using System.Collections;
using UnityEngine;

public class Bullet : ElementPool
{
    [SerializeField] float _lifeTime;
    [SerializeField] HealthPointCheck _hitCheck;
    [SerializeField] LayerCheck _layerCheck;

    Damaging _damaging = new Damaging(1);

    private void HitCheck(IHealth healthPoint)
    {
         
        if (healthPoint != null)
            healthPoint.TakeDamage(_damaging);
        StopCoroutine(_life);
        this.Release();
    }
    private void GroundedCollision(bool obj)
    {
        if (obj)
        {
            StopCoroutine(_life);
            this.Release();
        }
    }

    Coroutine _life;
    IEnumerator Life()
    {
        yield return new WaitForSeconds(_lifeTime);
        this.Release();
    }

    void OnEnable()
    {
        _layerCheck.InLayerChange += GroundedCollision;
        _hitCheck.SubscribeEnter(HitCheck);
        _life = StartCoroutine(Life());
    }

    private void OnDisable()
    {
        _layerCheck.InLayerChange -= GroundedCollision;
        _hitCheck.UnsubscribeEnter(HitCheck);
    }
 

 

}
