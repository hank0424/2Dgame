using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

// InventoryItem: ��ʾ����Ʒ UI �����
// �^�� MonoBehaviour �K�����Ă��¼����棺
// - IBeginDragHandler: ��ҷ�_ʼ
// - IDragHandler: ��ҷ��
// - IEndDragHandler: ��ҷ�Y��
// - IPointerClickHandler: �����c��
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("UI")]
    public Image image;       // ��Ʒ�Dʾ
    public Text countText;    // ��������
    public Text itemnameText; // ��Ʒ���Q����

    // ===== �[��߉݋�Ù�λ =====
    [HideInInspector] public Item item;  // ������ Item �Y�� (ScriptableObject)
    [HideInInspector] public int count = 1; // ���Д���
    [HideInInspector] public Transform parentAfterDrag; // ��ҷ�Y����Ҫ��ȥ��ԭʼ�����
    public Canvas parentCanvas; // �Á�Ӌ����ҷ�r UI ������ (���Ҫ�� Canvas)

    private InventorySlot currentSlot; // �o�Ŀǰ���Ă���λ��

    // ��ʼ����Ʒ (�O���D�� & ���Q & ����)
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
        RefreshName();
    }

    // ˢ�����@ʾ
    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActiv = count > 1; // ���������� 1 ���@ʾ���� (����ˎˮ *5)
        countText.gameObject.SetActive(textActiv);
    }

    // ˢ�����Q�@ʾ
    public void RefreshName()
    {
        itemnameText.text = item.itemName;
    }

    // �O����ǰ���ڵĲ�λ
    public void SetCurrentSlot(InventorySlot slot)
    {
        currentSlot = slot;
        if (slot != null)
        {
            slot.isOccupied = true; // ���²�λ��B�顸�ѱ����á�
        }
    }

    // ��ҷ�_ʼ�r�|�l
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false; // ���r�P�] Raycast������ UI �ж��ɔ_
        parentAfterDrag = transform.parent; // ӛ���ҷǰ�ĸ���� (ԭʼ��λ)

        if (parentCanvas == null)
        {
            parentCanvas = GetComponentInParent<Canvas>(); // ���߀�]ָ�� Canvas�����Ԅ�������� Canvas
        }

        // ����Ʒ�Ƅӵ� Canvas �£��_����ҷ�r���@ʾ�����ό�
        transform.SetParent(parentCanvas.transform);
    }

    // ��ҷ�У�ÿһ������λ�õ�����λ��
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    // ��ҷ�Y���r�|�l
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true; // �����_�� Raycast

        // ����Y���r��Ȼ�� Canvas �£�����]�з��M�κβ�λ
        if (transform.parent == parentCanvas.transform)
        {
            // ߀ԭ��ԭ���Ĳ�λ
            transform.SetParent(parentAfterDrag);
            transform.position = parentAfterDrag.position;
            Debug.Log("��ק�Y������δ�ŵ���Ч��λ����Ʒ����ԭλ");
        }
    }

    // �c���¼� (Ŀǰ�O�������I�h����Ʒ)
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (currentSlot != null)
            {
                currentSlot.ClearSlot(); // ��ղ�λ��B
            }
            Destroy(gameObject); // �h����Ʒ UI
        }
    }
}
