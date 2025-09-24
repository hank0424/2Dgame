using UnityEngine;
using UnityEngine.UI;

public class Choose : MonoBehaviour
{
    public GameObject yesImage;
    public GameObject noImage;
    public GameObject UI;
    public Text yesText;
    public Text noText;

    private bool isUIVisible = false;
    private bool isYesVisible = false;
    private bool isNoVisible = false;

    private void Start()
    {
        HideAllObjects();
    }

    void Update()
    {
        if(isYesVisible==false)
        {
            SceneCT.agree = 0;
        }
        if (isYesVisible)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 如果按下 Enter，回傳 agree = 1
                SceneCT.agree = 1;
                HideAllObjects();
            }
        }
        else if (isNoVisible)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 如果按下 Enter，回傳 agree = 0
                SceneCT.agree = 0;
                HideAllObjects();
            }
        }

        // 切換 yes 和 no 的顯示狀態
        if (Input.GetKeyDown(KeyCode.DownArrow)&& yesText.enabled==true)
        {
            isNoVisible = true;
            isYesVisible = false;
            UpdateUIVisibility();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)&& yesText.enabled == true)
        {
            isNoVisible = false;
            isYesVisible = true;
            UpdateUIVisibility();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            yesText.enabled = true;
            noText.enabled = true;
            // 顯示 yes 和文字
            isUIVisible = true;
            isYesVisible = true;
            isNoVisible = false;
            SceneCT.agree = -1; // 重置 agree
            UpdateUIVisibility();
        }
    }

    void UpdateUIVisibility()
    {
        // 設定 UI 的顯示狀態
        yesImage.SetActive(isYesVisible);
        noImage.SetActive(isNoVisible);
        UI.SetActive(isUIVisible);
        
        
    }

    void HideAllObjects()
    {
        // 隱藏所有物件
        yesImage.SetActive(false);
        noImage.SetActive(false);
        UI.SetActive(false);
        yesText.enabled = false;
        noText.enabled = false;
       
       
    }

}

