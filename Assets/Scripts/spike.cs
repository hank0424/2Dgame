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

        // �˴��O�_�I��a��
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
