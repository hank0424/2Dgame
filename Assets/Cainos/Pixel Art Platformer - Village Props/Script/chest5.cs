using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest5 : MonoBehaviour
{
    public int box=0;
    public Animator animator;
    private bool isopen=false;
    private TestAddItem testAddItem;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E)&&isopen==false)
        {
            Open();
           
            switch (box)
            {
                case 1:
                    isopen = true;
                    money.money1 += 100;
                    break;
                case 2:
                    isopen = true;
                    money.money1 += 150;
                    break;
                case 3:
                    isopen = true;
                    testAddItem.PickUpItem(9);
                    break;
                case 4:
                    isopen = true;
                    testAddItem.PickUpItem(10);
                    break;
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        testAddItem = FindObjectOfType<TestAddItem>();
    }
    void Open()
    {
        animator.SetBool("IsOpened", true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
