using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

// InventoryItem: 表示「物品 UI 物件」
// 繼承 MonoBehaviour 並實作四個事件介面：
// - IBeginDragHandler: 拖曳開始
// - IDragHandler: 拖曳中
// - IEndDragHandler: 拖曳結束
// - IPointerClickHandler: 滑鼠點擊
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("UI")]
    public Image image;       // 物品圖示
    public Text countText;    // 數量文字
    public Text itemnameText; // 物品名稱文字

    // ===== 遊戲邏輯用欄位 =====
    [HideInInspector] public Item item;  // 對應的 Item 資料 (ScriptableObject)
    [HideInInspector] public int count = 1; // 持有數量
    [HideInInspector] public Transform parentAfterDrag; // 拖曳結束後要回去的原始父物件
    public Canvas parentCanvas; // 用來計算拖曳時 UI 的座標 (必須要有 Canvas)

    private InventorySlot currentSlot; // 紀錄目前在哪個槽位中

    // 初始化物品 (設定圖像 & 名稱 & 數量)
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
        RefreshName();
    }

    // 刷新數量顯示
    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActiv = count > 1; // 如果數量大於 1 才顯示數字 (例如藥水 *5)
        countText.gameObject.SetActive(textActiv);
    }

    // 刷新名稱顯示
    public void RefreshName()
    {
        itemnameText.text = item.itemName;
    }

    // 設定當前所在的槽位
    public void SetCurrentSlot(InventorySlot slot)
    {
        currentSlot = slot;
        if (slot != null)
        {
            slot.isOccupied = true; // 更新槽位狀態為「已被佔用」
        }
    }

    // 拖曳開始時觸發
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false; // 暫時關閉 Raycast，避免 UI 判定干擾
        parentAfterDrag = transform.parent; // 記錄拖曳前的父物件 (原始槽位)

        if (parentCanvas == null)
        {
            parentCanvas = GetComponentInParent<Canvas>(); // 如果還沒指定 Canvas，就自動找最近的 Canvas
        }

        // 把物品移動到 Canvas 下，確保拖曳時能顯示在最上層
        transform.SetParent(parentCanvas.transform);
    }

    // 拖曳中，每一幀更新位置到滑鼠位置
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    // 拖曳結束時觸發
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true; // 重新開啟 Raycast

        // 如果結束時仍然在 Canvas 下，代表沒有放進任何槽位
        if (transform.parent == parentCanvas.transform)
        {
            // 還原到原本的槽位
            transform.SetParent(parentAfterDrag);
            transform.position = parentAfterDrag.position;
            Debug.Log("拖拽結束，但未放到有效槽位，物品返回原位");
        }
    }

    // 點擊事件 (目前設定為右鍵刪除物品)
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (currentSlot != null)
            {
                currentSlot.ClearSlot(); // 清空槽位狀態
            }
            Destroy(gameObject); // 刪除物品 UI
        }
    }
}
