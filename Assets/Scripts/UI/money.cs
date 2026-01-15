using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money : MonoBehaviour
{
    public Text w1;
    public Text b1;

    public Text w2;
    public Text b2;

    public Text w3;
    public Text b3;

    public Text m;
    public Text m1;
    public Text m2;
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
            Chara2.atk++;
            Chara2.magic++;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            skill_list.attack = true;
            skill_list.magic = true;
           
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Chara2.fireupdate = true;
        }
        
        w1.text = ":"+money1;
        b1.text = ":" + money1;

        w2.text = ":" + Chara2.atk;
        b2.text = ":" + Chara2.atk;

        w3.text = ":" + Chara2.magic;
        b3.text = ":" + Chara2.magic;

        m.text= "持有金額:" + money1;
        m1.text = "持有金額:" + money1;
        m2.text = "持有金額:" + money1;


    }
}
