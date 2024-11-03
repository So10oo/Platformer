using UnityEngine;

public class RepulsiveEffect : IHealthEffect
{
    Vector2 _force;
    Transform _drummer;

    public RepulsiveEffect(Vector2 force,Transform drummer)
    {
        this._force = force;
        this._drummer = drummer;
    }

    public void SetEffect(IHealth health)
    {
        if (health is MonoBehaviour mono && mono.GetComponent<Rigidbody2D>() is Rigidbody2D rb)
        {
            var dir = Mathf.Sign(rb.position.x - _drummer.position.x);
            rb.AddForce(new Vector2(dir, 1) * _force, ForceMode2D.Impulse);
        }
    }
}

