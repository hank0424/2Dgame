using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest5 : MonoBehaviour
{
    public int box=0;
    public Animator animator;
    private bool isopen=false;
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
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
