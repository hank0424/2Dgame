using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = transform.rotation;
        rot *= Quaternion.Euler(0, 0, 2.5f); // ÿ���� Y �S�� 1 ��
        transform.rotation = rot;
    }
}
