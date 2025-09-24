using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 背包槽位 (Slot)，用來放置物品 (InventoryItem)
// 繼承 IDropHandler → 可以接收「拖曳結束時」的事件
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool isOccupied = false; // 是否已經有物品
    public bool isOpened = true;    // 槽位是否開啟（有些格子可能被鎖住）
    private InventoryItem currentItem; // 槽位內當前的物品

    [Header("Slot Settings")]
    public ItemType allowedItemType; // 這個槽位允許的物品類型 (例如只能放武器)

    // ===== 設定與獲取物品 =====
    public void SetCurrentItem(InventoryItem item)
    {
        currentItem = item;
        isOccupied = (currentItem != null);
        Debug.Log($"槽位 {gameObject.name} 的 currentItem 已設置為 {(currentItem != null ? currentItem.item.itemName : "null")}，isOccupied={isOccupied}");
    }

    public InventoryItem GetCurrentItem()
    {
        return currentItem;
    }

    // ===== 拖放事件處理 =====
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            Debug.Log("拖動物件為 null");
            return;
        }

        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        // 1?? 驗證物品是否有效 & 類型是否允許
        if (draggedItem == null || draggedItem.item == null || draggedItem.item.type != allowedItemType)
        {
            Debug.Log($"拖動的物品無效或類型不匹配");
            return;
        }

        // 確保拖曳物品有記錄「原始槽位」
        if (draggedItem.parentAfterDrag == null)
        {
            Debug.Log("parentAfterDrag 為 null，無法拖動");
            return;
        }

        // 2?? 檢查槽位是否開啟
        if (!isOpened)
        {
            // 如果槽位未開啟 → 物品返回原位
            draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
            draggedItem.transform.position = draggedItem.parentAfterDrag.position;
            Debug.Log("未開啟，物品返回原位");
            return;
        }

        InventorySlot originalSlot = draggedItem.parentAfterDrag.GetComponent<InventorySlot>();
        if (originalSlot == null)
        {
            draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
            draggedItem.transform.position = draggedItem.parentAfterDrag.position;
            Debug.Log("原槽位無效，物品返回原位");
            return;
        }

        // 3?? 確保槽位狀態一致性
        if (isOccupied && currentItem == null)
        {
            Debug.LogWarning($"槽位 {gameObject.name} 狀態異常：isOccupied 為 true 但 currentItem 為 null → 重置狀態");
            isOccupied = false;
        }

        // 4?? 如果槽位是空的 → 直接放進來
        if (!isOccupied)
        {
            originalSlot.ClearSlot(); // 清空原槽位

            draggedItem.parentAfterDrag = transform;
            draggedItem.transform.SetParent(transform);
            draggedItem.transform.position = transform.position;

            isOccupied = true;
            SetCurrentItem(draggedItem);
            draggedItem.SetCurrentSlot(this);

            Debug.Log($"物品 {draggedItem.item.itemName} 放置到槽位 {gameObject.name}");
        }
        else
        {
            // 5?? 如果槽位已經有物品 → 檢查堆疊或交換
            InventoryItem targetItem = GetCurrentItem();

            if (targetItem == null || targetItem.item == null)
            {
                Debug.LogWarning($"目標槽位 {gameObject.name} 的 currentItem 或 item 為 null → 重置");
                ClearSlot();
                originalSlot.ClearSlot();

                draggedItem.parentAfterDrag = transform;
                draggedItem.transform.SetParent(transform);
                draggedItem.transform.position = transform.position;

                isOccupied = true;
                SetCurrentItem(draggedItem);
                draggedItem.SetCurrentSlot(this);
                return;
            }

            // 5a?? 如果物品相同 & 可堆疊 → 合併
            if (targetItem.item != null && draggedItem.item == targetItem.item && draggedItem.item.stackable)
            {
                int totalCount = draggedItem.count + targetItem.count;
                int maxStack = draggedItem.item.maxStack;

                if (totalCount <= maxStack)
                {
                    // ? 全部合併
                    targetItem.count = totalCount;
                    targetItem.RefreshCount();
                    originalSlot.ClearSlot();
                    Destroy(draggedItem.gameObject);
                    Debug.Log($"合併物品：{targetItem.item.itemName}，新數量：{targetItem.count}");
                }
                else
                {
                    // ?? 超過最大堆疊 → 部分合併
                    int addableCount = maxStack - targetItem.count;
                    if (addableCount > 0)
                    {
                        targetItem.count += addableCount;
                        targetItem.RefreshCount();
                        draggedItem.count -= addableCount;
                        draggedItem.RefreshCount();
                        Debug.Log($"部分合併 → {targetItem.item.itemName} 堆疊到 {targetItem.count}，剩餘 {draggedItem.count}");
                    }
                    // 剩餘物品回原位
                    draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
                    draggedItem.transform.position = draggedItem.parentAfterDrag.position;
                }
            }
            else
            {
                // 5b?? 不同物品或不可堆疊 → 嘗試交換
                if (originalSlot.allowedItemType == targetItem.item.type)
                {
                    // ? 交換位置
                    draggedItem.transform.SetParent(transform);
                    draggedItem.transform.position = transform.position;
                    draggedItem.SetCurrentSlot(this);

                    targetItem.transform.SetParent(originalSlot.transform);
                    targetItem.transform.position = originalSlot.transform.position;
                    targetItem.SetCurrentSlot(originalSlot);

                    // 更新槽位狀態
                    SetCurrentItem(draggedItem);
                    originalSlot.SetCurrentItem(targetItem);

                    Debug.Log($"交換物品：{draggedItem.item.itemName} ? {targetItem.item.itemName}");
                }
                else
                {
                    // ? 不能交換 → 物品返回原位
                    draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
                    draggedItem.transform.position = draggedItem.parentAfterDrag.position;
                    Debug.Log("物品類型不匹配，返回原位");
                }
            }
        }
    }

    // 清空槽位 (重置狀態)
    public void ClearSlot()
    {
        isOccupied = false;
        currentItem = null;
        Debug.Log($"槽位 {gameObject.name} 已被清空");
    }
}
