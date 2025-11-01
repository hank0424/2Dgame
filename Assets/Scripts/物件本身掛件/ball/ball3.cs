using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball3 : MonoBehaviour
{
    public float radius = 1f;      // 圓的半徑
    public float speed = 1f;       // 圓周運動的速度
    private float LX;
    private float LY;
    private float angle = 0;      // 角度
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
        // 使用正弦和餘弦函數計算物件在圓上的位置
        float x = LX + radius * Mathf.Cos(angle);             // 在 X 軸上保持不變
        float y = LY + radius * Mathf.Sin(angle);
        // 更新物件的位置
        transform.position = new Vector3(x, y, 0);
        // 增加角度以實現圓周運動
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
