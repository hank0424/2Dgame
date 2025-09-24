using UnityEngine;

public class TestAddItem : MonoBehaviour
{
    public InventoryManager inventoryManager; // ��Ʒ��������ؓ؟��Ʒ���������ġ����i��
    public Item[] PickUpWhatItems;            // ��ʰȡ����Ʒ��Σ�����Ё�ģ�M����ܓ쵽�Ė|����
    public Item leatherItem;                  // ��Ƥ��@��ָ����Ʒ�������������õ���

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
        // ���O������Ҫ 4 ��Ƥ��
        if (inventoryManager.ConsumeItem(leatherItem, 4))
        {
            Debug.Log("�ɹ����������������� 4 ��Ƥ��");
            inventoryManager.UnlockSlot(2); // ���i���ز� +2������ +2
        }
        else
        {
            Debug.Log("����ʧ����Ƥ�ﲻ��");
        }
    }
}
