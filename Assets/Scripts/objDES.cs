using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objDES : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查碰到的物體是否擁有 "bullet" 的 tag
        if (collision.gameObject.CompareTag("bullet"))
        {
           
            // 每次碰到時減少 hp
            hp--;

            // 如果 hp 為正，播放動畫       
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
