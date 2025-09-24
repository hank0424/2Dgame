using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AREA : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject area1Object;
  
    private void OnTriggerStay2D(Collider2D collision)
    {
         Collider2D[] collidersInArea = Physics2D.OverlapCircleAll(transform.position, 10); // �O�o����yourRadius���A��Ĳ�o�b�|

        // �ˬd�C��Collider2D�����ҡA�p�G�����Ҭ�"enemy"�A�h�����AREA 1������
        foreach (Collider2D collider in collidersInArea)
        {
            if (collider.CompareTag("enemy"))
            {
                area1Object.SetActive(false);
                return; // �@���o�{��"enemy"�A�N�i�H���e�����j��M���
           
            }
        }

        // �p�G�j��M�����Ҧ�Collider2D���S��"enemy"�A�h���AREA 1������
        area1Object.SetActive(true);
    }
}

