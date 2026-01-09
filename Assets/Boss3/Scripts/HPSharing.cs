using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HPSharing : MonoBehaviour
{
    public int sharedHP = 50;
    public Boss3ShootDog dogs1;
    public Boss3ChargeDog dogs2;
    public Boss3ScopeDog dogs3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dogs1&&dogs2&&dogs3!=null)
        {
            dogs1 = GameObject.Find("Boss3 ShootDog(Clone)").GetComponent<Boss3ShootDog>();
            dogs2 = GameObject.Find("Boss3 ChargeDog(Clone)").GetComponent<Boss3ChargeDog>();
            dogs3 = GameObject.Find("Boss3 ScopeDog(Clone)").GetComponent<Boss3ScopeDog>();
            print(sharedHP);
        }
       
    }

    public void TakeDamage(int dmg)
    {
        sharedHP -= dmg;
        if (sharedHP <= 0)
        {
            KillAllDogs();
            Destroy(this.gameObject);
        }
    }

    void KillAllDogs()
    {
        if (dogs1 != null)
            dogs1.Die();
        if (dogs2 != null)
            dogs2.Die();
        if (dogs3 != null)
            dogs3.Die();
    }
}