using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplat : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pointA; // A �c
    public Transform pointB; // B �c
    public float speed = 1.0f; // �Ƅ��ٶ�

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1); // �� 0 �� 1 ֮�g����
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }
}
