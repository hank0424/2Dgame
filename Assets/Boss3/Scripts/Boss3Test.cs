using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Test : MonoBehaviour
{
    [Header("Basic")]
    private Animator animator;
    private Rigidbody2D rb;
    public int hp = 76;
    public float fireDetectRange = 4f;
    public float chargeDetectRange = 6f;
    public Transform firePoint;
    public float bulletSpeed;
    public bool isTwoStage = false;
    private bool isAttacking = false;
    private bool isCrashCooldown = false;

    [Header("Prefab")]
    public GameObject bulletPrefab;
    public GameObject[] TwoStageDogPrefab;

    [Header("Player Push Settings")]
    public float pushForce = 10f;
    public float pushUpwardForce = 5f;
    public GameObject RayPos;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CheckCrashWall();
        if (player != null)
        {
            if (!isAttacking)
                FlipTowardsPlayer(player);

            float dist = Vector2.Distance(transform.position, player.transform.position);

            if (!isAttacking && isCrashCooldown == false)
            {
                if (dist <= fireDetectRange)
                {
                    StartCoroutine(FireAttack(player));
                }
                else
                {
                    StartCoroutine(ChargeAttack(player));
                }
            }
        }
    }
    IEnumerator ResetHitAnimation()
    {
        animator.SetBool("hit", true);

        yield return new WaitForSeconds(0.05f);


        animator.SetBool("hit", false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PushPlayerAway(collision.gameObject);
            hp -= Chara2.atk;
        }
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            print("-");
            hp -= Chara2.magic;

            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }
            if (hp <= 0)
            {
                isTwoStage = true;
                foreach (var prefab in TwoStageDogPrefab)
                    Instantiate(prefab, RayPos.transform.position, Quaternion.identity);
                Destroy(this.gameObject);

            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       
        
        if (other.gameObject.CompareTag("atk"))
        {
            print("-");
            hp -= Chara2.atk;

            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }
            if (hp <= 0)
            {
                isTwoStage = true;
                foreach (var prefab in TwoStageDogPrefab)
                    Instantiate(prefab, RayPos.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
               
            }
        }
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

    IEnumerator FireAttack(GameObject player)
    {
        isAttacking = true;
        Quaternion originalRotation = firePoint.rotation;
        int bulletCount = 10;
        for (int i = 0; i < bulletCount; i++)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 e = firePoint.rotation.eulerAngles;
            e.z -= 10f;
            firePoint.rotation = Quaternion.Euler(e);

            yield return new WaitForSeconds(0.05f);
        }
        firePoint.rotation = originalRotation;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
    IEnumerator ChargeAttack(GameObject player)
    {
        isAttacking = true;
        Vector2 targetPos = player.transform.position;
        float chargeDuration = 0.5f;
        float chargeSpeed = 10f;
        float elapsed = 0f;
        while (elapsed < chargeDuration)
        {
            Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
            rb.velocity = dir *1.5f* chargeSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
    IEnumerator BossCrashCooldown()
    {
        isCrashCooldown = true;
        isAttacking = true;
        animator.SetBool("stun", true);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(5f);
        animator.SetBool("stun", false);
        isAttacking = false;
       
        isCrashCooldown = false;
    }


    void CheckCrashWall()
    {
        if (isCrashCooldown) return;

        Vector2 dir = firePoint.right * (transform.rotation.y == 180 ? -1 : 1);

        RaycastHit2D[] hit = Physics2D.RaycastAll(firePoint.position, dir, 0.7f);
        foreach (var hitwhat in hit)
        {
            if (hitwhat.collider != null && hitwhat.collider.CompareTag("Ground"))
            {
                Debug.Log("Boss crashed into wall! Entering -3sec cooldown.");
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

    void OnDrawGizmos()
    {
        if (RayPos == null) return;
        float pushDistance = 3f;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(RayPos.transform.position, RayPos.transform.position + Vector3.right * pushDistance);
        Gizmos.DrawLine(RayPos.transform.position, RayPos.transform.position + Vector3.left * pushDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireDetectRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chargeDetectRange);
    }
}
