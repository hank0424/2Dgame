using UnityEngine;
using UnityEngine.UI;

public class ChangeWarehouse : MonoBehaviour
{
    // 儲存所有倉庫頁面的 GameObject（例如不同分類的 UI 面板）
    public GameObject[] warehouseViews;

    /// <summary>
    /// 切換到指定倉庫頁面
    /// </summary>
    /// <param name="warehouseIndex">要切換的倉庫索引（從 0 開始）</param>
    public void SwitchWarehouse(int warehouseIndex)
    {
        // 1?? 先把所有倉庫頁面關閉
        foreach (GameObject warehouse in warehouseViews)
        {
            warehouse.SetActive(false);
        }

        // 2?? 如果索引在合法範圍內，開啟對應倉庫
        if (warehouseIndex >= 0 && warehouseIndex < warehouseViews.Length)
        {
            warehouseViews[warehouseIndex].SetActive(true);
        }
    }
}
