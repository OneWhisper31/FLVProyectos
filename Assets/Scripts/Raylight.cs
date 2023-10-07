using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raylight : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        SetVelocity();
        //StartCoroutine(DestroyRay()); se autodestruyen cuando se quedan sin velocidad
    }
    public void Update()
    {
        if (transform.position.y > -350)
            Destroy(this.gameObject);
    }

    public IEnumerator DestroyRay()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    public void Bounce()
    {
        transform.up = (new Vector2(Random.Range(-1,1.1f)-transform.up.x, -transform.up.y)).normalized;
        SetVelocity();
    }
    public void SetVelocity()
    {
        rb.velocity = transform.up * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce();
    }
}
