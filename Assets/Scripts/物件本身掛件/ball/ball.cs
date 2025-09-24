using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float minY ;  // �̤p�� Y ��
    public float maxY;  // �̤j�� Y ��
    public float speed = 2f; // ���ʳt��
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

        // �N���� Y �]�w���s�� Y ��
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
