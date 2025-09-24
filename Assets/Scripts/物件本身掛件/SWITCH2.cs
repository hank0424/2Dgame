using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWITCH2 : MonoBehaviour
{
    private Animator a1;
    // Start is called before the first frame update
    void Start()
    {
        a1 = GetComponent<Animator>();
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            a1.SetBool("IsOn", true);
            
            fire_trap.delete =true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
