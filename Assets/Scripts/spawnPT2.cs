using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPT2 : MonoBehaviour
{

   
    // Start is called before the first frame update
    void Start()
    {
     
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.E))
        {
            SpawnPT.spawn_active = 2;
            health.HP = health.maxHp;


        }
    }
}
