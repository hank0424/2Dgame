using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss3area : MonoBehaviour
{
    public GameObject boss;


    
    public Image f;
    public Image m;
    public Image b;
    private GameObject bossspawn;
    public float transtime = 0.5f;
    private Coroutine update;
    private bool start = false;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && start == false)
        {
            Invoke("respawn", 1f);

            f.enabled = true;
            m.enabled = true;
            b.enabled = true;
            start = true;
        }
    }
    void respawn()
    {
        bossspawn = Instantiate(boss, new Vector3(44.67f, 38.81f, 0f), Quaternion.identity);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Destroy(bossspawn);
           
            start = false;
        }
    }
    void Start()
    {
     
        f.enabled = false;
        m.enabled = false;
        b.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar();
        if (Boss3Test.clear == true)
        {
        

        }
    }
    void hpbar()
    {
        if (health.HP <= 0 || bossspawn == null)
        {

            f.enabled = false;
            m.enabled = false;
            b.enabled = false;
        }
        f.fillAmount = Mathf.Clamp01(Boss3Test.hp / 80f);

        if (update != null)
        {
            StopCoroutine(update);
        }
        update = StartCoroutine(HealthEffect());

    }
    private IEnumerator HealthEffect()
    {
        float startFill = m.fillAmount;
        float endFill = f.fillAmount;
        float timeElapsed = 0f;

        while (timeElapsed < transtime)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / transtime);
            m.fillAmount = Mathf.Lerp(startFill, endFill, t);
            yield return null;
        }

        m.fillAmount = endFill;
    }
    
}
