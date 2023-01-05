using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void RemoveVelocity()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            RemoveVelocity();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ruby")
        {
            CurrencyController.CreditBalance(1);
            Destroy(col.gameObject);
        }
    }
}
