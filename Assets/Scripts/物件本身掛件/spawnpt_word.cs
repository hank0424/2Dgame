using UnityEngine;

public class spawnpt_word : MonoBehaviour
{
    public TextMesh title;
    public TextMesh title2;

    private int count = 0;
    private bool playerInRange = false;

    void Start()
    {
        title.gameObject.SetActive(false);
        title2.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            title.gameObject.SetActive(true);
            title2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            title.gameObject.SetActive(false);
            title2.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInRange) return;

        if (count == 0)
        {
            title.text = "\"按 E 紀錄存檔點和回復狀態\"";
            title2.text = "\"按 E 紀錄存檔點和回復狀態\"";
        }
        else if (health.HP < health.maxHp)
        {
            title.text = "\"按 E 回復狀態\"";
            title2.text = "\"按 E 回復狀態\"";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (count == 0)
            {
                count = 1;
                title.text = "\"已紀錄存檔點和回復狀態!\"";
                title2.text = "\"已紀錄存檔點和回復狀態!\"";
            }
            else
            {
                title.text = "\"已回復狀態!\"";
                title2.text = "\"已回復狀態!\"";

                // 這裡應該加真正回血邏輯
                // health.HP = health.maxHp;
            }
        }
    }
}
