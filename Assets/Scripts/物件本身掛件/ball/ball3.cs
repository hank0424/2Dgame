using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball3 : MonoBehaviour
{
    public float radius =1f;      // 圓的半徑
    public float speed = 1f;       // 圓周運動的速度
    private float LX;
    private float LY;
    private float angle =0;      // 角度
    private void Start()
    {
        LX = transform.localPosition.x;
        LY= transform.localPosition.y;
    }
    void Update()
    {
        // 使用正弦和餘弦函數計算物件在圓上的位置
        float x = LX+radius * Mathf.Cos(angle);             // 在 X 軸上保持不變
        float y = LY+radius * Mathf.Sin(angle);
        


        // 更新物件的位置
        transform.position = new Vector3(x,  y, 0);

        // 增加角度以實現圓周運動
        angle += speed * Time.deltaTime;
    }

}