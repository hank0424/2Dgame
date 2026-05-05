using UnityEngine;

public class TP_Stone : MonoBehaviour
{
    public GameObject stone_active;
    public GameObject ui;
    public GameObject player;
    public int number = 0;

    private bool playerInRange = false;

    public static bool plain = false;
    public static bool cave = false;
    public static bool villiage = false;
    public static bool dungeon = false;
    public static bool dungeon2 = false;

    void Start()
    {
        stone_active.SetActive(false);
        ui.SetActive(false);
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
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!stone_active.activeSelf)
            {
                stone_active.SetActive(true);

                switch (number)
                {
                    case 1: TP_stone_unlock.num = 1; break;
                    case 2: TP_stone_unlock.num = 2; break;
                    case 3: TP_stone_unlock.num = 3; break;
                    case 4: TP_stone_unlock.num = 4; break;
                    case 5: TP_stone_unlock.num = 5; break;
                }
            }
            else
            {
                ui.SetActive(true);
                Cursor.visible = true;
            }
        }
    }

    public void cancel()
    {
        ui.SetActive(false);
        Cursor.visible = false;
    }

    public void village()
    {
        player.transform.position = new Vector3(-27, 25.5f, 0);
        cancel();
    }

    public void under()
    {
        player.transform.position = new Vector3(43.43f, -27f, 0);
        cancel();
    }

    public void home()
    {
        player.transform.position = new Vector3(-20.47f, -4.12f, 0);
        cancel();
    }
}