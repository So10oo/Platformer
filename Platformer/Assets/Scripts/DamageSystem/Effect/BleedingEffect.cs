using System;
using System.Collections;
using UnityEngine;

public class BleedingEffect : IHealthEffect
{
    IHealth health;
    float timeEffect;
    float timeTick;

    public BleedingEffect(float timeEffect, float timeTick)
    {
        this.timeEffect = timeEffect;
        this.timeTick = timeTick;
    }
     
    public void SetEffect(IHealth health)
    {
        this.health = health;
        ((MonoBehaviour)(health)).StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        float time = 0;
        var tick = new WaitForSeconds(timeTick);

        while (time <= timeEffect)
        {
            health.TakeDamage(1);
            yield return tick;
            time += timeTick;
        }
    }
}

