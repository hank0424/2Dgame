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
        // �ˬd�I�쪺����O�_�֦� "bullet" �� tag
        if (collision.gameObject.CompareTag("bullet"))
        {
           
            // �C���I��ɴ�� hp
            hp--;

            // �p�G hp �����A����ʵe       
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
