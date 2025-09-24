using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_trap : MonoBehaviour
{
    public float zero;
    public float two;
    public float delay;
    private Vector3 pos;
    public GameObject firepos;
    public GameObject arrows;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(firepos.transform.position.x,firepos.transform.position.y,firepos.transform.position.z);
        Invoke("reapt", delay);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void reapt()
    {
        InvokeRepeating("arrow", zero, two);
    }
    void arrow()
    {
        Instantiate(arrows, pos, arrows.transform.rotation);
    }

}
