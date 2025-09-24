using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    private Animator animator;
    public static int hp=50;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    private float lastXPosition;


    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("Shoot", 0, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            print("-");
            hp-=Chara2.magic;

            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }

            if (hp <= 0)
            {
                Destroy(this.gameObject);
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

    void Update()
    {
        
        lastXPosition = Mathf.Lerp(lastXPosition, transform.localPosition.x, 0.3f);
        // 沒有需要放在 Update 的內容
    }

    void Shoot()
    {
       
            Vector3 bulletSpawnPosition = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                float currentXPosition = transform.localPosition.x;


                Vector2 bulletDirection = currentXPosition >= lastXPosition ? Vector2.left : Vector2.left;




                bulletScript.SetDirection(bulletDirection);
            }

            Destroy(bullet, 0.6f);
        
    }

  
    

    IEnumerator ResetHitAnimation()
    {
        animator.SetBool("hit", true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("hit", false);
    }

}
