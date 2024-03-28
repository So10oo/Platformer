using System;
using System.Collections;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _timeDestroy;

    float _time;

    public void Fire(float direction)
    {
        StartCoroutine(ObjectFlight(direction));
    }

    IEnumerator ObjectFlight(float direction)
    {
        _time = 0;
        while (_timeDestroy > _time) 
        {
            yield return new WaitForEndOfFrame();
            _time += Time.deltaTime;
            var p = gameObject.transform.position;
            p.x += direction * _speed * Time.deltaTime;
            gameObject.transform.position = p;
        }
        Destroy(gameObject);
    }

}
