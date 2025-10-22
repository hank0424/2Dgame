using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SHOP_UI : MonoBehaviour
{
    
    
    public GameObject ShopUI;
    
    private void Start()
    {
        ShopUI.SetActive(false);
        Cursor.visible = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShopUI.SetActive(true);
            Cursor.visible = true;
        }
    }
    public void exit()
    {
        {
            ShopUI.SetActive(false);
            Cursor.visible = false;
        }
    }
    public void buy()
    {
       
    }

}
