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

    public Image f, m, b;
    public Image f1, m1, b1;
    public Image f2, m2, b2;
    public Image f3, m3, b3;

    public float transtime = 0.5f;
    private Coroutine update;
    public static bool start2=false;
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
            plat1.SetActive(true);
            plat2.SetActive(true);

            ShowMainBar(false);
            ShowDogBars(true);

            if (f1.fillAmount <= 0) HideBar(f1, m1, b1);
            if (f2.fillAmount <= 0) HideBar(f2, m2, b2);
            if (f3.fillAmount <= 0) HideBar(f3, m3, b3);
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

        plat1.SetActive(false);
        plat2.SetActive(false);

        Boss3Test.hp = Boss3Test.bosshp;
        Boss3ChargeDog.OwnHP = 50;
        Boss3ScopeDog.OwnHP = 50;
        Boss3ShootDog.OwnHP = 50;

        if (bossspawn != null)
            Destroy(bossspawn);

        ShowMainBar(false);
        ShowDogBars(false);
    }

    // -------------------------
    void Start()
    {
        plat1.SetActive(false);
        plat2.SetActive(false);

        ShowMainBar(false);
        ShowDogBars(false);
    }

    // -------------------------
    void Update()
    {
        hpbar();
        start2 = start;
    }

    // -------------------------
    void bossdectect()
    {
        if (bossspawn == null && start)
        {
            stage1 = true;
        }
    }

    void play()
    {
        a1.SetTrigger("play");
    }

    void respawn()
    {
        bossspawn = Instantiate(boss, new Vector3(44.67f, 38.61f, 0f), Quaternion.identity);
    }

    // -------------------------
    void hpbar()
    {
        f.fillAmount = Mathf.Clamp01(Boss3Test.hp /200f);

        if (stage1)
        {
            f1.fillAmount = Mathf.Clamp01(Boss3ChargeDog.OwnHP / 70f);
            f2.fillAmount = Mathf.Clamp01(Boss3ScopeDog.OwnHP / 70f);
            f3.fillAmount = Mathf.Clamp01(Boss3ShootDog.OwnHP / 70f);
        }

        if (f.fillAmount != lastF ||
            f1.fillAmount != lastF1 ||
            f2.fillAmount != lastF2 ||
            f3.fillAmount != lastF3)
        {
            if (update != null)
                StopCoroutine(update);

            update = StartCoroutine(HealthEffect());

            lastF = f.fillAmount;
            lastF1 = f1.fillAmount;
            lastF2 = f2.fillAmount;
            lastF3 = f3.fillAmount;
        }
    }

    // -------------------------
    IEnumerator HealthEffect()
    {
        float s0 = m.fillAmount;
        float e0 = f.fillAmount;

        float s1 = m1.fillAmount;
        float e1 = f1.fillAmount;

        float s2 = m2.fillAmount;
        float e2 = f2.fillAmount;

        float s3 = m3.fillAmount;
        float e3 = f3.fillAmount;

        float t = 0;

        while (t < transtime)
        {
            t += Time.deltaTime;
            float k = t / transtime;

            m.fillAmount = Mathf.Lerp(s0, e0, k);
            m1.fillAmount = Mathf.Lerp(s1, e1, k);
            m2.fillAmount = Mathf.Lerp(s2, e2, k);
            m3.fillAmount = Mathf.Lerp(s3, e3, k);

            yield return null;
        }

        m.fillAmount = e0;
        m1.fillAmount = e1;
        m2.fillAmount = e2;
        m3.fillAmount = e3;
    }

    // -------------------------
    void ShowMainBar(bool on)
    {
        f.enabled = on;
        m.enabled = on;
        b.enabled = on;
    }

    void ShowDogBars(bool on)
    {
        f1.enabled = on;
        m1.enabled = on;
        b1.enabled = on;

        f2.enabled = on;
        m2.enabled = on;
        b2.enabled = on;

        f3.enabled = on;
        m3.enabled = on;
        b3.enabled = on;
    }

    void HideBar(Image a, Image b, Image c)
    {
        a.enabled = false;
        b.enabled = false;
        c.enabled = false;
    }

}
