using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void Update()
    {
        // �N�l�u���ʨ�]�w����V
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ˬd�I�쪺����O�_�֦� "bullet" �� tag
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("obj"))
        {
            Destroy(this.gameObject);
        }
    }
}