using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_door : MonoBehaviour
{
    public Collider2D col;
    public Animator animator;
    public GameObject door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("IsOpened",false);
        col.isTrigger = false;
        
    }
    private void Start()
    {
      
     
    }
    private void Update()
    {
        if(health.HP<=0)
        {
            col.isTrigger = true;
        }
    }
}
