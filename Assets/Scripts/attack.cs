using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject chara;

    Vector3 charapos;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        charapos = chara.transform.position;
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.C)&& !Input.GetKey(KeyCode.LeftArrow))
        {

            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.gameObject.transform.position = new Vector3(charapos.x + 0.7f, charapos.y + 0.1f, charapos.z);

        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.C)&& !Input.GetKey(KeyCode.RightArrow))
        {

            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.gameObject.transform.position = new Vector3(charapos.x - 0.7f, charapos.y + 0.1f, charapos.z);
            ;

        }
      

    }
}




