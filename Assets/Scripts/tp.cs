using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tp : MonoBehaviour
{
    public GameObject ui;
    public GameObject player;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ui.SetActive(true);
        }
    }
    public void cancel()
    {
        ui.SetActive(false);
    }
    public void village()
    {
        player.transform.position =new Vector3(-34,25.5f,0);
        ui.SetActive(false);
    }
    public void under()
    {
        player.transform.position = new Vector3(54.2F, -2, 0);
        ui.SetActive(false);
    }
    public void home()
    {
        player.transform.position = new Vector3(22f, -4.8f, 0);
        ui.SetActive(false);
    }
    public void dungeon()
    {
        player.transform.position = new Vector3(40f, 27.3f, 0);
        ui.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
