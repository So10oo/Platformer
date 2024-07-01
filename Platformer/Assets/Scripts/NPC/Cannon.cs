using System.Collections;
using UnityEngine;
using Zenject;

public class Cannon : MonoBehaviour
{
    [SerializeField] Bullet _bullet;
    [SerializeField] LayerCheck _playerCheck;
    [SerializeField] float _flightTime;
    [SerializeField] float _gravityScale;


    Pool _pool;
    Transform _target;
    Rigidbody2D _targetRigidbody;
    [Inject]
    void Construct(Character character)
    {
        _target = character.gameObject.transform;
        _targetRigidbody = character.GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        _playerCheck.ValueChandge += PlayerInZoneValueChange;
        _pool = new Pool(_bullet);
    }

    private void PlayerInZoneValueChange(bool obj)
    {
        if (obj)
            StartCoroutine(Shooting—ycle());
        else
            StopAllCoroutines();
    }

    WaitForSeconds _timeShooting—ycleTime;
    IEnumerator Shooting—ycle()
    {
        _timeShooting—ycleTime = new WaitForSeconds(0.5f);
        while (true)
        {
            var gravity = _gravityScale * Physics2D.gravity.y;

            #region legasy
            //var dx = _target.position.x - transform.position.x;
            //var dy = _target.position.y - transform.position.y;
            //var polynomial = new Polynomial(
            //    gravity * gravity / 4,
            //    -_targetRigidbody.velocity.y * gravity,
            //    _targetRigidbody.velocity.magnitude - _velocityMagnitude - gravity * dy,
            //    2 * (_targetRigidbody.velocity.x * dx + _targetRigidbody.velocity.y * dy),
            //    dx * dx + dy * dy
            //    );
            //var t = polynomial.findRoot(a + error, b, error);
            #endregion

            var targetVelocityX = _targetRigidbody.velocity.x;//_velocityMarks.Sum() / _velocityMarks.Length; //_targetRigidbody.velocity.x;
            var targetVelocityY = 0;//_targetRigidbody.velocity.y;
            var t = _flightTime;
            var vx = (_target.position.x + targetVelocityX * t - transform.position.x) / t;
            var vy = (_target.position.y + targetVelocityY * t - transform.position.y - gravity * t * t / 2f) / t;

            var bullet = _pool.Get(transform.position);

            var bulletRigidbody = bullet.gameObject.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = new Vector2(vx, vy);
            bulletRigidbody.gravityScale = _gravityScale;

            yield return _timeShooting—ycleTime;
        }
    }
    public void SetSpeedShooting—ycle(float time)
    {
        _timeShooting—ycleTime = new WaitForSeconds(time);
    }
}
