using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en : MonoBehaviour
{
    public int hp=5;
    void Update()
    {
        if(hp<=0)
        {
            Destroy(this.gameObject);
            boss.hp-=5;
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("atk"))
        {

            hp -= Chara2.atk;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ˬd�I�쪺����O�_�֦� "bullet" �� tag
        if (collision.gameObject.CompareTag("bullet"))
        {

            hp -= Chara2.magic;


        }
    }
}
