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

    //-------------------------
    // ��Ʒ�Y��
    private string str_name = "��ơ��";
    private string str_desc = "ُ�I������������������(1)";

    private string mana_name = "÷��t��";
    private string mana_desc = "ُ�I����������ħ��������(1)�cħ������(1)";

    private string sp_name = "�������{";
    private string sp_desc = "ُ�I����������ȫ����(1)";

    private string bk_name = "��������I";
    private string bk2_name = "��������II";
    private string bk3_name = "��������III";
    private string bk4_name = "��������IV";
    private string bk_desc = "������������������[�ز�(1) ����(1) ����(1)]";

    private string n_name = "Ո�x�����";
    private string n_desc = "�c������@�����P�YӍ����";

    private string reg_name = "�؏�ˎˮ";
    private string reg_desc = "ʹ���ጢHP�؏������ֵ(����Ʒ)";

    private string bomb_name = "ը��";
    private string bomb_desc = "ʹ���ጦ�����ȵ����І�λ��ɂ���(����Ʒ)";

    // Start is called before the first frame update
    void Start()
    {
        SelectItem("none");
    }

    // Update is called once per frame
    void Update()
    {
        if (remain1 != null)
            remain1.text = "ʣ�N:" + str_potion;

        if (remain2 != null)
            remain2.text = "ʣ�N:" + mana_potion;

        if (remain3 != null)
            remain3.text = "ʣ�N:" + sp_potion;

    
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
                name.color = new Color32(255, 255, 255, 255);
                break;
            case "str":
                name.text = str_name;
                scribe.text = str_desc;
                name.color = new Color32(245, 191, 94, 255);
                break;
            case "mana":
                name.text = mana_name;
                scribe.text = mana_desc;
                name.color = new Color32(140, 140, 255, 255);
                break;
            case "sp":
                name.text = sp_name;
                scribe.text = sp_desc;
                name.color = new Color32(255, 100, 150, 255);
                break;
            case "bk":
                UpdateBackpackName();
                break;
            case "reg":
                name.text = reg_name;
                scribe.text = reg_desc;
                name.color = new Color32(245, 98, 94, 255);
                break;
            case "bomb":
                name.text = bomb_name;
                scribe.text = bomb_desc;
                name.color = new Color32(128, 128, 128, 255);
                break;
        }
    }

    public void BuyItem()
    {
        if (currentItem == "")
        {
            Debug.Log("��δ�x����Ʒ��");
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
        }
    }

    public void str() { SelectItem("str"); }
    public void mana() { SelectItem("mana"); }
    public void sp() { SelectItem("sp"); }
    public void bk() { SelectItem("bk"); }
    public void reg() { SelectItem("reg"); }
    public void bomb() { SelectItem("bomb"); }

    public void buy()
    {
        switch (name.text)
        {
            case "��ơ��":
                if (money.money1 >= 100 && str_potion >= 1)
                {
                    str_potion -= 1;
                    money.money1 -= 100;
                    Chara2.atk += 1;
                }
                break;

            case "÷��t��":
                if (money.money1 >= 100 && mana_potion >= 1)
                {
                    mana_potion -= 1;
                    money.money1 -= 100;
                    Chara2.magic += 1;
                    health.maxMana += 1f;
                    health.m1 += 0.125f;
                }
                break;

            case "�������{":
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
        }
    }
}
