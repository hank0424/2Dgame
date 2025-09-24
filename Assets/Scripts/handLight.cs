using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handLight : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool handLT;
    public  GameObject LT;
    public static bool act = false;
    // Update is called once per frame
    void Start()
    {
        handLT = false;
    }
  
    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            LT.transform.rotation = Quaternion.Euler(0,0,-92.273f);

         
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            LT.transform.rotation = Quaternion.Euler(0, 180, -92.273f);
               
        }
      

        if (handLT==false&&Input.GetKeyDown(KeyCode.R)&&act==true)
            {
            print("open");
            handLT = true;
            LTAC();
        }
        else if (handLT ==true &&Input.GetKeyDown(KeyCode.R) && act == true)
        {
            print("closed");
            handLT = false;
            LTAC();
        }
        void LTAC()
        {
            if (handLT == true)
            {
                print("actT");
                LT.SetActive(true);
                
            }
            else if (handLT == false)
            {
                print("actF");
                LT.SetActive(false);
            }
        }
       
    }
}
