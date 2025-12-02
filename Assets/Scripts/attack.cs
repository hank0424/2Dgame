using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject chara;
    private bool right=true;
    private bool left = false;
   
    Vector3 charapos;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            left = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            right = false;
            left = true;
        }
        charapos = chara.transform.position;
        if (Input.GetKeyDown(KeyCode.C)&&right==true)
        {

            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.gameObject.transform.position = new Vector3(charapos.x + 0.7f, charapos.y + 0.1f, charapos.z);

        }
        if (Input.GetKeyDown(KeyCode.C)&& right == false)
        {

            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.gameObject.transform.position = new Vector3(charapos.x - 0.7f, charapos.y + 0.1f, charapos.z);
            ;

        }
      

    }
}




