using UnityEngine;

public class SHOP_UI : MonoBehaviour
{
    public GameObject ShopUI;

    private bool playerInRange = false;

    void Start()
    {
        ShopUI.SetActive(false);
        Cursor.visible = false;
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
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShopUI.SetActive(true);
            Cursor.visible = true;
        }
    }

    public void exit()
    {
        ShopUI.SetActive(false);
        Cursor.visible = false;
    }

    public void Nextpage()
    {
        ShopUI.SetActive(true);
    }

    public void closepage()
    {
        ShopUI.SetActive(false);
    }

    public void buy()
    {

    }
}
