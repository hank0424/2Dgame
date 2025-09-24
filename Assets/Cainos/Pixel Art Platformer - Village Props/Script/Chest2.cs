using System.Collections;
using System.Collections.Generic;
using UnityEngine;




    public class Chest2 : MonoBehaviour
    {


        private void OnTriggerStay2D(Collider2D collision)
        {
        ;
        if (Input.GetKey(KeyCode.E))
            {
                print("z");
                Open();
                Chara2.doubleJ = true;

            }
        }

        public Animator animator;





       void  Open()
        {
            animator.SetBool("IsOpened", true);
        }



    }

