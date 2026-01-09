using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    private Animator a1;
    private Rigidbody2D rb;
    private bool explosion = false;

    // Start is called before the first frame update
    void Start()
    {
        a1 = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("enemy"))
        {
            explosion = true;
            a1.SetTrigger("boom");
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
            Destroy(this.gameObject, 1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(explosion==false)
        {
            Invoke("explosed",2f);
        }
    }
    void explosed()
    {
        a1.SetTrigger("boom");
        Destroy(this.gameObject, 1f);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;
    }
}
