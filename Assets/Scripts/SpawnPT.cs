using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPT : MonoBehaviour
{
   
    public static float spawn_active=0;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.E))
        {
            print(spawn_active);

            health.HP = 5;

            spawn_active = 1;
        }
    }
}
