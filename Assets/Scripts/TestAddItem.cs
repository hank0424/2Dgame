using Unity.VisualScripting;
using UnityEngine;

public class TestAddItem : MonoBehaviour
{
    public InventoryManager inventoryManager; // ��Ʒ��������ؓ؟��Ʒ���������ġ����i��
    public Item[] PickUpWhatItems;            // ��ʰȡ����Ʒ��Σ�����Ё�ģ�M����ܓ쵽�Ė|����
    public Item leatherItem;                  // ��Ƥ��@��ָ����Ʒ�������������õ���
    public Item slimeItem;
    public Item herb;
    public Item iron;
    public Item gunpowder;
    public Item spiderSilkItem;
    public Item healing;

    int lv;

   
    void Start()
    {
       
       
    }
    public commodity commodity;
    /// <summary>
    /// ģ�M����һ����Ʒ������Ĉ����Ы@�ã�
    /// </summary>
    /// <param name="id">PickUpWhatItems ������</param>
    public void PickUpItem(int id)
    {
        // �Lԇ͸�^ InventoryManager ������Ʒ
        bool result = inventoryManager.AddItem(PickUpWhatItems[id]);

        if (result == true)
        {
            Debug.Log("��ӳɹ�");
        }
        else
        {
            Debug.Log("���ʧ��");
        }
    }

    /// <summary>
    /// ģ�M�����������ܣ���Ҫ����Ƥ�
    /// </summary>
    public void UpgradeBackpack()
    {
 
        if(commodity.currentItem=="bk")
        {
            lv = inventoryManager.backpackLV;
            if (lv >= 4)
            {
                Debug.Log("Backpack Max Leval");
            }
            int leatherNeed = 0, slimeNeed = 0, spiderSilkNeed = 0;
            switch (lv)
            {
                case 0:
                    leatherNeed = 4;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 4);
                        Debug.Log("UP Backpack LV , cost 4 leatherItem");
                        inventoryManager.UnlockSlot(1);
                        inventoryManager.backpackLV++;
                        commodity.bkLV +=1;
                        
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
                case 1:
                    leatherNeed = 6; slimeNeed = 2;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed && inventoryManager.GetItemCount(slimeItem) >= slimeNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 6);
                        inventoryManager.ConsumeItem(slimeItem, 2);
                        Debug.Log("UP Backpack LV , cost 6 leatherItem and 2 SlimeItem");
                        inventoryManager.UnlockSlot(2);
                        inventoryManager.backpackLV++;
                        commodity.bkLV += 1;
                       
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
                case 2:
                    leatherNeed = 8; slimeNeed = 4; spiderSilkNeed = 2;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed && inventoryManager.GetItemCount(slimeItem) >= slimeNeed && inventoryManager.GetItemCount(spiderSilkItem) >= spiderSilkNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 8);
                        inventoryManager.ConsumeItem(slimeItem, 4);
                        inventoryManager.ConsumeItem(spiderSilkItem, 2);
                        Debug.Log("UP Backpack LV , cost 8 leatherItem �p 4 SlimeItem and 2 SpiderSilkItem");
                        inventoryManager.UnlockSlot(3);
                        inventoryManager.backpackLV++;
                        commodity.bkLV += 1;
                        
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
                case 3:
                    leatherNeed = 10; slimeNeed = 6; spiderSilkNeed = 4;
                    if (inventoryManager.GetItemCount(leatherItem) >= leatherNeed && inventoryManager.GetItemCount(slimeItem) >= slimeNeed && inventoryManager.GetItemCount(spiderSilkItem) >= spiderSilkNeed)
                    {
                        inventoryManager.ConsumeItem(leatherItem, 10);
                        inventoryManager.ConsumeItem(slimeItem, 6);
                        inventoryManager.ConsumeItem(spiderSilkItem, 4);
                        Debug.Log("UP Backpack LV , cost 10 leatherItem �p 6 SlimeItem and 4 SpiderSilkItem");
                        inventoryManager.UnlockSlot(4);
                        inventoryManager.backpackLV++;
                        commodity.bkLV += 1;
                    
                    }
                    else
                    {
                        Debug.Log("Upgrade fales");
                    }
                    break;
            }
        }
        if (commodity.currentItem=="reg")
        {
            int herbNeed=3;
            if (inventoryManager.GetItemCount(herb) >= herbNeed)
            {
                inventoryManager.ConsumeItem(herb, 3);
                Debug.Log("�u���ɹ�");
                PickUpItem(2);
            }
        }
        if(commodity.currentItem=="bomb")
        {
            int gunpowderNeed = 3;
            if (inventoryManager.GetItemCount(gunpowder) >= gunpowderNeed)
            {
                inventoryManager.ConsumeItem(gunpowder, 3);
                Debug.Log("�u���ɹ�");
                PickUpItem(7);
            }
        }
    }

        
}