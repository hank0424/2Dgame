using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goboss : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position =new Vector3(20.2800007f, -44.5999985f, 0);
        }
    }
}
