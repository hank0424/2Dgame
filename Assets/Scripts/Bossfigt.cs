using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{
    public GameObject energy;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Image F;
    public Image M;
    public Image B;
    private int randomnum;

    public float transtime = 0.5f;
    private Coroutine update;


    private void Start()
    {

        F.enabled = false;
        M.enabled = false;
        B.enabled = false;
    }
    void Update()
    {
        randomnum = Random.Range(1, 5);
        HpBar();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Boss1Test.hp -= 1; // ���� BossScript �еľ�̬���� hp
        }
    }

    void HpBar()
    {
       
        if(health.HP<=0||boss.hp<=0)
        {

            F.enabled = false;
            M.enabled = false;
            B.enabled = false;
        }
        // ȷ�� BossScript.hp ��������Χ
        F.fillAmount = Mathf.Clamp01(Boss1Test.hp / 50f); // ������� HP �� 50

        if (update != null)
        {
            StopCoroutine(update);
        }
        update = StartCoroutine(HealthEffect());
    }

    private IEnumerator HealthEffect()
    {
        float startFill = M.fillAmount;
        float endFill = F.fillAmount;
        float timeElapsed = 0f;

        while (timeElapsed < transtime)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / transtime);
            M.fillAmount = Mathf.Lerp(startFill, endFill, t);
            yield return null;
        }

        M.fillAmount = endFill;
    }

private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
        {
            F.enabled =true;
            M.enabled = true;
            B.enabled = true;
            GameObject[] existingEnergy = GameObject.FindGameObjectsWithTag("energy");
            if (existingEnergy.Length < 1)
            {
                switch (randomnum)
                {
                    case 1:
                        Instantiate(energy, pos1.position, Quaternion.identity); // ��λ��1��������
                        break;
                    case 2:
                        Instantiate(energy, pos2.position, Quaternion.identity); // ��λ��2��������
                        break;
                    case 3:
                        Instantiate(energy, pos3.position, Quaternion.identity); // ��λ��3��������
                        break;
                    case 4:
                        Instantiate(energy, pos4.position, Quaternion.identity); // ��λ��4��������
                        break;
                }
            }
        }
        
        }
}
