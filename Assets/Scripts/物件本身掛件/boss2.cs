using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    [Header("Basic")]
    private Animator animator;
    private Rigidbody2D rb;
    public static float bosshp = 150;
    public static float hp = 150 ;
    public Transform firePoint;
    public float bulletSpeed;
    public bool isTwoStage = false;
    public static bool clear=false;
    public GameObject RayPos;
    public GameObject showDmgPrefab;
    [Header("Prefab")]
    public GameObject bulletPrefab;
    public GameObject slimePrefab;
    public GameObject deleteGround;

    [Header("Player Push Settings")]
    public float pushForce = 1f;
    public float pushUpwardForce = 1f;
  
    private Coroutine attackCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackCoroutine = StartCoroutine(AttackCycle());
    }

    // Update is called once per frame
    void Update()
    {
        if (isTwoStage == false && hp <= (hp/2))
        {
            isTwoStage = true;
            StartCoroutine(Change2Stage());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.CompareTag("bullet"))
        {
            Vector3 down = new Vector3(0, -0.5f, 0);
            print("-");
            hp -= Chara2.magic;

            GameObject show = Instantiate(showDmgPrefab, (this.transform.position + down) + Vector3.up * 1f, Quaternion.identity);
            show.GetComponent<ShowDmg>().SetDamage(Chara2.magic);
            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }
            if (hp <= 0)
            {
                clear = true;
                animator.SetBool("died", true);
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            print("-");
            hp -= Chara2.magic; 
            Vector3 down = new Vector3(0, -0.5f, 0);
            GameObject show = Instantiate(showDmgPrefab, (this.transform.position + down) + Vector3.up * 1f, Quaternion.identity);
            show.GetComponent<ShowDmg>().SetDamage(Chara2.magic);
            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }
            if (hp <= 0)
            {
                clear = true;
                animator.SetBool("died", true);
                Destroy(this.gameObject,0.5f);
            }
        }
        if (other.gameObject.CompareTag("atk"))
        {
            print("-atk");
            hp -= Chara2.atk;
            Vector3 down = new Vector3(0, -0.5f, 0);
            GameObject show = Instantiate(showDmgPrefab, (this.transform.position + down) + Vector3.up * 1f, Quaternion.identity);
            show.GetComponent<ShowDmg>().SetDamage(Chara2.atk);
            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }
            if (hp <= 0)
            {
                clear = true;
                   animator.SetBool("died", true);
                Destroy(this.gameObject,0.5f);
            }
        }
    }
    IEnumerator ResetHitAnimation()
    {
        animator.SetBool("hit", true);

        yield return new WaitForSeconds(0.05f);


        animator.SetBool("hit", false);
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
                if (hit.collider == null)
                    continue;
                if (hit.collider.gameObject == this.gameObject)
                    continue;
                if (hit.collider.transform.IsChildOf(this.transform))
                    continue;
                if (hit.collider.gameObject.CompareTag("Player"))
                    continue;
                if (hit.collider.gameObject.CompareTag("enemy"))
                    continue;

                if (hit.collider.CompareTag("Ground"))
                {
                    canPushLeft = false;
                    break;
                }
            }

            bool canPushRight = true;
            RaycastHit2D[] RightHit = Physics2D.RaycastAll(RayPos.transform.position, Vector2.right, pushDistance);
            foreach (var hit in RightHit)
            {
                if (hit.collider == null)
                    continue;
                if (hit.collider.gameObject == this.gameObject)
                    continue;
                if (hit.collider.transform.IsChildOf(this.transform))
                    continue;
                if (hit.collider.gameObject.CompareTag("Player"))
                    continue;
                if (hit.collider.gameObject.CompareTag("enemy"))
                    continue;

                if (hit.collider.CompareTag("Ground"))
                {
                    canPushRight = false;
                    break;
                }
            }

            Vector2 pushDir;

            if (preferRight && canPushRight)
            {
                pushDir = Vector2.right;
                Debug.Log("right");
            }
            else if (!preferRight && canPushLeft)
            {
                pushDir = Vector2.left;
                Debug.Log("left");
            }
            else if (canPushRight)
            {
                pushDir = Vector2.right;
                Debug.Log("right");
            }
            else if (canPushLeft)
            {
                pushDir = Vector2.left;
                Debug.Log("left");
            }
            else
            {
                pushDir = Vector2.up;
            }
            playerRb.velocity = Vector2.zero;
            Vector2 finalForce = new Vector2(pushDir.x * pushForce, pushUpwardForce);
            playerRb.AddForce(finalForce, ForceMode2D.Impulse);

            StartCoroutine(EnablePlayerTriggerTemporarily(playerCollider));
        }
    }
    void OnDrawGizmos()
    {
        if (RayPos == null) return;

        float pushDistance = 3f;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(RayPos.transform.position, RayPos.transform.position + Vector3.left * pushDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(RayPos.transform.position, RayPos.transform.position + Vector3.right * pushDistance);
    }
    IEnumerator EnablePlayerTriggerTemporarily(Collider2D playerCollider)
    {
        yield return new WaitForSeconds(0.3f);
        playerCollider.isTrigger = false;
    }

    //BOSSATK Logic
    void NormalAtk()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
    void SkllAtk()
    {
        StartCoroutine(SkllBehavior());
    }
    IEnumerator SkllBehavior()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x, originalScale.y * 0.35f, originalScale.z);
        float shrinkTime = 1.5f;
        float t = 0f;
        while (t < shrinkTime)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t / shrinkTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = 0f;
        Vector3 startPos = transform.position;
        Vector3 jumpPos = new Vector3(playerPos.x, playerPos.y + 2f, startPos.z);
        t = 0f;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t / 0.5f);
            transform.position = Vector3.Lerp(startPos, jumpPos, t / 0.5f);
            yield return null;
        }
        yield return new WaitForSeconds(0.15f);
        rb.gravityScale = 3f;
        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), 0);
            Instantiate(slimePrefab, spawnPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(10f);
    }
    IEnumerator AttackCycle()
    {
        while (true)
        {
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(0.5f);
                NormalAtk();
                yield return new WaitForSeconds(0.5f);
                NormalAtk();
                yield return new WaitForSeconds(0.5f);
                NormalAtk();
                yield return new WaitForSeconds(0.5f);
                NormalAtk();
                yield return new WaitForSeconds(0.5f);
                NormalAtk();
                yield return new WaitForSeconds(2f);
            }
            SkllAtk();
            yield return new WaitForSeconds(7.5f);
        }
    }
    IEnumerator Change2Stage()
    {
        Debug.Log("change 2 stage");
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            Debug.Log("stop first atk");
        }

        yield return new WaitForSeconds(3f);
        Destroy(deleteGround);
        StartCoroutine(AttackCycle());
        Debug.Log("star 2 stage atk");
    }
}