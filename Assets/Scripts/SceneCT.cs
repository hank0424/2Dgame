using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneCT : MonoBehaviour
{public static int agree=0;
 public  int scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
       if(scene==0&&agree==1)
        {
            SceneManager.LoadScene("Outdoor");
            
        }

      


        if (scene==1&&agree==1)
        {
            SceneManager.LoadScene("Basement");
            
        }
    }
    
    void Update()
    {
      
    }
}
