using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletchange : MonoBehaviour
{
    private Animator an1;
    // Start is called before the first frame update
    void Start()
    {
        an1 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Chara2.fireupdate == true)
        {
            an1.SetBool("fire", true);
        }
        if (Chara2.darkupdate == true)
        {
            an1.SetBool("fire", false);
            an1.SetBool("dark", true);
        }
    }
}
