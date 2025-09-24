using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LETTER : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D targetRb;


    void Start()
    {
        // 取得 target 的 Rigidbody2D
        targetRb = target.GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        // 在 Update 中執行其他的邏輯
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Chara2.moveSpeed = 4;
        Chara2.jumpForce = 4;
        targetRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        targetRb.constraints |= RigidbodyConstraints2D.FreezePositionY;
      
        Chara2.animator.speed = 1f;
        Chara2.moveSpeed = 2f;
        Chara2.jumpForce = 1;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Chara2.animator.SetInteger("walk", 1);
            target.transform.Translate(Vector3.up *4* Time.deltaTime);
         
        }
             

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Chara2.animator.SetInteger("walk", 1);
            target.transform.Translate(Vector3.down *4* Time.deltaTime);
          
        }
       
    }
}

