using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public static float max=0.5f;
    [Header("UI 元件")]
    public static float h1 = 0.5f;
    public static float m1 = 0.5f;
    [SerializeField] public  Image total_health;   // 背景條
    [SerializeField] private Image current_health; // 現在的血條
    [SerializeField] public  Image total_mana;
    [SerializeField] private Image current_mana;

    [Header("角色數值")]
    public static float maxHp = 5f;   // 初始最大血量 = 5格
    public static float HP = 5f;      // 當前血量 = 5格
    public static float maxMana = 4f; // 初始最大魔力 = 5格
    public static float mana = 4f;    // 當前魔力 = 5格

    private float manaRegenInterval = 4f;
    private bool isRegen = false;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {

        if(HP>maxHp)
        {
            HP = maxHp;
        }
        
        // 測試按鍵：L 減血，K 加血
        if (Input.GetKeyDown(KeyCode.L))
            HP -= 1;
        if (Input.GetKeyDown(KeyCode.K))
            HP += 1;

        UpdateUI();

        if (mana < maxMana && !isRegen)
            StartCoroutine(RegenMana());
      
      
    }

    // 更新UI顯示
    public void UpdateUI()
    {
        current_health.fillAmount = HP / 10f;
        current_mana.fillAmount = mana / 8f;

        // 這裡假設 total_health / total_mana 是固定背景，永遠滿格
        total_health.fillAmount = h1;
        total_mana.fillAmount = m1;
    }

    private IEnumerator RegenMana()
    {
        isRegen = true;
        while (mana < maxMana)
        {
            yield return new WaitForSeconds(manaRegenInterval);
            mana += 1f;
            mana = Mathf.Clamp(mana, 0f, maxMana);
            UpdateUI();
        }
        isRegen = false;
    }
}