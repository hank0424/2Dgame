using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest4 : MonoBehaviour
{
    // Start is called before the first frame update
    public static int genshin = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        ;
        if (Input.GetKey(KeyCode.E))
        {
            Open();
            genshin = 1;
        }
    }

    public Animator animator;





    void Open()
    {
        animator.SetBool("IsOpened", true);
    }

}
