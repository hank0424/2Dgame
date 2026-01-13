using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class commodity : MonoBehaviour
{
    private int str_potion = 3;
    private int mana_potion = 3;
    private int sp_potion = 1;
    public Text scribe;
    public Text remain1;
    public Text remain2;
    public Text remain3;
    public Text name;
    public static int bkLV = 0;
    public static string currentItem = "";
    private TestAddItem testAddItem;
    private InventoryManager inventoryManager;


    //-------------------------
    // 商品資料
    private string str_name = "生啤酒";
    private string str_desc = "購買後永久提升物理攻擊力(1)";

    private string mana_name = "梅洛紅酒";
    private string mana_desc = "購買後永久提升魔法攻擊力(1)與魔力上限(1)";

    private string sp_name = "神秘特調";
    private string sp_desc = "購買後永久提升全屬性(1)";

    private string bk_name = "背包升級I";
    private string bk2_name = "背包升級II";
    private string bk3_name = "背包升級III";
    private string bk4_name = "背包升級IV";
    private string bk_desc = "升級後提升背包容量[素材(1) 道具(1) 技能(1)]";

    private string n_name = "請選擇物品";
    private string n_desc = "點物品圖片獲得相關資訊敘述";

    private string reg_name = "回復藥水";
    private string reg_desc = "使用後將HP回復至最大值(消耗品)";

    private string bomb_name = "炸彈";
    private string bomb_desc = "使用後對範圍內的所有單位造成傷害(消耗品)";

    private string leather_name = "皮革";
    private string leather_desc = "從野生動物身上獲取的生皮,用於升級背包的素材";

    private string slime_name = "史萊姆黏液";
    private string slime_desc = "從史萊姆提取的特殊黏液,用於升級背包的素材";

    private string needle_name = "線";
    private string needle_desc = "從蜘蛛身上獲取的絲線,用於升級背包的素材";

    private string herb_name = "藥草";
    private string herb_desc = "氣味讓人精神氣爽的草藥,用於製作回復藥水的素材";

    private string gunpowder_name = "火藥";
    private string gunpowder_desc = "易燃的黑色粉末,用於製作炸彈的素材";

    private string iron_name = "鐵錠";
    private string iron_desc = "品質優良的金屬,用於強化武器的素材";

    private string atkHP_name = "攻擊吸血";
    private string atkHP_desc = "被動技能,揮擊攻擊成功命中時有50%的機率回復1HP";

    private string atkC_name = "加倍機會";
    private string atkC_desc = "被動技能,揮擊攻擊成功命中時有50%的機率造成雙倍傷害";

    private string atkMP_name = "攻擊回魔";
    private string atkMP_desc = "被動技能,揮擊攻擊成功命中時有50%的機率回復1MP";

    private string shield_name = "魔力屏障";
    private string shield_desc = "主動技能,按下F鍵施放,消耗2MP生成阻擋*魔法攻擊力*點傷害的屏障";

    private string dash_name = "空中衝刺";
    private string dash_desc = "主動技能,於空中按下方向鍵左或右+Ctrl鍵施放,消耗1MP位移一段距離並對碰撞到的生物造成*魔法攻擊力*點傷害";

    private string fire_name = "焰型直劍";
    private string fire_desc = "物理攻擊+1,將武器附加火元素,主動技能會因此產生變化";

    private string dark_name = "殞落王者之劍";
    private string dark_desc = "物理、魔法攻擊+1,將武器附加闇元素,主動技能會因此產生變化";


    // Start is called before the first frame update
    void Start()
    {
        SelectItem("none");
        testAddItem = FindObjectOfType<TestAddItem>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (remain1 != null)
            remain1.text = "剩餘:" + str_potion;

        if (remain2 != null)
            remain2.text = "剩餘:" + mana_potion;

        if (remain3 != null)
            remain3.text = "剩餘:" + sp_potion;

    
        if (currentItem == "bk")
        {
            UpdateBackpackName();
        }
    }

    
    void UpdateBackpackName()
    {
        switch (bkLV)
        {
            case 0:
                name.text = bk_name;
                break;
            case 1:
                name.text = bk2_name;
                break;
            case 2:
                name.text = bk3_name;
                break;
            case 3:
                name.text = bk4_name;
                break;
        }

        scribe.text = bk_desc;
        name.color = new Color32(180, 101, 37, 255);
    }

    public void SelectItem(string item)
    {
        currentItem = item;
        switch (item)
        {
            case "none":
                name.text = n_name;
                scribe.text = n_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(255, 255, 255, 255);
                break;
            case "str":
                name.text = str_name;
                scribe.text = str_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(245, 191, 94, 255);
                break;
            case "mana":
                name.text = mana_name;
                scribe.text = mana_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(140, 140, 255, 255);
                break;
            case "sp":
                name.text = sp_name;
                scribe.text = sp_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(255, 100, 150, 255);
                break;
            case "bk":
                UpdateBackpackName();
                break;
            case "reg":
                name.text = reg_name;
                scribe.text = reg_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(245, 98, 94, 255);
                break;
            case "bomb":
                name.text = bomb_name;
                scribe.text = bomb_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(128, 128, 128, 255);
                break;
            case "leather":
                name.text = leather_name;
                scribe.text = leather_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(213, 116, 87, 255);
                break;
            case "slime":
                name.text = slime_name;
                scribe.text = slime_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(130, 201, 126, 255);
                break;
            case "needle":
                name.text = needle_name;
                scribe.text = needle_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(255, 255, 255, 255);
                break;
            case "herb":
                name.text = herb_name;
                scribe.text = herb_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(141, 255, 61, 255);
                break;
            case "gunpowder":
                name.text = gunpowder_name;
                scribe.text = gunpowder_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(128, 128, 128, 255);
                break;
            case "iron":
                name.text = iron_name;
                scribe.text = iron_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(249, 227, 255, 255);
                break;
            case "passive_atkHP":
                name.text = atkHP_name;
                scribe.text = atkHP_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(255, 78, 75, 255);
                break;
            case "passive_atkC":
                name.text = atkC_name;
                scribe.text = atkC_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(255, 212, 82, 255);
                break;
            case "passive_atkMP":
                name.text = atkMP_name;
                scribe.text = atkMP_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(130, 222, 255, 255);
                break;
            case "shield":
                name.text = shield_name;
                scribe.text = shield_desc;
                scribe.fontSize = 22;
                scribe.lineSpacing = 1f;
                name.color = new Color32(130, 222, 255, 255);
                break;
            case "dash":
                name.text = dash_name;
                scribe.text = dash_desc;
                scribe.fontSize = 16;
                scribe.lineSpacing = 1.2f;
                name.color = new Color32(130, 222, 255, 255);
                break;
            case "sword1":
                name.text = fire_name;
                scribe.text = fire_desc;
                scribe.fontSize = 16;
                scribe.lineSpacing = 1.2f;
                name.color = new Color32(255, 148, 123, 255);
                break;
            case "sword2":
                name.text = dark_name;
                scribe.text = dark_desc;
                scribe.fontSize = 16;
                scribe.lineSpacing = 1.2f;
                name.color = new Color32(200, 148, 255, 255);      
                break;
        }
    }

    public void BuyItem()
    {
        if (currentItem == "")
        {
            Debug.Log("尚未選擇商品！");
            return;
        }

        switch (currentItem)
        {
            case "str":
                break;
            case "mana":
                break;
            case "sp":
                break;
            case "leather":
                break;
            case "slime":
                break;
            case "needle":
                break;
            case "herb":
                break;
            case "gunpowder":
                break;
            case "iron":
                break;
            case "passive_atkHP":
                break;
            case "passive_atkC":
                break;
            case "passive_atkMP":
                break;
            case "shield":
                break;
            case "dash":
                break;
        }
    }

    public void str() { SelectItem("str"); }
    public void mana() { SelectItem("mana"); }
    public void sp() { SelectItem("sp"); }
    public void bk() { SelectItem("bk"); }
    public void reg() { SelectItem("reg"); }
    public void bomb() { SelectItem("bomb"); }
    public void leather() { SelectItem("leather"); }
    public void slime() { SelectItem("slime"); }
    public void needle() { SelectItem("needle"); }
    public void herb() { SelectItem("herb"); }
    public void gunpowder() { SelectItem("gunpowder"); }
    public void iron() { SelectItem("iron"); }
    public void passive_atkHP() { SelectItem("passive_atkHP"); }
    public void passive_atkC() { SelectItem("passive_atkC"); }
    public void passive_atkMP() { SelectItem("passive_atkMP"); }
    public void shield() { SelectItem("shield"); }
    public void dash() { SelectItem("dash"); }
    public void sword1() { SelectItem("sword1"); }
    public void sword2() { SelectItem("sword2"); }

    public void buy()
    {
        switch (name.text)
        {
            case "生啤酒":
                if (money.money1 >= 150 && str_potion >= 1)
                {
                    str_potion -= 1;
                    money.money1 -= 150;
                    Chara2.atk += 1;
                }
                break;

            case "梅洛紅酒":
                if (money.money1 >= 250 && mana_potion >= 1)
                {
                    mana_potion -= 1;
                    money.money1 -= 250;
                    Chara2.magic += 1;
                    health.maxMana += 1f;
                    health.m1 += 0.125f;
                }
                break;

            case "神秘特調":
                if (money.money1 >= 300 && sp_potion >= 1)
                {
                    sp_potion -= 1;
                    money.money1 -= 300;

                    Chara2.atk += 1;
                    Chara2.magic += 1;
                    health.HP += 1;
                    health.maxHp += 1f;
                    health.maxMana += 1f;
                    health.h1 += 0.1f;
                    health.m1 += 0.125f;
                    health.max += 0.1f;
                }
                break;
            case "皮革":
                if (money.money1 >=20 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[0]))
                {               
                    money.money1 -= 20;
                    testAddItem.PickUpItem(0);

                }
                break;
            case "藥草":
                if (money.money1 >= 50 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[4]))
                {
                    money.money1 -= 50;
                    testAddItem.PickUpItem(4);
                }
                break;
            case "史萊姆黏液":
                if (money.money1 >= 50 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[6]))
                {
                    money.money1 -= 50;
                    testAddItem.PickUpItem(6);
                }
                break;
            case "線":
                if (money.money1 >= 50 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[5]))
                {
                    money.money1 -= 50;
                    testAddItem.PickUpItem(5);
                }
                break;
            case "火藥":
                if (money.money1 >= 50 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[8]))
                {
                    money.money1 -= 50;
                    testAddItem.PickUpItem(8);
                }
                break;
            case "鐵錠":
                if (money.money1 >= 100 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[1]))
                {
                    money.money1 -= 100;
                    testAddItem.PickUpItem(1);
                }
                break;
            case "攻擊吸血":
                if (money.money1 >= 50 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[14])&&skill_list.atkHP==false)
                {
                    money.money1 -= 50;
                    skill_list.atkHP = true;
                }
                break;
            case "加倍機會":
                if (money.money1 >= 100 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[14]) && skill_list.atkC == false)
                {
                    money.money1 -= 100;
                    skill_list.atkC = true;
                }
                break;
            case "攻擊回魔":
                if (money.money1 >= 50 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[14]) && skill_list.atkMP == false)
                {
                    money.money1 -= 50;
                    skill_list.atkMP = true;
                }
                break;
            case "魔力屏障":
                if (money.money1 >= 100 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[14]) && skill_list.shield == false)
                {
                    money.money1 -= 100;
                    skill_list.shield = true;
                }
                break;
            case "空中衝刺":
                if (money.money1 >= 100 && inventoryManager.CanAddItem(testAddItem.PickUpWhatItems[14]) && skill_list.dash == false)
                {
                    money.money1 -= 100;
                    skill_list.dash = true;
                }
                break;
        }
    }
}
