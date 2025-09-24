using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_trap : MonoBehaviour
{
    public Animator fire;
    private Collider2D box;
    public float zero;
    public float two;
    public float delay;
    // Start is called before the first frame update
    public static bool delete = false;
    void Start()
    {
        box = gameObject.GetComponent<Collider2D>();

        Invoke("reapt", delay);
         
    }

    // Update is called once per frame
    void Update()
    {
        if(delete==true)
        {
            Destroy(this.gameObject);
        }
    }
    void reapt()
    {
        InvokeRepeating("FireTrigger", zero, two);
    }
    void FireTrigger()
    {
        fire.SetTrigger("fire");
        box.enabled = true;

        StartCoroutine(DisableColliderAfterDelay(0.5f));
    }

    IEnumerator DisableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        box.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            health.HP--;
        }
    }
}
