using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3tp : MonoBehaviour
{
    public GameObject player;
    public Animator a1;
    private bool unlock = false;
    public TextMesh t1;
    public TextMesh t2;
    private InventoryManager InventoryManager;
    private TestAddItem TestAddItem;
    // Start is called before the first frame update
    void Start()
    {
        TestAddItem = FindObjectOfType<TestAddItem>();
        InventoryManager = FindObjectOfType<InventoryManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && unlock == false&&InventoryManager.GetItemCount(TestAddItem.PickUpWhatItems[9])==1)
        {
            InventoryManager.ConsumeItem(TestAddItem.PickUpWhatItems[9],1);
            a1.SetBool("IsOpened", true);
            Invoke("unlock1", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.E) && unlock == true)
        {
            player.transform.position = new Vector3(35.904f, 46.4f, 0f);
        }
        if(unlock==false)
        {
            t1.text = "\"上鎖了\"";
            t2.text = "\"上鎖了\"";
        }
        if (unlock == true)
        {
            t1.text = "\"進入\"";
            t2.text = "\"進入\"";
        }
    }
    // Update is called once per frame

    void Update()
    {

    }
    void unlock1()
        {
        unlock = true;
    }

}
