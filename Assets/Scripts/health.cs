using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [SerializeField] private Image total_health;
    [SerializeField] private Image current_health;
    [SerializeField] private Image total_mana;
    [SerializeField] private Image current_mana;
    public static float HP = 5f;
    public static float mana = 4f;
    public static float MAXmana = 4f;

    private float manaRegenerationInterval = 4f; // 每次增加1点魔法的时间间隔
    private bool isRegeneratingMana = false;

    // Start is called before the first frame update
    void Start()
    {
        total_health.fillAmount = HP / 10f;
        current_health.fillAmount = HP / 10f;
        total_mana.fillAmount = mana / 8f;
        current_mana.fillAmount = mana / 8f;
    }

    // Update is called once per frame
    void Update()
    {
        print(mana);
        print(current_mana.fillAmount);

        // 检查生命值是否小于等于0，如果是，重置为5
        if (HP <= 0)
        {
            HP = 5;
        }

        // 按下L键时减少生命值
        if (Input.GetKeyDown(KeyCode.L))
        {
            HP -= 1;
        }

        // 更新生命值和魔法值的UI显示
        current_health.fillAmount = HP / 10f; // 10为最大生命值
        current_mana.fillAmount = mana / 8f; // 8为最大魔法值

        // 检查是否需要开始魔法值恢复
        if (current_mana.fillAmount < total_mana.fillAmount && !isRegeneratingMana)
        {
            StartCoroutine(RegenerateMana());
        }
    }

    // 魔法值恢复协程
    private IEnumerator RegenerateMana()
    {
        isRegeneratingMana = true;
        while (mana < MAXmana)
        {
            yield return new WaitForSeconds(manaRegenerationInterval);
            mana += 1f;
            mana = Mathf.Clamp(mana, 0f, 8f); // 确保mana不会超过8
            current_mana.fillAmount = mana / 8f;
        }
        isRegeneratingMana = false;
    }
}