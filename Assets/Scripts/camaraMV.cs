using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraMV : MonoBehaviour
{
    public float speed = 1.0f;
    private int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-7.0f, transform.position.y,-2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime*dir);
        if (transform.position.x<-7.0f||transform.position.x>7.0f)
        {
            dir = dir * (-1);
        }
    }
}
