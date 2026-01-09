using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest5 : MonoBehaviour
{
    public int box=0;
    public GameObject pic;
    public Animator animator;
    private bool isopen=false;
    private TestAddItem testAddItem;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E)&&isopen==false)
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
                case 3:
                    isopen = true;
                    testAddItem.PickUpItem(9);//bosskey
                    break;
                case 4:
                    isopen = true;
                    testAddItem.PickUpItem(10);//≈ÆŒ◊ËÄ≥◊
                    break;
                case 5:
                    isopen = true;
                    testAddItem.PickUpItem(12);//ª∫À
                    break;
                case 6:
                    isopen = true;
                    testAddItem.PickUpItem(13);//∫⁄∫À
                    break;
                case 7:
                    isopen = true;
                    testAddItem.PickUpItem(7);//’®èó
                    break;
                case 8:
                    isopen = true;
                    testAddItem.PickUpItem(2);//ÀÆ
                    break;
                case 9:
                    isopen = true;
                    skill_list.magic = true;
                    break;
                case 10:
                    isopen = true;
                    testAddItem.PickUpItem(3);//sword
                    Chara2.atk += 1;
                    break;
                case 11:
                    isopen = true;
                    skill_list.attack = true;
                    break;
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        testAddItem = FindObjectOfType<TestAddItem>();
    }
    void Open()
    {
        animator.SetBool("IsOpened", true);
        StartCoroutine(MoveUp());
        Destroy(pic, 1.5f);
    }
    IEnumerator MoveUp()
    {
        float duration = 0.5f;       
        float height = 0.5f;            
        float elapsed = 0f;

        Vector3 startPos = pic.transform.position;
        Vector3 endPos = startPos + Vector3.up * height;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            pic.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

       
        pic.transform.position = endPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
