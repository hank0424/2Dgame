using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float deletTime = 3f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, deletTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
