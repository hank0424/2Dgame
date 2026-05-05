using System.Collections;
using UnityEngine;

public class chest5 : MonoBehaviour
{
    public int box = 0;
    public GameObject pic;
    public Animator animator;

    private bool isopen = false;
    private bool playerInRange = false;
    private TestAddItem testAddItem;

    void Start()
    {
        testAddItem = FindObjectOfType<TestAddItem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isopen)
        {
            OpenReward();
        }
    }

    void OpenReward()
    {
        Open();
        isopen = true;

        switch (box)
        {
            case 1:
                money.money1 += 100;
                break;
            case 2:
                money.money1 += 150;
                break;
            case 3:
                testAddItem.PickUpItem(9);
                break;
            case 4:
                testAddItem.PickUpItem(10);
                witch_house.key = true;
                break;
            case 5:
                testAddItem.PickUpItem(12);
                break;
            case 6:
                testAddItem.PickUpItem(13);
                break;
            case 7:
                testAddItem.PickUpItem(7);
                break;
            case 8:
                testAddItem.PickUpItem(2);
                break;
            case 9:
                skill_list.magic = true;
                break;
            case 10:
                testAddItem.PickUpItem(3);
                Chara2.atk += 1;
                break;
            case 11:
                skill_list.attack = true;
                break;
            case 12:
                money.money1 += 500;
                break;
        }
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
}