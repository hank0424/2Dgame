using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestAddItem : MonoBehaviour
{
    public InventoryManager inventoryManager; // 物品管理器（負責物品新增、消耗、解鎖）
    public Item[] PickUpWhatItems;            // 可拾取的物品清單（用陣列來模擬玩家能撿到的東西）
    public Item leatherItem;                  // 「皮革」這個指定物品（升級背包會用到）
    public Item slimeItem;
    public Item herb;
    public Item iron;
    public Item gunpowder;
    public Item spiderSilkItem;
    public Item healing;
    public Image m1;
    public Image m2;
    public Image m3;
    public Text t1;
    public Text t2;
    public Text t3;

    int lv;

   
    void Start()
    {

        m1.rectTransform.anchoredPosition = new Vector2(-202f, 0);
        m2.gameObject.SetActive(false);
        m3.gameObject.SetActive(false);
    }
    public commodity commodity;
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
 
        if(commodity.currentItem=="bk")
        {
            lv = inventoryManager.backpackLV;
            if (lv >= 4)
            {
                Debug.Log("Backpack Max Leval");
            }
            int leatherNeed = 0, slimeNeed = 0, spiderSilkNeed = 0;
            switch (lv)
            {
                case 0:
                    leatherNeed = 4;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 4);
                        Debug.Log("UP Backpack LV , cost 4 leatherItem");
                        inventoryManager.UnlockSlot(1);
                        inventoryManager.backpackLV++;
                        commodity.bkLV +=1;
                        t1.text = "6";
                        t3.text = "2";
                        m1.rectTransform.anchoredPosition = new Vector2(-322f, 0);
                        m3.rectTransform.anchoredPosition = new Vector2(-75f, 0);
                        m3.gameObject.SetActive(true);
                        
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
                case 1:
                    leatherNeed = 6; slimeNeed = 2;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed && inventoryManager.GetItemCount(slimeItem) >= slimeNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 6);
                        inventoryManager.ConsumeItem(slimeItem, 2);
                        Debug.Log("UP Backpack LV , cost 6 leatherItem and 2 SlimeItem");
                        inventoryManager.UnlockSlot(2);
                        inventoryManager.backpackLV++;
                        commodity.bkLV += 1;
                        t1.text = "8";
                        t2.text = "2";
                        t3.text = "4";

                        m2.gameObject.SetActive(true);
                        m1.rectTransform.anchoredPosition = new Vector2(-456f, 0);
                        m2.rectTransform.anchoredPosition = new Vector2(52, 0);
                        m3.rectTransform.anchoredPosition = new Vector2(-202, 0);
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
                case 2:
                    leatherNeed = 8; slimeNeed = 4; spiderSilkNeed = 2;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed && inventoryManager.GetItemCount(slimeItem) >= slimeNeed && inventoryManager.GetItemCount(spiderSilkItem) >= spiderSilkNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 8);
                        inventoryManager.ConsumeItem(slimeItem, 4);
                        inventoryManager.ConsumeItem(spiderSilkItem, 2);
                        Debug.Log("UP Backpack LV , cost 8 leatherItem ﹑ 4 SlimeItem and 2 SpiderSilkItem");
                        inventoryManager.UnlockSlot(3);
                        inventoryManager.backpackLV++;
                        commodity.bkLV += 1;
                        t1.text = "10";
                        t2.text = "4";
                        t3.text = "6";
                      
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
                case 3:
                    leatherNeed = 10; slimeNeed = 6; spiderSilkNeed = 4;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed && inventoryManager.GetItemCount(slimeItem) >= slimeNeed && inventoryManager.GetItemCount(spiderSilkItem) >= spiderSilkNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 10);
                        inventoryManager.ConsumeItem(slimeItem, 6);
                        inventoryManager.ConsumeItem(spiderSilkItem, 4);
                        Debug.Log("UP Backpack LV , cost 10 leatherItem ﹑ 6 SlimeItem and 4 SpiderSilkItem");
                        inventoryManager.UnlockSlot(4);
                        inventoryManager.backpackLV++;
                        commodity.bkLV += 1;
                    
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
            }
        }
        if (commodity.currentItem == "reg" && inventoryManager.CanAddItem(PickUpWhatItems[2]))
        {
            int herbNeed = 3;
            if (inventoryManager.GetItemCount(herb) >= herbNeed)
            {
                inventoryManager.ConsumeItem(herb, 3);
                Debug.Log("Add healing");
                PickUpItem(2);
            }
        }

        if (commodity.currentItem== "bomb" && inventoryManager.CanAddItem(PickUpWhatItems[7]))
        {
            int gunpowderNeed = 3;
            if (inventoryManager.GetItemCount(gunpowder) >= gunpowderNeed)
            {
                inventoryManager.ConsumeItem(gunpowder, 3);
                Debug.Log("製作成功");
                PickUpItem(7);
            }
        }
    }

        
}