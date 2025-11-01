using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Test : MonoBehaviour
{
    private Animator animator;
    public static int hp = 50;
    public Transform firePoint;
    public float bulletSpeed;
    private float lastXPosition;
    private ball3 ball3;
    private TestAddItem testAddItem;

    [Header("Prefab")]
    public GameObject bulletPrefab;
    public GameObject skllBulletPrefab;

    void Start()
    {
        testAddItem = FindObjectOfType<TestAddItem>();
        animator = GetComponent<Animator>();
        ball3 = GetComponent<ball3>();
        StartCoroutine(AttackCycle());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            print("-");
            hp -= Chara2.magic;

            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }

            if (hp <= 0)
            {
                Destroy(this.gameObject);
                money.money1 += 500;
                testAddItem.PickUpItem(12);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            health.HP--;
        }
    }

    void NormalAtk()
    {
        Vector3 bulletSpawnPosition = firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(Vector2.left);
        }
        Destroy(bullet, 0.6f);
    }

    void SkllAtk()
    {
        if (!ball3.isSklling)
        {
            ball3.SkllMove();
            InvokeRepeating("Find", 0f, 0.3f);
            Invoke("StopSkillFind", 1.2f);
        }
    }

    void StopSkillFind()
    {
        CancelInvoke(nameof(Find));
    }


    void Find()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject skillBullet = Instantiate(skllBulletPrefab, firePoint.position, Quaternion.identity);
        SkllBulletTest sbt = skillBullet.GetComponent<SkllBulletTest>();
        if (sbt != null)
        {
            sbt.FindTargetAndShoot(playerPos);
        }
    }

    IEnumerator ResetHitAnimation()
    {
        animator.SetBool("hit", true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("hit", false);
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            for (int i = 0; i < 11; i++)
            {
                NormalAtk();
                yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
            }
            SkllAtk();
            yield return new WaitForSeconds(4f);
        }
    }
}
