using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 這個屬性會讓 Unity 編輯器中可以透過右鍵選單建立此 ScriptableObject
// 路徑為「Create → Scriptable object → Item」
[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    // ===== 遊戲邏輯相關設定 =====
    [Header("Only gameplay")] // 在 Inspector 顯示為一個標題
    public TileBase tile;      // 該物品對應的 Tile（用於地圖上的顯示）
    public ItemType type;      // 物品類型（素材、道具、武器等）
    public ActionType actionType; // 使用方式（被動、主動、裝備）
    public Vector2Int range = new Vector2Int(5, 4);
    // 使用範圍（可能用於武器攻擊範圍或道具影響區域）

    // ===== UI 顯示相關設定 =====
    [Header("Only UI")]
    public bool stackable = true; // 是否可以堆疊（如藥水、素材）
    public int maxStack = 10;     // 堆疊上限數量
    public string itemName;       // 物品名稱（UI 顯示）

    // ===== UI 與遊戲都會用到 =====
    [Header("Both")]
    public Sprite image;          // 物品圖片（UI 顯示或遊戲內使用）

    // ===== 物品使用邏輯 =====
    public virtual void Use()
    {
        if (actionType == ActionType.主動使用)
        {
            // 如果物品設定為「主動使用」，則執行這裡的邏輯
            Debug.Log($"使用了道具：{name}");

            // TODO: 在這裡加入具體功能，例如：
            // - 回復 HP
            // - 增加攻擊力
            // - 觸發 Buff
            // - 生成特效等等
        }
        else
        {
            // 如果物品不是主動使用類型，顯示警告訊息
            Debug.LogWarning($"道具：{name} 無法主動使用！");
        }
    }
}

// ===== 物品類型列舉 =====
// 用於區分物品大分類（方便管理和判斷）
public enum ItemType
{
    素材,     // 基本材料
    道具,     // 一般道具（例如回復藥水）
    關鍵道具, // 劇情或解謎用的特殊道具
    武器,     // 可以裝備的武器
    技能      // 技能書或學習技能的物品
}

// ===== 使用方式列舉 =====
public enum ActionType
{
    被動使用, // 自動生效（例如裝備後自動增加屬性）
    主動使用, // 玩家手動使用（例如藥水、技能卷軸）
    裝備      // 可裝備型物品（武器、盔甲）
}
