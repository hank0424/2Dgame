using UnityEngine;

public class text_set : MonoBehaviour
{
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "Default"; // 或你自己的 Sorting Layer 名稱
        renderer.sortingOrder = 10; // 改這個數值讓文字在前面顯示
    }
}