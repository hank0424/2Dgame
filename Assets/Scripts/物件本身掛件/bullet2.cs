using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Vector2 moveDirection;

    private void Start()
    {

        Destroy(this.gameObject, 3.0f);
        player = GameObject.FindWithTag("Player");
       
            if (player.transform.position.x < transform.position.x)
            {
                // �������߅���ӏ������w
                moveDirection = new Vector2(-1, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
            if (player.transform.position.x >transform.position.x)
            {
                // �������߅���ӏ������w
                moveDirection = new Vector2(1, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }

          
        
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
