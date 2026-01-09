using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill_list : MonoBehaviour
{
    private TestAddItem testAddItem;
    public static bool attack = false;
    public static bool magic = false;
    public static bool shield = false;
    public static bool dash = false;
    public static bool atkHP = false;
    public static bool atkMP = false;
    public static bool atkC = false;
    public static int shield_hp = 0;
    public static bool isshield=false;
    private Dictionary<string, bool> used = new Dictionary<string, bool>();
    static bool atkCActive = false;
    static int baseAtk;
    static skill_list instance;
    void Start()
    {
        testAddItem = FindObjectOfType<TestAddItem>();
        instance = this;
        // 初始化每個技能的觸發紀錄
        used["attack"] = false;
        used["magic"] = false;
        used["shield"] = false;
        used["dash"] = false;
        used["atkHP"] = false;
        used["atkMP"] = false;
        used["atkC"] = false;
    }

    void Update()
    {
       
        TriggerOnce(attack, "attack", 14);
        TriggerOnce(magic, "magic", 18);
        TriggerOnce(shield, "shield", 21);
        TriggerOnce(dash, "dash", 17);
        TriggerOnce(atkHP, "atkHP", 24);
        TriggerOnce(atkMP, "atkMP", 26);
        TriggerOnce(atkC, "atkC", 25);

     if(shield_hp==0)
        {
            isshield = false;
            
        }
       
    }

    void TriggerOnce(bool condition, string key, int itemID)
    {
        if (condition && !used[key])
        {
            used[key] = true;          // 記錄為已觸發
            testAddItem.PickUpItem(itemID);
        }
    }
    
   public static void  skill_shield()
    {
        if(shield == true)
        {
            shield_hp += Chara2.magic;
            isshield = true;
        }
    }
    public static void passive_atkHP()
    {
         if(atkHP==true)
        {
            float chance = 0.5f;
            if(Random.value<chance)
            {
                health.HP++;
            }
            
        }
    }
    public static void passive_atkMP()
    {
        if (atkMP == true)
        {
            float chance = 0.5f;
            if (Random.value < chance)
            {
                health.mana++;
            }

        }
    }
    public static void passive_atkC()
    {
        if (!atkC) return;
        if (atkCActive) return;   // 正在 2 倍中，不允許再觸發

        if (Random.value < 0.5f)
        {
            atkCActive = true;
            baseAtk = Chara2.atk;
            Chara2.atk = baseAtk * 2;

            // 0.1 秒後自動還原
            instance.Invoke(nameof(ResetAtkC), 0.4f);
        }
    }
    void ResetAtkC()
    {
        Chara2.atk = baseAtk;
        atkCActive = false;
    }
}