using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3ChargeDog : MonoBehaviour
{
    [Header("Basic")]
   // public HPSharing manager;
    public static int OwnHP = 70;
    public Transform RayPosition;
    public GameObject OneDog;
    public GameObject ThreeDog;
    public bool isAttacking = false;
    public bool isCrashCooldown = false;
    public bool isOneDogAlice = true;
    public bool isThreeDogAlive = true;
    public int howManyDogDied = 0;
    public Animator animator;
    private Rigidbody2D rb;

    [Header("Player Push Settings")]
    public float pushForce = 10f;
    public float pushUpwardForce = 5f;
    public GameObject RayPos;
    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
       // manager = GameObject.Find("Boss3 Stage2 HP Sharing Gamemager").GetComponent<HPSharing>();


    }
    IEnumerator ResetHitAnimation()
    {
        animator.SetBool("hit", true);

        yield return new WaitForSeconds(0.05f);


        animator.SetBool("hit", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (boss3area.start2 == false)
        {
            Destroy(this.gameObject);
        }
        OneDog = GameObject.Find("Boss3 ShootDog(Clone)");
        ThreeDog = GameObject.Find("Boss3 ScopeDog(Clone)");
        if (isOneDogAlice == true && OneDog == null)
        {
            howManyDogDied += 1;
            isOneDogAlice = false;
        }
        if (isThreeDogAlive == true && ThreeDog == null)
        {
            howManyDogDied += 1;
            isThreeDogAlive = false;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CheckCrashWall();
        if (player != null)
        {
            if (!isAttacking)
                FlipTowardsPlayer(player);

            float dist = Vector2.Distance(transform.position, player.transform.position);

            if (!isAttacking && isCrashCooldown == false)
            {
                StartCoroutine(ChargeAttack(player));
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player take damge");
            rb.velocity = Vector2.zero;
            PushPlayerAway(collision.gameObject);
          

        }
        if (collision.gameObject.CompareTag("bullet"))
        {
            StartCoroutine(ResetHitAnimation());
            OwnHP -= Chara2.magic;
            // manager.TakeDamage(Chara2.magic);
            Destroy(collision.gameObject);
            if (OwnHP <= 0)
            {
                Die();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("atk"))
        {
            StartCoroutine(ResetHitAnimation());
            OwnHP -= Chara2.atk;
         //   manager.TakeDamage(Chara2.atk);
          
            if (OwnHP <= 0)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }

    void FlipTowardsPlayer(GameObject player)
    {
        if (player == null) return;

        float playerX = player.transform.position.x;
        float bossX = transform.position.x;

        if (playerX > bossX)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    IEnumerator ChargeAttack(GameObject player)
    {
        isAttacking = true;
        Vector2 targetPos = player.transform.position;
        float chargeDuration = 0.5f;
        float chargeSpeed = 10f;
        float elapsed = 0f;
        if (howManyDogDied == 0)
        {
            while (elapsed < chargeDuration)
            {
                Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
                rb.velocity = dir * (chargeSpeed * 0.65f);
                elapsed += Time.deltaTime;
                yield return null;
            }
            rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(2f);
        }
        else if (howManyDogDied == 1)
        {
            while (elapsed < chargeDuration)
            {
                Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
                rb.velocity = dir * (chargeSpeed * 0.95f);
                elapsed += Time.deltaTime;
                yield return null;
            }
            rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(1.5f);
        }
        else if (howManyDogDied == 2)
        {
            while (elapsed < chargeDuration)
            {
                Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
                rb.velocity = dir * (chargeSpeed * 1.2f);
                elapsed += Time.deltaTime;
                yield return null;
            }
            rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(1.2f);
        }
        isAttacking = false;
    }
    IEnumerator BossCrashCooldown()
    {
        isCrashCooldown = true;
        isAttacking = true;

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(5f);

        isAttacking = false;
        yield return new WaitForSeconds(0.1f);
        isCrashCooldown = false;
    }


    void CheckCrashWall()
    {
        if (isCrashCooldown) return;

        Vector2 dir = RayPosition.right * (transform.rotation.y == 180 ? -1 : 1);

        RaycastHit2D[] hit = Physics2D.RaycastAll(RayPosition.position, dir, 0.35f);
        foreach (var hitwhat in hit)
        {
            if (hitwhat.collider != null && hitwhat.collider.CompareTag("Ground"))
            {
                Debug.Log("Boss crashed into wall! Entering 10-sec cooldown.");
                StartCoroutine(BossCrashCooldown());
            }
        }
    }
    void PushPlayerAway(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Collider2D playerCollider = player.GetComponent<Collider2D>();

        if (playerRb != null && playerCollider != null)
        {
            playerCollider.isTrigger = true;
            float playerX = player.transform.position.x;
            float bossX = transform.position.x;

            float pushDistance = 3f;
            bool preferRight = playerX >= bossX;

            bool canPushLeft = true;
            RaycastHit2D[] leftHits = Physics2D.RaycastAll(RayPos.transform.position, Vector2.left, pushDistance);
            foreach (var hit in leftHits)
            {
                if (hit.collider == null) continue;
                if (hit.collider.gameObject.CompareTag("Ground")) { canPushLeft = false; break; }
            }

            bool canPushRight = true;
            RaycastHit2D[] rightHits = Physics2D.RaycastAll(RayPos.transform.position, Vector2.right, pushDistance);
            foreach (var hit in rightHits)
            {
                if (hit.collider == null) continue;
                if (hit.collider.gameObject.CompareTag("Ground")) { canPushRight = false; break; }
            }

            Vector2 pushDir;

            if (preferRight && canPushRight) pushDir = Vector2.right;
            else if (!preferRight && canPushLeft) pushDir = Vector2.left;
            else if (canPushRight) pushDir = Vector2.right;
            else if (canPushLeft) pushDir = Vector2.left;
            else pushDir = Vector2.up;

            playerRb.velocity = Vector2.zero;
            Vector2 finalForce = new Vector2(pushDir.x * pushForce, pushUpwardForce);
            playerRb.AddForce(finalForce, ForceMode2D.Impulse);

            StartCoroutine(EnablePlayerTriggerTemporarily(playerCollider));
        }
    }
    IEnumerator EnablePlayerTriggerTemporarily(Collider2D playerCollider)
    {
        yield return new WaitForSeconds(0.3f);
        playerCollider.isTrigger = false;
    }
}