using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AREA : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject area1Object;
  
    private void OnTriggerStay2D(Collider2D collision)
    {
         Collider2D[] collidersInArea = Physics2D.OverlapCircleAll(transform.position, 10); // 記得替換yourRadius為你的觸發半徑

        // 檢查每個Collider2D的標籤，如果有標籤為"enemy"，則不顯示AREA 1的物件
        foreach (Collider2D collider in collidersInArea)
        {
            if (collider.CompareTag("enemy"))
            {
                area1Object.SetActive(false);
                return; // 一旦發現有"enemy"，就可以提前結束迴圈和函數
           
            }
        }

        // 如果迴圈遍歷完所有Collider2D都沒有"enemy"，則顯示AREA 1的物件
        area1Object.SetActive(true);
    }
}

