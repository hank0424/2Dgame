using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball2 : MonoBehaviour
{
    public float minX;  // 最小的 Y 值
    public float maxX;  // 最大的 Y 值
    public float speed = 2f; // 移動速度
    // Start is called before the first frame update
    void Start()
    {
        minX = transform.position.x;
        maxX = transform.position.x + 5;
    }

    // Update is called once per frame
    void Update()
    {
        float newX   = Mathf.PingPong(Time.time * speed, maxX - minX) + minX;

        // 將物件的 Y 設定為新的 Y 值
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
