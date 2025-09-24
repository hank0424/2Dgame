using UnityEngine;

public class TestAddItem : MonoBehaviour
{
    public InventoryManager inventoryManager; // 物品管理器（負責物品新增、消耗、解鎖）
    public Item[] PickUpWhatItems;            // 可拾取的物品清單（用陣列來模擬玩家能撿到的東西）
    public Item leatherItem;                  // 「皮革」這個指定物品（升級背包會用到）

    /// <summary>
    /// 模擬撿起一個物品（例如從場景中獲得）
    /// </summary>
    /// <param name="id">PickUpWhatItems 的索引</param>
    public void PickUpItem(int id)
    {
        // 嘗試透過 InventoryManager 新增物品
        bool result = inventoryManager.AddItem(PickUpWhatItems[id]);

        if (result == true)
        {
            Debug.Log("添加成功");
        }
        else
        {
            Debug.Log("添加失敗");
        }
    }

    /// <summary>
    /// 模擬升級背包功能（需要消耗皮革）
    /// </summary>
    public void UpgradeBackpack()
    {
        // 假設升級需要 4 個皮革
        if (inventoryManager.ConsumeItem(leatherItem, 4))
        {
            Debug.Log("成功升級背包，消耗了 4 個皮革");
            inventoryManager.UnlockSlot(2); // 解鎖：素材 +2，道具 +2
        }
        else
        {
            Debug.Log("升級失敗，皮革不足");
        }
    }
}
