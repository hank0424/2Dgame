using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots; // 所有的背包槽位（包含素材、道具、武器等）
    public InventorySlot[] hotbarSlots;    // 快捷欄的槽位（數字鍵 1~9 可使用）
    public GameObject inventoryItemPrefab; // 用來生成 UI 物品的 prefab
    public int backpackLV = 0;

    public void Start()
    {
        RefreshSlotActive();
    }
    private void Update()
    {
        HandleHotbarInput(); // 每幀監聽快捷鍵輸入
    }

    /// <summary>
    /// 監聽快捷欄按鍵 (Alpha1 ~ Alpha9)，並呼叫使用道具
    /// </summary>
    private void HandleHotbarInput()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                UseHotbarItem(i);
            }
        }
    }

    /// <summary>
    /// 使用快捷欄指定槽位的道具
    /// </summary>
    private void UseHotbarItem(int index)
    {
        if (index < 0 || index >= hotbarSlots.Length) return;

        InventorySlot hotbarSlot = hotbarSlots[index];
        InventoryItem itemInSlot = hotbarSlot.GetComponentInChildren<InventoryItem>();

        // 確認該槽位有物品且為「主動使用」類型
        if (itemInSlot != null && itemInSlot.item.actionType == ActionType.主動使用)
        {
            itemInSlot.item.Use();   // 執行道具邏輯
            itemInSlot.count--;      // 使用後數量減少

            Debug.Log($"使用了快捷欄 {index + 1} 的道具: {itemInSlot.item.name}，剩餘數量: {itemInSlot.count}");

            if (itemInSlot.count <= 0)
            {
                // 數量耗盡 -> 移除物品
                Destroy(itemInSlot.gameObject);
                hotbarSlot.ClearSlot();
                Debug.Log($"快捷欄 {index + 1} 的道具已用完並被移除。");
            }
            else
            {
                itemInSlot.RefreshCount(); // 更新 UI 顯示
            }
        }
        else
        {
            Debug.Log($"快捷欄 {index + 1} 沒有可用的道具！");
        }
    }

    public bool CanAddItem(Item item, int amount = 1)
    {
        int remaining = amount;
        // 1?? 嘗試先堆疊到已存在的同類物品
        foreach (var slot in inventorySlots)
        {
            if (!slot.isOpened) continue; // 未解鎖的槽位跳過

            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < item.maxStack &&
                item.stackable &&
                slot.allowedItemType == item.type)
            {
                int addableCount = Mathf.Min(amount, item.maxStack - itemInSlot.count);
                remaining -= addableCount;
                if (remaining <= 0)
                    return true; // 全部堆完了
            }
        }

        // 2?? 如果還有剩餘數量 -> 嘗試放到空槽位
        foreach (var slot in inventorySlots)
        {
            if (!slot.isOpened) continue;
            if (slot.allowedItemType != item.type) continue; // 只能放允許類型的物品

            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                return true;
            }
        }
        Debug.Log("You don,t have a slot to pick up this item");
        return false;
    }


    /// <summary>
    /// 嘗試將物品加入背包
    /// </summary>
    public bool AddItem(Item item, int amount = 1)
    {
        // 1?? 嘗試先堆疊到已存在的同類物品
        foreach (var slot in inventorySlots)
        {
            if (!slot.isOpened) continue; // 未解鎖的槽位跳過

            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < item.maxStack &&
                item.stackable &&
                slot.allowedItemType == item.type)
            {
                int addableCount = Mathf.Min(amount, item.maxStack - itemInSlot.count);
                itemInSlot.count += addableCount;
                itemInSlot.RefreshCount();
                amount -= addableCount;

                Debug.Log($"堆疊物品：{item.itemName} 到槽位 {slot.gameObject.name}，新增數量：{addableCount}");

                if (amount <= 0) return true; // 全部堆完了
            }
        }

        // 2?? 如果還有剩餘數量 -> 嘗試放到空槽位
        foreach (var slot in inventorySlots)
        {
            if (!slot.isOpened) continue;
            if (slot.allowedItemType != item.type) continue; // 只能放允許類型的物品

            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot, amount);
                Debug.Log($"新物品 {item.itemName} 添加到槽位 {slot.gameObject.name}");
                return true;
            }
        }

        Debug.Log($"沒有可用的 {item.type} 類型槽位放置 {item.itemName}");
        return false;
    }

    /// <summary>
    /// 在指定槽位生成新物品
    /// </summary>
    private void SpawnNewItem(Item item, InventorySlot slot, int amount = 1)
    {
        // 檢查槽位是否已經被佔用
        if (slot.isOccupied || slot.GetComponentInChildren<InventoryItem>() != null)
        {
            Debug.LogWarning($"槽位 {slot.gameObject.name} 已佔用或包含物品，無法生成新物品");
            return;
        }

        // 生成物品 prefab
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        if (inventoryItem == null)
        {
            Debug.LogError($"生成的新物品缺少 InventoryItem 組件，槽位：{slot.gameObject.name}");
            Destroy(newItemGo);
            return;
        }

        // 初始化物品
        inventoryItem.InitialiseItem(item);
        inventoryItem.count = Mathf.Min(amount, item.maxStack);
        inventoryItem.RefreshCount();

        // 更新槽位狀態
        slot.isOccupied = true;
        slot.SetCurrentItem(inventoryItem);
        inventoryItem.SetCurrentSlot(slot);

        Debug.Log($"生成新物品：{item.itemName}，數量：{inventoryItem.count}，槽位：{slot.gameObject.name}");
    }

    public int GetItemCount(Item targetItem)
    {
        int totalCount = 0;

        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == targetItem)
            {
                totalCount += itemInSlot.count;
            }
        }

        return totalCount;
    }

    /// <summary>
    /// 消耗指定數量的物品
    /// </summary>
    public bool ConsumeItem(Item targetItem, int amount)
    {
        int totalCount = 0;

        // 1?? 計算總數量
        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == targetItem)
            {
                totalCount += itemInSlot.count;
            }
        }

        if (totalCount < amount)
        {
            Debug.Log($"消耗失敗：需要 {amount} 個 {targetItem.itemName}，但只有 {totalCount}");
            return false;
        }

        // 2?? 開始逐槽扣除
        int remaining = amount;

        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == targetItem)
            {
                if (itemInSlot.count <= remaining)
                {
                    // 扣掉整個槽位
                    remaining -= itemInSlot.count;
                    Destroy(itemInSlot.gameObject);
                    slot.ClearSlot();
                }
                else
                {
                    // 只扣一部分
                    itemInSlot.count -= remaining;
                    itemInSlot.RefreshCount();
                    remaining = 0;
                }
            }

            if (remaining <= 0) break;
        }

        Debug.Log($"成功消耗 {amount} 個 {targetItem.itemName}");
        return true;
    }

    public void RefreshSlotActive()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot == null) continue;
            slot.gameObject.SetActive(slot.isOpened);
        }
    }


    /// <summary>
    /// 解鎖背包槽位（每種類型解鎖指定數量）
    /// </summary>
    public void UnlockSlot(int backpackLevel)
    {
        int materialNum = 0, itemNum = 0, skillNum = 0;

        switch (backpackLevel)
        {
            case 0:
                materialNum = 1;
                itemNum = 0;
                skillNum = 0;
                break;
            case 1:
                materialNum = 1;
                itemNum = 0;
                skillNum = 0;
                break;
            case 2:
                materialNum = 2;
                itemNum = 1;
                skillNum = 1;
                break;
            case 3:
                materialNum = 2;
                itemNum = 1;
                skillNum = 1;
                break;
            case 4:
                materialNum = 2;
                itemNum = 1;
                skillNum = 1;
                break;
        }

        int openedMaterial = 0, openedItem = 0, openedSkill = 0;

        foreach (var slot in inventorySlots)
        {
            if (slot.isOpened) continue;

            if (slot.allowedItemType == ItemType.素材 && openedMaterial < materialNum)
            {
                slot.isOpened = true;
                openedMaterial++;
            }
            else if (slot.allowedItemType == ItemType.道具 && openedItem < itemNum)
            {
                slot.isOpened = true;
                openedItem++;
            }
            else if (slot.allowedItemType == ItemType.技能 && openedSkill < skillNum)
            {
                slot.isOpened = true;
                openedSkill++;
            }

            if (openedMaterial >= materialNum &&
                openedItem >= itemNum &&
                openedSkill >= skillNum)
                break;
        }
        RefreshSlotActive();
    }
}