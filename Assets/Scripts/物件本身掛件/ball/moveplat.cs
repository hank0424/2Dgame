using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplat : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pointA; // A 點
    public Transform pointB; // B 點
    public float speed = 1.0f; // 移動速度

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1); // 在 0 和 1 之間反覆
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }
}
