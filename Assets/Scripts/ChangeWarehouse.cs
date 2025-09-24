using UnityEngine;
using UnityEngine.UI;

public class ChangeWarehouse : MonoBehaviour
{
    // �������Ђ}������ GameObject�����粻ͬ��� UI ��壩
    public GameObject[] warehouseViews;

    /// <summary>
    /// �ГQ��ָ���}�����
    /// </summary>
    /// <param name="warehouseIndex">Ҫ�ГQ�Ă}���������� 0 �_ʼ��</param>
    public void SwitchWarehouse(int warehouseIndex)
    {
        // 1?? �Ȱ����Ђ}������P�]
        foreach (GameObject warehouse in warehouseViews)
        {
            warehouse.SetActive(false);
        }

        // 2?? ��������ںϷ������ȣ��_�������}��
        if (warehouseIndex >= 0 && warehouseIndex < warehouseViews.Length)
        {
            warehouseViews[warehouseIndex].SetActive(true);
        }
    }
}
