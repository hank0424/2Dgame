using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handLight : MonoBehaviour
{
    // Start is called before the first frame update

    public  GameObject LT;
    
    // Update is called once per frame
    void Start()
    {
       
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
      

        
    }
}
