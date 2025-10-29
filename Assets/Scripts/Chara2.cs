using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chara2 : MonoBehaviour
{

    public static float moveSpeed = 4f;
    public static float jumpForce = 4f;
    public static int remainingJumps = 2;
    public bool isGrounded = true;
    public static bool doubleJ;
    //
    public static Animator animator;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;
    public Animator animator5;
    //
    public static int hp;
    public static bool shooting = false;
    //
    public GameObject bulletPrefab;
    public GameObject emptyPrefab;
    public GameObject emptyPrefab2;
    //
    public Transform firePoint;
    private float lastXPosition;
    private int dashtime = 1;
    public static int atk = 1;
    public static int magic = 2;
    public static float atkspeed = 0.4f;
    public static float atkspeedRCD = 0;
    public BoxCollider2D Player;
    public BoxCollider2D atkbox;
    void Start()
    {
        animator = GetComponent<Animator>();
        atkbox.enabled = !atkbox.enabled;

    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("anibullet"))
        {

            health.HP--;


        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    
        // 檢測是否碰到地面
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            dashtime = 1;
            remainingJumps = 2; // 在地面時重置跳躍次數
            moveSpeed = 3f;
        }
        if (collision.collider.CompareTag("enemy"))
        {
            health.HP--;

        }

        {
            if (collision.gameObject.CompareTag("anibullet"))
            {

                health.HP--;


            }
        }
    }



    void Update()
    {
        anitest();
        attack();
        if (health.HP <= 0 && SpawnPT.spawn_active == 0)
        {
            SceneManager.LoadScene("Basement");
            health.HP = health.maxHp;
        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 1)
        {
            this.gameObject.transform.position = new Vector3(12.5100002f, -25.9899998f, 0);
            health.HP = health.maxHp;

        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 2)
        {
            this.gameObject.transform.position = new Vector3(44.4599991f, -26.132f, 0);
            health.HP = health.maxHp;
        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 3)
        {
            this.gameObject.transform.position = new Vector3(74.655f, 27.988f, 0);
            health.HP = health.maxHp;
        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 4)
        {
            this.gameObject.transform.position = new Vector3(104.24f, 40.5f, 0);
            health.HP = health.maxHp;
        }
        lastXPosition = Mathf.Lerp(lastXPosition, transform.localPosition.x, 0.3f);
        float currentXPosition = transform.localPosition.x;
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

        movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Z) && remainingJumps > 0)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)&&Input.GetKey(KeyCode.LeftArrow)&& isGrounded == false&&dashtime==1)
        {
            Ldash();
            dashtime -= 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightArrow) && isGrounded == false&& dashtime == 1)
        {
            Rdash();
            dashtime -= 1;
        }



        UpdateAnimatorParameters(horizontalInput);
    }
    void anitest()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
             animator5.SetTrigger("testT");
        }
    }
    void Shoot()
    {
        if (health.mana > 0&&shooting==true)
        {
            health.mana--;
            Vector3 bulletSpawnPosition = new Vector3(firePoint.position.x, firePoint.position.y - 0.08f, firePoint.position.z);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                float currentXPosition = transform.localPosition.x;


                Vector2 bulletDirection = currentXPosition >= lastXPosition ? Vector2.left : Vector2.left;


                if (currentXPosition >= lastXPosition)
                {

                    bullet.transform.Rotate(0f, 180f, 0f);
                }


                bulletScript.SetDirection(bulletDirection);
            }

            Destroy(bullet, 0.6f);
        }

    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
        remainingJumps -= 2;

        // 如果需要，在這裡添加其他處理跳躍的邏輯
        if (doubleJ == true)
        {
            remainingJumps++;
        }
        // 注意：這裡不再設定動畫觸發器
        isGrounded = false;
    }

    void UpdateAnimatorParameters(float horizontalInput)
    {
        if (horizontalInput > 0f)
        {
            animator.SetInteger("walk", 0);
            animator.speed = 1;
        }
        else if (horizontalInput < 0f)
        {
            animator.SetInteger("walk", 3);
            animator.speed = 1;
        }
        else
        {
            animator.speed = 0;
        }
    }
    void Ldash()
    {
        
            animator3.SetTrigger("dash");
            Vector3 spawnPos = new Vector3(this.transform.position.x+0.2f, this.transform.position.y, this.transform.position.z);
            GameObject empty = Instantiate(emptyPrefab, spawnPos, Quaternion.identity);
          
            Destroy(empty, 0.3f);
    }
    void Rdash()
    {
        animator4.SetTrigger("dash");
        Vector3 spawnPos = new Vector3(this.transform.position.x -0.2f, this.transform.position.y, this.transform.position.z);
        GameObject empty = Instantiate(emptyPrefab2, spawnPos, Quaternion.identity);

        Destroy(empty, 0.3f);
    }
    void attack()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
           
          
            if (atkspeedRCD + atkspeed <= Time.time)
            {
                atkbox.enabled = true;
                atkspeedRCD = Time.time;
                animator2.SetTrigger("atk");
                Invoke("closed",0.5f);
            }
        }
      
    }
 
    void closed()
    {
        atkbox.enabled = false;
    }
}
