using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money : MonoBehaviour
{
    public Text w;
    public Text b;
    public Text m;
    public static int money1;
    // Start is called before the first frame update
    void Start()
    {
        money1 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            money1 += 100;
        }
        w.text = ":"+money1;
        b.text = ":" + money1;
        m.text= "³ÖÓÐ½ðî~:" + money1;
    }
}
