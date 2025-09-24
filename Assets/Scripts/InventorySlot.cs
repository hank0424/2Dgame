using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ������λ (Slot)���Á������Ʒ (InventoryItem)
// �^�� IDropHandler �� ���Խ��ա���ҷ�Y���r�����¼�
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool isOccupied = false; // �Ƿ��ѽ�����Ʒ
    public bool isOpened = true;    // ��λ�Ƿ��_������Щ���ӿ��ܱ��iס��
    private InventoryItem currentItem; // ��λ�Ȯ�ǰ����Ʒ

    [Header("Slot Settings")]
    public ItemType allowedItemType; // �@����λ���S����Ʒ��� (����ֻ�ܷ�����)

    // ===== �O���c�@ȡ��Ʒ =====
    public void SetCurrentItem(InventoryItem item)
    {
        currentItem = item;
        isOccupied = (currentItem != null);
        Debug.Log($"��λ {gameObject.name} �� currentItem ���O�Þ� {(currentItem != null ? currentItem.item.itemName : "null")}��isOccupied={isOccupied}");
    }

    public InventoryItem GetCurrentItem()
    {
        return currentItem;
    }

    // ===== �Ϸ��¼�̎�� =====
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            Debug.Log("�τ������ null");
            return;
        }

        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        // 1?? ��C��Ʒ�Ƿ���Ч & ����Ƿ����S
        if (draggedItem == null || draggedItem.item == null || draggedItem.item.type != allowedItemType)
        {
            Debug.Log($"�τӵ���Ʒ�oЧ����Ͳ�ƥ��");
            return;
        }

        // �_����ҷ��Ʒ��ӛ䛡�ԭʼ��λ��
        if (draggedItem.parentAfterDrag == null)
        {
            Debug.Log("parentAfterDrag �� null���o���τ�");
            return;
        }

        // 2?? �z���λ�Ƿ��_��
        if (!isOpened)
        {
            // �����λδ�_�� �� ��Ʒ����ԭλ
            draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
            draggedItem.transform.position = draggedItem.parentAfterDrag.position;
            Debug.Log("δ�_������Ʒ����ԭλ");
            return;
        }

        InventorySlot originalSlot = draggedItem.parentAfterDrag.GetComponent<InventorySlot>();
        if (originalSlot == null)
        {
            draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
            draggedItem.transform.position = draggedItem.parentAfterDrag.position;
            Debug.Log("ԭ��λ�oЧ����Ʒ����ԭλ");
            return;
        }

        // 3?? �_����λ��Bһ����
        if (isOccupied && currentItem == null)
        {
            Debug.LogWarning($"��λ {gameObject.name} ��B������isOccupied �� true �� currentItem �� null �� ���à�B");
            isOccupied = false;
        }

        // 4?? �����λ�ǿյ� �� ֱ�ӷ��M��
        if (!isOccupied)
        {
            originalSlot.ClearSlot(); // ���ԭ��λ

            draggedItem.parentAfterDrag = transform;
            draggedItem.transform.SetParent(transform);
            draggedItem.transform.position = transform.position;

            isOccupied = true;
            SetCurrentItem(draggedItem);
            draggedItem.SetCurrentSlot(this);

            Debug.Log($"��Ʒ {draggedItem.item.itemName} ���õ���λ {gameObject.name}");
        }
        else
        {
            // 5?? �����λ�ѽ�����Ʒ �� �z��ѯB�򽻓Q
            InventoryItem targetItem = GetCurrentItem();

            if (targetItem == null || targetItem.item == null)
            {
                Debug.LogWarning($"Ŀ�˲�λ {gameObject.name} �� currentItem �� item �� null �� ����");
                ClearSlot();
                originalSlot.ClearSlot();

                draggedItem.parentAfterDrag = transform;
                draggedItem.transform.SetParent(transform);
                draggedItem.transform.position = transform.position;

                isOccupied = true;
                SetCurrentItem(draggedItem);
                draggedItem.SetCurrentSlot(this);
                return;
            }

            // 5a?? �����Ʒ��ͬ & �ɶѯB �� �ρ�
            if (targetItem.item != null && draggedItem.item == targetItem.item && draggedItem.item.stackable)
            {
                int totalCount = draggedItem.count + targetItem.count;
                int maxStack = draggedItem.item.maxStack;

                if (totalCount <= maxStack)
                {
                    // ? ȫ���ρ�
                    targetItem.count = totalCount;
                    targetItem.RefreshCount();
                    originalSlot.ClearSlot();
                    Destroy(draggedItem.gameObject);
                    Debug.Log($"�ρ���Ʒ��{targetItem.item.itemName}��������{targetItem.count}");
                }
                else
                {
                    // ?? ���^���ѯB �� ���ֺρ�
                    int addableCount = maxStack - targetItem.count;
                    if (addableCount > 0)
                    {
                        targetItem.count += addableCount;
                        targetItem.RefreshCount();
                        draggedItem.count -= addableCount;
                        draggedItem.RefreshCount();
                        Debug.Log($"���ֺρ� �� {targetItem.item.itemName} �ѯB�� {targetItem.count}��ʣ�N {draggedItem.count}");
                    }
                    // ʣ�N��Ʒ��ԭλ
                    draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
                    draggedItem.transform.position = draggedItem.parentAfterDrag.position;
                }
            }
            else
            {
                // 5b?? ��ͬ��Ʒ�򲻿ɶѯB �� �Lԇ���Q
                if (originalSlot.allowedItemType == targetItem.item.type)
                {
                    // ? ���Qλ��
                    draggedItem.transform.SetParent(transform);
                    draggedItem.transform.position = transform.position;
                    draggedItem.SetCurrentSlot(this);

                    targetItem.transform.SetParent(originalSlot.transform);
                    targetItem.transform.position = originalSlot.transform.position;
                    targetItem.SetCurrentSlot(originalSlot);

                    // ���²�λ��B
                    SetCurrentItem(draggedItem);
                    originalSlot.SetCurrentItem(targetItem);

                    Debug.Log($"���Q��Ʒ��{draggedItem.item.itemName} ? {targetItem.item.itemName}");
                }
                else
                {
                    // ? ���ܽ��Q �� ��Ʒ����ԭλ
                    draggedItem.transform.SetParent(draggedItem.parentAfterDrag);
                    draggedItem.transform.position = draggedItem.parentAfterDrag.position;
                    Debug.Log("��Ʒ��Ͳ�ƥ�䣬����ԭλ");
                }
            }
        }
    }

    // ��ղ�λ (���à�B)
    public void ClearSlot()
    {
        isOccupied = false;
        currentItem = null;
        Debug.Log($"��λ {gameObject.name} �ѱ����");
    }
}
