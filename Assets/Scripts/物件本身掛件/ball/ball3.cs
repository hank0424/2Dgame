using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball3 : MonoBehaviour
{
    public float radius = 1f;
    public float speed = 3f;

    float angle;
    float LX, LY;

    // 移除 static，讓每顆球獨立運作
    public bool isSklling = false;
    private Coroutine skillRoutine;

    void Start()
    {
        // 記錄初始中心點
        LX = transform.localPosition.x;
        LY = transform.localPosition.y;
        angle = 0f;
    }

    void Update()
    {
        if (!isSklling)
        {
            NormalMove();
        }
    }

    void NormalMove()
    {
        // 使用圓周公式
        float x = LX + radius * Mathf.Cos(angle);
        float y = LY + radius * Mathf.Sin(angle);
        transform.localPosition = new Vector3(x, y, 0);

        // 角度隨時間增加
        angle += speed * Time.deltaTime;

        // 保持角度在 0 ~ 2π 之間，避免數值過大產生誤差
        if (angle > Mathf.PI * 2) angle -= Mathf.PI * 2;
    }

    public void SkllMove()
    {
        // 確保不會重複啟動
        if (isSklling) return;
        skillRoutine = StartCoroutine(SkillMoveLogic());
    }

    IEnumerator SkillMoveLogic()
    {
        isSklling = true;

        Vector3 startPos = transform.localPosition;
        Vector3 forwardPos = startPos + Vector3.left * 6f;

        // 1. 衝出去
        yield return MoveTo(startPos, forwardPos, 0.75f);

        // 2. 停頓
        yield return new WaitForSeconds(1f);

        // 3. 回到原位 (圓周上的點)
        yield return MoveTo(forwardPos, startPos, 0.4f);

        
        // 計算相對於圓心 (LX, LY) 的向量
        Vector2 offset = new Vector2(transform.localPosition.x - LX, transform.localPosition.y - LY);
        angle = Mathf.Atan2(offset.y, offset.x);

        isSklling = false;
        skillRoutine = null;
    }

    // 輔助方法：處理平滑移動，減少重複程式碼
    IEnumerator MoveTo(Vector3 start, Vector3 end, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(start, end, t / duration);
            yield return null;
        }
        transform.localPosition = end;
    }
}
