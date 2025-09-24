using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC : MonoBehaviour
{
       public GameObject text;
       public GameObject button;
    public GameObject ui;
    private bool isObjectHidden;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            if (isObjectHidden)
            {
                ShowObject();
                print("op");
            }
            else
            {
                HideObject();
                print("cl");
            }
        }
    }

    void HideObject()
    {
       
        text.SetActive(false);
        ui.SetActive(false);
        button.SetActive(false);
        isObjectHidden = true;
    }

    void ShowObject()
    {
      
       text.SetActive(true);
        ui.SetActive(true);
        button.SetActive(true);
        isObjectHidden = false;
    }
}

