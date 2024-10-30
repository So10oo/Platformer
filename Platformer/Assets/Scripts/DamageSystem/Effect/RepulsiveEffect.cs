using UnityEngine;

public class RepulsiveEffect : IHealthEffect
{
    float _force;
    Transform _drummer;

    public RepulsiveEffect(float force,Transform drummer)
    {
        this._force = force;
        this._drummer = drummer;
    }

    public void SetEffect(IHealth health)
    {
        if (health is MonoBehaviour mono && mono.GetComponent<Rigidbody2D>() is Rigidbody2D rb)
        {
            var dir = Mathf.Sign(rb.position.x - _drummer.position.x);
            rb.AddForce(dir * new Vector2(_force, 0), ForceMode2D.Impulse);
        }
    }
}

