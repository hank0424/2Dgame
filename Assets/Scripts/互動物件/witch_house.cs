using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class witch_house : MonoBehaviour
{
    public GameObject player;
    public Vector3 choose;
    public static bool key = false;
    public TextMesh ui1;
    public TextMesh ui2;
    public string st1;
    public string st2;
    private void Start()
    {
    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E)&&key==true)
        {
            player.transform.position = choose;
        }
    }
    private void Update()
    {
        if(key==true)
        {
            ui1.text =(st1);
            ui2.text =(st2);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            key = true;
        }
    }
}


    