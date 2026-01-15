using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss3area : MonoBehaviour
{
    float lastF, lastF1, lastF2, lastF3;

    public GameObject boss;
    private GameObject bossspawn;

    public Animator a1;
    public GameObject plat1;
    public GameObject plat2;

    [Header("Main Boss Bar")]
    public Image f; // Front
    public Image m; // Middle (Effect)
    public Image b; // Background

    [Header("Dog Bars")]
    public Image f1; public Image m1; public Image b1;
    public Image f2; public Image m2; public Image b2;
    public Image f3; public Image m3; public Image b3;

    public float transtime = 0.5f;
    private Coroutine updateEffect;
    public static bool start2 = false;
    bool start = false;
    bool stage1 = false;
    bool detectStarted = false;

    // -------------------------
    // Player enter area
    // -------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (!detectStarted)
        {
            InvokeRepeating(nameof(bossdectect), 2f, 0.2f);
            detectStarted = true;
        }

        if (!start)
        {
            Invoke(nameof(respawn), 1.3f);
            Invoke(nameof(play), 0.4f);

            ShowMainBar(true);
            start = true;
        }
    }

    // -------------------------
    // Player stay
    // -------------------------
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (start && stage1)
        {
            if (plat1 != null) plat1.SetActive(true);
            if (plat2 != null) plat2.SetActive(true);

            ShowMainBar(false);
            ShowDogBars(true);

            // 檢查 fillAmount 之前也要確保 Image 存在
            if (f1 != null && f1.fillAmount <= 0) HideBar(f1, m1, b1);
            if (f2 != null && f2.fillAmount <= 0) HideBar(f2, m2, b2);
            if (f3 != null && f3.fillAmount <= 0) HideBar(f3, m3, b3);
        }
    }

    // -------------------------
    // Player exit
    // -------------------------
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        CancelInvoke(nameof(bossdectect));
        detectStarted = false;

        stage1 = false;
        start = false;

        if (plat1 != null) plat1.SetActive(false);
        if (plat2 != null) plat2.SetActive(false);

        // 重置數值
        Boss3Test.hp = Boss3Test.bosshp;
        Boss3ChargeDog.OwnHP = 50;
        Boss3ScopeDog.OwnHP = 50;
        Boss3ShootDog.OwnHP = 50;

        if (bossspawn != null)
            Destroy(bossspawn);

        ShowMainBar(false);
        ShowDogBars(false);
    }

    void Start()
    {
        if (plat1 != null) plat1.SetActive(false);
        if (plat2 != null) plat2.SetActive(false);

        ShowMainBar(false);
        ShowDogBars(false);
    }

    void Update()
    {
        hpbar();
        start2 = start;
    }

    void bossdectect()
    {
        if (bossspawn == null && start)
        {
            stage1 = true;
        }
    }

    void play()
    {
        if (a1 != null) a1.SetTrigger("play");
    }

    void respawn()
    {
        bossspawn = Instantiate(boss, new Vector3(44.67f, 38.61f, 0f), Quaternion.identity);
    }

    void hpbar()
    {
        // 增加 null 檢查，避免在 Update 中噴錯
        if (f == null || f1 == null || f2 == null || f3 == null) return;

        f.fillAmount = Mathf.Clamp01(Boss3Test.hp / 200f);

        if (stage1)
        {
            f1.fillAmount = Mathf.Clamp01(Boss3ChargeDog.OwnHP / 70f);
            f2.fillAmount = Mathf.Clamp01(Boss3ScopeDog.OwnHP / 70f);
            f3.fillAmount = Mathf.Clamp01(Boss3ShootDog.OwnHP / 70f);
        }

        // 偵測血量變化觸發平滑效果
        if (f.fillAmount != lastF || f1.fillAmount != lastF1 || f2.fillAmount != lastF2 || f3.fillAmount != lastF3)
        {
            if (updateEffect != null) StopCoroutine(updateEffect);
            updateEffect = StartCoroutine(HealthEffect());

            lastF = f.fillAmount;
            lastF1 = f1.fillAmount;
            lastF2 = f2.fillAmount;
            lastF3 = f3.fillAmount;
        }
    }

    IEnumerator HealthEffect()
    {
        // 預防性檢查
        if (m == null || m1 == null || m2 == null || m3 == null) yield break;

        float s0 = m.fillAmount; float e0 = f.fillAmount;
        float s1 = m1.fillAmount; float e1 = f1.fillAmount;
        float s2 = m2.fillAmount; float e2 = f2.fillAmount;
        float s3 = m3.fillAmount; float e3 = f3.fillAmount;

        float t = 0;
        while (t < transtime)
        {
            t += Time.deltaTime;
            float k = t / transtime;

            if (m != null) m.fillAmount = Mathf.Lerp(s0, e0, k);
            if (m1 != null) m1.fillAmount = Mathf.Lerp(s1, e1, k);
            if (m2 != null) m2.fillAmount = Mathf.Lerp(s2, e2, k);
            if (m3 != null) m3.fillAmount = Mathf.Lerp(s3, e3, k);

            yield return null;
        }

        if (m != null) m.fillAmount = e0;
        if (m1 != null) m1.fillAmount = e1;
        if (m2 != null) m2.fillAmount = e2;
        if (m3 != null) m3.fillAmount = e3;
    }

    // --- UI 顯示與隱藏（強化安全性） ---

    void ShowMainBar(bool on)
    {
        if (f != null) f.enabled = on;
        if (m != null) m.enabled = on;
        if (b != null) b.enabled = on;
    }

    void ShowDogBars(bool on)
    {
        if (f1 != null) f1.enabled = on;
        if (m1 != null) m1.enabled = on;
        if (b1 != null) b1.enabled = on;

        if (f2 != null) f2.enabled = on;
        if (m2 != null) m2.enabled = on;
        if (b2 != null) b2.enabled = on;

        if (f3 != null) f3.enabled = on;
        if (m3 != null) m3.enabled = on;
        if (b3 != null) b3.enabled = on;
    }

    void HideBar(Image a, Image b, Image c)
    {
        if (a != null) a.enabled = false;
        if (b != null) b.enabled = false;
        if (c != null) c.enabled = false;
    }
}