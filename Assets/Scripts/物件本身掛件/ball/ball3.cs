using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball3 : MonoBehaviour
{
    public float radius =1f;      // �ꪺ�b�|
    public float speed = 1f;       // ��P�B�ʪ��t��
    private float LX;
    private float LY;
    private float angle =0;      // ����
    private void Start()
    {
        LX = transform.localPosition.x;
        LY= transform.localPosition.y;
    }
    void Update()
    {
        // �ϥΥ����M�l����ƭp�⪫��b��W����m
        float x = LX+radius * Mathf.Cos(angle);             // �b X �b�W�O������
        float y = LY+radius * Mathf.Sin(angle);
        


        // ��s���󪺦�m
        transform.position = new Vector3(x,  y, 0);

        // �W�[���ץH��{��P�B��
        angle += speed * Time.deltaTime;
    }

}