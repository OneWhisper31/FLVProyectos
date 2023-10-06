using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raylight : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rb;

    public void Bounce()
    {
        //transform.up = -transform.up;
        //rb.velocity = transform.up * speed;
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       //rb.velocity *= speed;
        
        //transform.up = -transform.up;
    }
}
