using UnityEngine;

public class text_set : MonoBehaviour
{
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "Default"; // �����Լ��� Sorting Layer ���Q
        renderer.sortingOrder = 10; // ���@����ֵ׌������ǰ���@ʾ
    }
}