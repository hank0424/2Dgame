using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor2 : MonoBehaviour
{
    public GameObject chara;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            chara.transform.position = new Vector3(103f,40.4f,0);
           
        }
    }
}
