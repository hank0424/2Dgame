using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float minY ;  // 最小的 Y 值
    public float maxY;  // 最大的 Y 值
    public float speed = 2f; // 移動速度
    // Start is called before the first frame update
    void Start()
    {
        minY = transform.position.y;
        maxY = transform.position.y+5;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.PingPong(Time.time * speed, maxY - minY) + minY;

        // 將物件的 Y 設定為新的 Y 值
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
