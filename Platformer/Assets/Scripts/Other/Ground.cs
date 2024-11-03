using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name + " " + collision.rigidbody.velocity);
        collision.rigidbody.velocity = collision.rigidbody.velocity.SetY(0);
    }
}
