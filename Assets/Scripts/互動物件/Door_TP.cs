using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_TP : MonoBehaviour
{
    public GameObject player;
    public Vector3 choose;
    private Vector3 Bar_Door;
    private Vector3 Bar_DoorExit;
    private void Start()
    {
        Bar_Door=new Vector3(-20.1978f, 45.422f, 0);
        Bar_DoorExit=new Vector3(-24.45f, 25.697f, 0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = choose;
        }
    }
  
    
       
    
  
    
    
}
