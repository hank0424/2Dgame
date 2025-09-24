using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        // 檢測是否碰到地面
        if (collision.collider.CompareTag("Player"))
        {
            health.HP =0;
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
