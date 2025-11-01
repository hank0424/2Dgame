using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball3 : MonoBehaviour
{
    public float radius = 1f;      // �A�İ돽
    public float speed = 1f;       // �A���\�ӵ��ٶ�
    private float LX;
    private float LY;
    private float angle = 0;      // �Ƕ�
    public static bool isSklling = false;
    private void Start()
    {
        LX = transform.localPosition.x;
        LY = transform.localPosition.y;
    }
    void Update()
    {
        if (!isSklling)
        {
            NormalMove();
        }
    }

    public void SkllMove()
    {
        isSklling = true;
        StartCoroutine(SkillMoveLogic());
    }
    public void NormalMove()
    {
        isSklling = false;
        // ʹ�����Һ��N�Һ���Ӌ������ڈA�ϵ�λ��
        float x = LX + radius * Mathf.Cos(angle);             // �� X �S�ϱ��ֲ�׃
        float y = LY + radius * Mathf.Sin(angle);
        // ���������λ��
        transform.position = new Vector3(x, y, 0);
        // ���ӽǶ��Ԍ��F�A���\��
        angle += speed * Time.deltaTime;
    }
    IEnumerator SkillMoveLogic()
    {
        Vector3 startPos = transform.position;
        Vector3 forwardPos = startPos + transform.right * -6f;
        float moveTime = 0.75f;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, forwardPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = forwardPos;
        yield return new WaitForSeconds(1f);

        elapsed = 0f;
        moveTime = 0.4f;
        while (elapsed < moveTime)
        {

            transform.position = Vector3.Lerp(forwardPos, startPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;
        isSklling = false;
    }
}
