using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWITCH2 : MonoBehaviour
{
    public GameObject destory;
    
    public int num=0;
    private Animator a1;
    // Start is called before the first frame update
    void Start()
    {
        a1 = GetComponent<Animator>();
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (num)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    a1.SetBool("IsOn", true);

                    destory.SetActive(false);
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    a1.SetBool("IsOn", true);

                    destory.SetActive(false);
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    a1.SetBool("IsOn", true);

                    destory.SetActive(false);
                }
                break;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
