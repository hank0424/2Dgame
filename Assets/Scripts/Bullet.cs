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
        // 將子彈移動到設定的方向
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查碰到的物體是否擁有 "bullet" 的 tag
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