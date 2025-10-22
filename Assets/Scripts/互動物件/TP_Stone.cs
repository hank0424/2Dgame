using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TP_Stone : MonoBehaviour
{
    public GameObject stone_active;
    public GameObject ui;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        stone_active.SetActive(false);
        ui.SetActive(false);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && !stone_active.activeSelf)
        {     
            stone_active.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && stone_active.activeSelf)
        {
            ui.SetActive(true);
            Cursor.visible =true;
        }

    }
    public void cancel()
    {
        ui.SetActive(false);
        Cursor.visible = false;
    }
    public void village()
    {
        player.transform.position = new Vector3(-27, 25.5f, 0);
        ui.SetActive(false);
        Cursor.visible = false;
    }
    public void under()
    {
        player.transform.position = new Vector3(40, -26.5f, 0);
        ui.SetActive(false);
        Cursor.visible = false;
    }
    public void home()
    {
        player.transform.position = new Vector3(-20.47f, -4.12f, 0);
        ui.SetActive(false);
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
