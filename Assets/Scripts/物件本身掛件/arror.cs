using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arror : MonoBehaviour
{
    public float speed;
    public int dir;
    private Vector2 moveDirection;

    private void Start()
    {

        Destroy(this.gameObject, 3.0f);
     

        if (dir==1)
        {
            
            moveDirection = new Vector2(0, 1);
          
        }
        if (dir==2)
        {
            // 玩家在右邊，子彈向右飛
            moveDirection = new Vector2(0,-1);
           
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