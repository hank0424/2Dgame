using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest3 : MonoBehaviour
{
    public static int cheat=0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        ;
        if (Input.GetKey(KeyCode.E))
        {
           
            Open();
            Chara2.shooting = true;
            cheat = 1;
        }
    }

    public Animator animator;





    void Open()
    {
        animator.SetBool("IsOpened", true);
    }

}
