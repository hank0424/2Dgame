using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en : MonoBehaviour
{
    public GameObject showDmgPrefab;
    public int hp=5;
    void Update()
    {
        if(hp<=0)
        {
            Destroy(this.gameObject);
            Boss1Test.hp-=5;
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("atk"))
        {
            Vector3 down = new Vector3(0, -0.5f, 0);
            GameObject show = Instantiate(showDmgPrefab, (this.transform.position + down) + Vector3.up * 1f, Quaternion.identity);
            show.GetComponent<ShowDmg>().SetDamage(Chara2.atk);
            hp -= Chara2.atk;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查碰到的物體是否擁有 "bullet" 的 tag
        if (collision.gameObject.CompareTag("bullet"))
        {
            Vector3 down = new Vector3(0, -0.5f, 0);
            GameObject show = Instantiate(showDmgPrefab, (this.transform.position + down) + Vector3.up * 1f, Quaternion.identity);
            show.GetComponent<ShowDmg>().SetDamage(Chara2.magic);
            hp -= Chara2.magic;


        }
    }
}
