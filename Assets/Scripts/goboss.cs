using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goboss : MonoBehaviour
{
    public int num;
    public GameObject player;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            switch (num)
            {
                case 0:
                    player.transform.position = new Vector3(20.2800007f, -44.5999985f, 0);
                    break;
                case 1:
                    player.transform.position = new Vector3(41.5f, -26.71f, 0);
                    break;
            }

            

            }

        }
    } 

