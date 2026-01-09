using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_stone_unlock : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public static int num=0;
    // Start is called before the first frame update
    void Start()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        button5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (num)
        {
            case 1:
                button1.SetActive(true);
                break;
            case 2:
                button2.SetActive(true);
                button3.SetActive(true);
                break;
           
              
            case 4:
                button4.SetActive(true);
                break;
            case 5:
                button5.SetActive(true);
                break;
        }
            
     }
    }

