using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball2 : MonoBehaviour
{
    public float minX;  // �̤p�� Y ��
    public float maxX;  // �̤j�� Y ��
    public float speed = 2f; // ���ʳt��
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

        // �N���� Y �]�w���s�� Y ��
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
