using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPT3 : MonoBehaviour
{
    public int num=0;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.E))
        {
            SpawnPT.spawn_active = num;
            health.HP = health.maxHp;


        }
    }
}
