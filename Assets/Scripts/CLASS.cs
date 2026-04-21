using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CLASS : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = new Vector3(49.5f, -26.7f, 0);
        }
    }
}
