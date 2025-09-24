using UnityEngine;

public class ShowBackpack : MonoBehaviour
{
    public bool Backpack = false; // 初始狀態為不顯示背包
    public GameObject BackpackCanva; // 連結到背包介面的 Canvas
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (BackpackCanva != null)
        {
            BackpackCanva.SetActive(false); // 確保初始狀態背包介面為隱藏
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // 當玩家按下 TAB 鍵
        {
            Backpack = !Backpack; // 切換背包狀態

            if (BackpackCanva != null)
            {
                BackpackCanva.SetActive(Backpack); // 顯示或隱藏背包介面
            }
            if (Backpack)
            {
                Cursor.visible = true; // 顯示滑鼠
                Cursor.lockState = CursorLockMode.None; // 解鎖滑鼠
            }
            else
            {
                Cursor.visible = false; // 隱藏滑鼠
                Cursor.lockState = CursorLockMode.Locked; // 鎖定滑鼠
            }
        }
    }
}
