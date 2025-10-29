using UnityEngine;

public class ShowBackpack : MonoBehaviour
{
    [Header("All Backpack")]
    public bool isBackpackOpen = false;
    public GameObject BackpackCanva;

    [Header("ChangeWarehouse.cs")]
    public ChangeWarehouse changewarehouse;

    [Header("Default Warehouse Index")]
    public int defaultWarehouseIndex = 0;

    void Start()
    {
        if (BackpackCanva != null && changewarehouse != null)
        {
            BackpackCanva.SetActive(false);
            changewarehouse.SwitchWarehouse(defaultWarehouseIndex);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isBackpackOpen = !isBackpackOpen;
            if (isBackpackOpen == true)
                OpenBackpack();
            if (isBackpackOpen == false)
                CloseBackpack();
        }
    }

    void OpenBackpack()
    {
        if (BackpackCanva != null)
            BackpackCanva.SetActive(true);

        Cursor.visible = true;
    }

    void CloseBackpack()
    {
        if (BackpackCanva != null)
            BackpackCanva.SetActive(false);

        Cursor.visible = false;
    }
}