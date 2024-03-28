using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : ElementPool
{
    [SerializeField] float _lifeTime;
    [SerializeField] LayerCheck _hitCheck;


    private void HitCheck(bool value)
    {
        if (value)
        {
            StopCoroutine(_life);
            this.Destroy();
        } 
    }

    Coroutine _life;
    void OnEnable()
    {
        _hitCheck.ValueChandge += HitCheck;
        _life = StartCoroutine(Life());
    }

    private void OnDisable()
    {
        _hitCheck.ValueChandge -= HitCheck;
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(_lifeTime);
        this.Destroy();
    }

  
}
