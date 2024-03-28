using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : CoolDownWeapon
{
    [SerializeField] float _attackTime;

    float _currentAttackTime;
    Collider2D bladeCollider; 
    public override void OnStart()
    {
        base.OnStart();
        bladeCollider = GetComponent<Collider2D>();
        bladeCollider.enabled = false;
    }

    public override void Attack()
    {
        if (BeforeAttack())
            return;
        StartCoroutine(Stroke());
    }

    IEnumerator Stroke()
    {
        bladeCollider.enabled = true;
        _currentAttackTime = 0;
        var start = gameObject.transform.eulerAngles;
        while (true) 
        {
            if (_currentAttackTime < _attackTime)
            {
                var d = gameObject.transform.lossyScale.x > 0 ? 1 : -1;
                var temp = start;
                temp.z = Mathf.Lerp(start.z, start.z - d * 60, _currentAttackTime / _attackTime);
                gameObject.transform.eulerAngles = temp;
                yield return new WaitForEndOfFrame();
                _currentAttackTime += Time.deltaTime;

            }
            else
                break;
        }

        var x = gameObject.transform.lossyScale.x > 0 ? -1 : 1;
        var a = gameObject.transform.eulerAngles;
        a.z = x * Mathf.Abs(15);
        gameObject.transform.eulerAngles = a;
        bladeCollider.enabled = false;
    }
}

