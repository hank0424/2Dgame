using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chara2 : MonoBehaviour
{
    private InventoryManager InventoryManager;
    private TestAddItem TestAddItem;
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
    public Animator animator6;

    //
    public static int hp;
    public static bool shooting = false;
    //
    public GameObject bulletPrefab;
    public GameObject bomb;
    public GameObject emptyPrefab;
    public GameObject emptyPrefab2;
    //
    public Transform firePoint;
    private float lastXPosition;
    private int dashtime = 1;
    public static int atk = 0;
    public static int magic = 1;
    public static float atkspeed = 0.4f;
    public static float atkspeedRCD = 0;
    public BoxCollider2D Player;
    public BoxCollider2D atkbox;

    public static bool fireupdate=false;
    public static bool darkupdate = false;

    public static bool death = false;
    int faceDir = 1;
    void Start()
    {
        animator = GetComponent<Animator>();
       
        atkbox.enabled = !atkbox.enabled;
        TestAddItem = FindObjectOfType<TestAddItem>();
        InventoryManager = FindObjectOfType<InventoryManager>();
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("anibullet")&&skill_list.isshield!=true)
        {

            health.HP--;


        }
        if (collision.gameObject.CompareTag("anibullet") && skill_list.isshield== true)
        {

            skill_list.shield_hp--;


        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    
        // 浪代琌窱
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            dashtime = 1;
            remainingJumps = 2; // 
            moveSpeed = 3f;
        }
        if (collision.collider.CompareTag("enemy") && skill_list.isshield != true)
        {
            health.HP--;
           
        }

        if (collision.collider.CompareTag("enemy") && skill_list.isshield == true)
        {
            skill_list.shield_hp--;
     
        }
        if (collision.gameObject.CompareTag("anibullet") && skill_list.isshield != true)
        {

            health.HP--;


        }
        {
            if (collision.gameObject.CompareTag("anibullet") && skill_list.isshield == true)
            {

                skill_list.shield_hp--;


            }
        }
    }
    void heal()
    {
        if (Input.GetKeyDown(KeyCode.G) && health.HP != health.maxHp)
        {
            InventoryManager.ConsumeItem(TestAddItem.healing, 1);
            health.HP = health.maxHp;
            animator6.SetTrigger("heal");
        }
    }


    void Update()
    {
        
        bombset();
        animatechange();
        debugJump();
        sheild();
        attack();
        heal();
        if (health.HP <= 0 && SpawnPT.spawn_active == 0)
        {
            this.gameObject.transform.position = new Vector3(-21.19f, -4.68f, 0);
            health.HP = health.maxHp;
            Player.isTrigger = false;
            death = false;
        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 1)
        {
            this.gameObject.transform.position = new Vector3(12.5100002f, -25.9899998f, 0);
            health.HP = health.maxHp;
            Player.isTrigger = false;
            death = false;
        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 2)
        {
            this.gameObject.transform.position = new Vector3(39.92f, -26.62f, 0);
            health.HP = health.maxHp;
            Player.isTrigger = false;
            death = false;
        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 3)
        {
            this.gameObject.transform.position = new Vector3(74.655f, 27.988f, 0);
            health.HP = health.maxHp;
            Player.isTrigger = false;
            death = false;
        }
        if (health.HP <= 0 && SpawnPT.spawn_active == 4)
        {
            this.gameObject.transform.position = new Vector3(104.24f, 40.5f, 0);
            health.HP = health.maxHp;
            Player.isTrigger = false;
            death = false;
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
        if (Input.GetKeyDown(KeyCode.X)&& skill_list.magic == true)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)&&Input.GetKey(KeyCode.LeftArrow)&& isGrounded == false&&dashtime==1 && skill_list.dash == true)
        {
            Ldash();
            dashtime -= 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightArrow) && isGrounded == false&& dashtime == 1 && skill_list.dash == true)
        {
            Rdash();
            dashtime -= 1;
        }

        if (horizontalInput > 0)
            faceDir = 1;
        else if (horizontalInput < 0)
            faceDir = -1;

        UpdateAnimatorParameters(horizontalInput);
    }
    void sheild()
    {
        if(Input.GetKeyDown(KeyCode.F)&&health.mana>0&&skill_list.isshield==false&&skill_list.shield==true)
        {
            health.mana-=2;
            animator5.SetBool("shield",true);
            skill_list.skill_shield();
        }
        if(skill_list.isshield!=true)
        {
            animator5.SetBool("shield", false);
        }
    }
    void bombset()
    {
        if (Input.GetKeyDown(KeyCode.V)&& InventoryManager.GetItemCount(TestAddItem.PickUpWhatItems[7])>=1)
        {

            InventoryManager.ConsumeItem(TestAddItem.bomb, 1);
            // 用真正的面向方向
            Vector3 spawnPos = transform.position + new Vector3(0.3f * faceDir, 0.1f, 0f);

            GameObject bombObj = Instantiate(bomb, spawnPos, Quaternion.identity);

            Rigidbody2D rb = bombObj.GetComponent<Rigidbody2D>();
            Collider2D bombCol = bombObj.GetComponent<Collider2D>();

            Physics2D.IgnoreCollision(bombCol, Player);

            if (rb != null)
            {
                rb.velocity = new Vector2(2f * faceDir, 3f);
            }

            
        }
    }
    void Shoot()
    {
        if (health.mana > 0)
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
   void animatechange()
    {
        if(fireupdate==true)
        {
            animator2.SetBool("fire", true);
            animator5.SetBool("fire", true);

        }
        if (darkupdate == true)
        {
            animator2.SetBool("fire", false);
            animator2.SetBool("dark", true);
            animator5.SetBool("fire", false);
            animator5.SetBool("dark", true);
        }
    }
    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
        remainingJumps -= 2;

        // 狦惠璶硂柑睰ㄤ矪瞶铬臘呸胯
        if (doubleJ == true)
        {
            remainingJumps++;
        }
        // 猔種硂柑ぃ砞﹚笆礶牟祇竟
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
        if (Input.GetKeyDown(KeyCode.C)&& skill_list.attack==true)
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
 void debugJump()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Vector3 debug = new Vector3(0,0.1f, 0);
            Player.transform.position +=debug;
        }
    }
    void closed()
    {
        atkbox.enabled = false;
    }
}
