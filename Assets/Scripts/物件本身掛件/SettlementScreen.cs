using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettlementScreen : MonoBehaviour
{
    public GameObject SettlementScreenObj;
    public float Timer = 0f;
    public Text TotalTime;
    public static int DeadCount = 0;
    public Text DeadCountTxt;
    public Text rating;
    public Text rating2;
    public static bool isClear = false;

    // Start is called before the first frame updates
    void Start()
    {
            //TotalTime.gameObject.SetActive(false);
            //DeadCountTxt.gameObject.SetActive(false);
            //rating.gameObject.SetActive(false);
            //rating2.gameObject.SetActive(false);
        SettlementScreenObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear == false)
            Timer += Time.deltaTime;
        if (isClear == true)
        {
            Cursor.visible = true;
            this.gameObject.transform.position = new Vector3(41.493f, 38.54f, -1f);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && isClear == true)
        {

            int minutes = Mathf.FloorToInt(Timer / 60f);
            int seconds = Mathf.FloorToInt(Timer % 60f);
            TotalTime.text = $"ºÄ•r:{minutes}·Ö,{seconds:00}Ãë";
            DeadCountTxt.text = $"ËÀÍö:{DeadCount}";
            SettlementScreenObj.SetActive(true);
            if (minutes <= 9 && DeadCount <= 1)
            {
                rating.text = "S";
                rating.color = new Color32(255, 199, 103, 255);
                rating2.text = "S";
                rating2.color = new Color32(143, 83, 0, 255);
            }
            else if ((minutes > 9 && minutes <= 12) || (DeadCount >= 2 && DeadCount <= 4))
            {
                rating.text = "A";
                rating.color = new Color32(255, 107, 100, 255);
                rating2.text = "A";
                rating2.color = new Color32(115, 24, 43, 255);
            }
            else if ((minutes > 12 && minutes <= 15) || (DeadCount >= 5 && DeadCount <= 8))
            {
                rating.text = "B";
                rating.color = new Color32(100, 192, 255, 255);
                rating2.text = "B";
                rating2.color = new Color32(24, 42, 115, 255);
            }
            else
            {
                rating.text = "C";
                rating.color = new Color32(169, 255, 107, 255);
                rating2.text = "C";
                rating2.color = new Color32(115, 24, 43, 255);
            }
        }
    }
 
}
        
