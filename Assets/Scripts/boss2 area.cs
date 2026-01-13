using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss2area : MonoBehaviour
{
    public GameObject boss;
   
    public GameObject unlock;
    public Animator animator;
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
        if(collision.CompareTag("Player")&&start==false)
        {
            bossspawn=Instantiate(boss, new Vector3(95.8f, 31.546f,0f), Quaternion.identity);

            f.enabled = true;
            m.enabled = true;
            b.enabled = true;
            start = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Boss2.hp = Boss2.bosshp;
            Destroy(bossspawn);
            DestroySlimeLayerObjects();
            start = false;
        }
    }
    void Start()
    {
        unlock.SetActive(false);
        f.enabled = false;
        m.enabled = false;
        b.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
         hpbar();
        if (Boss2.clear==true)
        {
            unlock.SetActive(true);
            animator.SetBool("IsOpened", true);
            Destroy(this.gameObject);
            Destroy(f);
            Destroy(m);
            Destroy(b);
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
            f.fillAmount = Mathf.Clamp01(Boss2.hp / 150f); 

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
    void DestroySlimeLayerObjects()
    {
        int slimeLayer = LayerMask.NameToLayer("slime");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == slimeLayer)
            {
                Destroy(obj);
            }
        }
    }
}
