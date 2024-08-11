using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float force;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jump(collision);
    }
    void Jump(Collider2D collision)
    {
        if (collision.attachedRigidbody is Rigidbody2D rb)
        {
            rb.SetVelocityY(0);
            Vector2 vector = (collision.transform.position - transform.position);
            vector.x /= 5;
            vector = vector.normalized * force;
            rb.AddForce(vector, ForceMode2D.Impulse);
        }
    }
}
