using UnityEngine;

public class SpawnPT : MonoBehaviour
{
    public static float spawn_active = 0;

    private bool playerInRange = false;

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
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(spawn_active);

            health.HP = health.maxHp;
            spawn_active = 1;
        }
    }
}