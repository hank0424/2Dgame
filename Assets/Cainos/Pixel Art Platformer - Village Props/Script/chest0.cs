using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest0 : MonoBehaviour
{
    public static int cheat = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        ;
        if (Input.GetKey(KeyCode.E))
        {

            Open();

            handLight.act = true;
        }
    }

    public Animator animator;





    void Open()
    {
        animator.SetBool("IsOpened", true);
    }

}
