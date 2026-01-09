using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnpt_word : MonoBehaviour
{
    public TextMesh title;
    public TextMesh title2;
    private int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        title.gameObject.SetActive(false);
        title2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        title.gameObject.SetActive(true);
        title2.gameObject.SetActive(true);
        if(Input.GetKeyDown(KeyCode.E)&&collision.CompareTag("Player")&& count==0)
        {
            count++;
            title.text = "\"已紀錄存檔點和回復狀態!\"";
            title2.text = "\"已紀錄存檔點和回復狀態!\"";
        }
        if(health.HP<health.maxHp&& count==1)
        {
            title.text = "\"回復狀態\"";
            title2.text = "\"回復狀態\"";
        }
        if (Input.GetKeyDown(KeyCode.E) && collision.CompareTag("Player") && count == 1)
        {
          
            title.text = "\"已回復狀態!\"";
            title2.text = "\"已回復狀態!\"";
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        title.gameObject.SetActive(false);
        title2.gameObject.SetActive(false);
    }
}
