using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Bullet1 : MonoBehaviour
{
    public float speed = 50f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
