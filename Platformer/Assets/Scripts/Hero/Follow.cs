using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Follow : MonoBehaviour
{
    GameObject objectToFollow;
    Rigidbody2D rb;

    [Inject]
    void Construct(Character hero)
    {
        objectToFollow = hero.gameObject;
        rb = hero.GetComponent<Rigidbody2D>();  
    }

    [SerializeField] float speed = 2.0f;
    [SerializeField] float offsetX = 0f;
    [SerializeField] float offsetY = 0f;

    void LateUpdate()
    {
        //var normalized = (Vector3)rb.velocity.normalized * 5;
        //var follow = objectToFollow.transform.position;
        //var a = normalized + follow;
        //transform.position = Vector3.Lerp(transform.position, a, rb.velocity.magnitude * Time.deltaTime);


        float interpolation = speed * Time.deltaTime;
        Vector3 position = this.transform.position;
        var follow = objectToFollow.transform.position + new Vector3(offsetX, offsetY);
        position.y = Mathf.Lerp(this.transform.position.y, follow.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, follow.x, interpolation);
        this.transform.position = position;
    }
}
