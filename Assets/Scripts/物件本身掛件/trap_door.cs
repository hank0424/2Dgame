using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_door : MonoBehaviour
{
    public Collider2D col;
    public Animator animator;
    public GameObject door;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("IsOpened", false);
            col.isTrigger = false;

        }
      
        
    }
    private void Start()
    {
       

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("IsOpened", true);
            col.isTrigger = true;
        }

          
    }
}
