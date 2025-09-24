using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTitle : MonoBehaviour
{
    public GameObject title;
    public GameObject title2;

    // Start is called before the first frame update
    private void Start()
    {
        title.gameObject.SetActive(false);
        title2.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        title.gameObject.SetActive(true);
        title2.gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        title.gameObject.SetActive(false);
        title2.gameObject.SetActive(false);
    }
}
