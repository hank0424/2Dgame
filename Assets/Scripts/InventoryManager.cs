using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots; // ���еı�����λ�������زġ����ߡ������ȣ�
    public InventorySlot[] hotbarSlots;    // ��ݙڵĲ�λ�������I 1~9 ��ʹ�ã�
    public GameObject inventoryItemPrefab; // �Á����� UI ��Ʒ�� prefab

    private void Update()
    {
        HandleHotbarInput(); // ÿ���O ����Iݔ��
    }

    /// <summary>
    /// �O ��ݙڰ��I (Alpha1 ~ Alpha9)���K����ʹ�õ���
    /// </summary>
    private void HandleHotbarInput()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                UseHotbarItem(i);
            }
        }
    }

    /// <summary>
    /// ʹ�ÿ�ݙ�ָ����λ�ĵ���
    /// </summary>
    private void UseHotbarItem(int index)
    {
        if (index < 0 || index >= hotbarSlots.Length) return;

        InventorySlot hotbarSlot = hotbarSlots[index];
        InventoryItem itemInSlot = hotbarSlot.GetComponentInChildren<InventoryItem>();

        // �_�Jԓ��λ����Ʒ�Ҟ顸����ʹ�á����
        if (itemInSlot != null && itemInSlot.item.actionType == ActionType.����ʹ��)
        {
            itemInSlot.item.Use();   // ���е���߉݋
            itemInSlot.count--;      // ʹ���ᔵ���p��

            Debug.Log($"ʹ���˿�ݙ� {index + 1} �ĵ���: {itemInSlot.item.name}��ʣ�N����: {itemInSlot.count}");

            if (itemInSlot.count <= 0)
            {
                // �����ıM -> �Ƴ���Ʒ
                Destroy(itemInSlot.gameObject);
                hotbarSlot.ClearSlot();
                Debug.Log($"��ݙ� {index + 1} �ĵ���������K���Ƴ���");
            }
            else
            {
                itemInSlot.RefreshCount(); // ���� UI �@ʾ
            }
        }
        else
        {
            Debug.Log($"��ݙ� {index + 1} �]�п��õĵ��ߣ�");
        }
    }

    /// <summary>
    /// �Lԇ����Ʒ���뱳��
    /// </summary>
    public bool AddItem(Item item, int amount = 1)
    {
        // 1?? �Lԇ�ȶѯB���Ѵ��ڵ�ͬ���Ʒ
        foreach (var slot in inventorySlots)
        {
            if (!slot.isOpened) continue; // δ���i�Ĳ�λ���^

            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < item.maxStack &&
                item.stackable &&
                slot.allowedItemType == item.type)
            {
                int addableCount = Mathf.Min(amount, item.maxStack - itemInSlot.count);
                itemInSlot.count += addableCount;
                itemInSlot.RefreshCount();
                amount -= addableCount;

                Debug.Log($"�ѯB��Ʒ��{item.itemName} ����λ {slot.gameObject.name}������������{addableCount}");

                if (amount <= 0) return true; // ȫ��������
            }
        }

        // 2?? ���߀��ʣ�N���� -> �Lԇ�ŵ��ղ�λ
        foreach (var slot in inventorySlots)
        {
            if (!slot.isOpened) continue;
            if (slot.allowedItemType != item.type) continue; // ֻ�ܷ����S��͵���Ʒ

            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot, amount);
                Debug.Log($"����Ʒ {item.itemName} ��ӵ���λ {slot.gameObject.name}");
                return true;
            }
        }

        Debug.Log($"�]�п��õ� {item.type} ��Ͳ�λ���� {item.itemName}");
        return false;
    }

    /// <summary>
    /// ��ָ����λ��������Ʒ
    /// </summary>
    private void SpawnNewItem(Item item, InventorySlot slot, int amount = 1)
    {
        // �z���λ�Ƿ��ѽ�������
        if (slot.isOccupied || slot.GetComponentInChildren<InventoryItem>() != null)
        {
            Debug.LogWarning($"��λ {slot.gameObject.name} �с��û������Ʒ���o����������Ʒ");
            return;
        }

        // ������Ʒ prefab
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        if (inventoryItem == null)
        {
            Debug.LogError($"���ɵ�����Ʒȱ�� InventoryItem �M������λ��{slot.gameObject.name}");
            Destroy(newItemGo);
            return;
        }

        // ��ʼ����Ʒ
        inventoryItem.InitialiseItem(item);
        inventoryItem.count = Mathf.Min(amount, item.maxStack);
        inventoryItem.RefreshCount();

        // ���²�λ��B
        slot.isOccupied = true;
        slot.SetCurrentItem(inventoryItem);
        inventoryItem.SetCurrentSlot(slot);

        Debug.Log($"��������Ʒ��{item.itemName}��������{inventoryItem.count}����λ��{slot.gameObject.name}");
    }

    /// <summary>
    /// ����ָ����������Ʒ
    /// </summary>
    public bool ConsumeItem(Item targetItem, int amount)
    {
        int totalCount = 0;

        // 1?? Ӌ�㿂����
        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == targetItem)
            {
                totalCount += itemInSlot.count;
            }
        }

        if (totalCount < amount)
        {
            Debug.Log($"����ʧ������Ҫ {amount} �� {targetItem.itemName}����ֻ�� {totalCount}");
            return false;
        }

        // 2?? �_ʼ��ۿ۳�
        int remaining = amount;

        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == targetItem)
            {
                if (itemInSlot.count <= remaining)
                {
                    // �۵�������λ
                    remaining -= itemInSlot.count;
                    Destroy(itemInSlot.gameObject);
                    slot.ClearSlot();
                }
                else
                {
                    // ֻ��һ����
                    itemInSlot.count -= remaining;
                    itemInSlot.RefreshCount();
                    remaining = 0;
                }
            }

            if (remaining <= 0) break;
        }

        Debug.Log($"�ɹ����� {amount} �� {targetItem.itemName}");
        return true;
    }

    /// <summary>
    /// ���i������λ��ÿ�N��ͽ��iָ��������
    /// </summary>
    public void UnlockSlot(int countPerType)
    {
        int openedForMaterial = 0;
        int openedForItem = 0;

        foreach (var slot in inventorySlots)
        {
            if (!slot.isOpened)
            {
                if (slot.allowedItemType == ItemType.�ز� && openedForMaterial < countPerType)
                {
                    slot.isOpened = true;
                    openedForMaterial++;
                    Debug.Log($"���i��һ���زĲۣ�{slot.gameObject.name}");
                }
                else if (slot.allowedItemType == ItemType.���� && openedForItem < countPerType)
                {
                    slot.isOpened = true;
                    openedForItem++;
                    Debug.Log($"���i��һ�����߲ۣ�{slot.gameObject.name}");
                }

                // ����ɷN��Ͷ����i����������
                if (openedForMaterial >= countPerType && openedForItem >= countPerType)
                    break;
            }
        }
    }
}
