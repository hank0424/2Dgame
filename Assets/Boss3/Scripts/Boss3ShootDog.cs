using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss3ShootDog : MonoBehaviour
{
    [Header("Basic")]
    //public HPSharing manager;
    public static int OwnHP = 70;
    public int hitNumber = 0;
    public Transform firePos;
    public Transform[] TeleportPos;
    public GameObject TwoDog;
    public GameObject ThreeDog;
    public bool isAttacking = false;
    public bool isTwoDogAlice = true;
    public bool isThreeDogAlive = true;
    public int howManyDogDied = 0;
    private Rigidbody2D rb;
    public Animator animator;
    [Header("Prefabs")]
    public GameObject BulletPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

      
        //manager = GameObject.Find("Boss3 Stage2 HP Sharing Gamemager").GetComponent<HPSharing>();

      
        TeleportPos = GameObject.FindGameObjectsWithTag("boss3tp")
                                .Select(o => o.transform)
                                .ToArray();

        TP();
    }
    IEnumerator ResetHitAnimation()
    {
        animator.SetBool("hit", true);

        yield return new WaitForSeconds(0.05f);


        animator.SetBool("hit", false);
    }
    void Update()
    {
        if (boss3area.start2== false)
        {
            Destroy(this.gameObject);
        }
        TwoDog = GameObject.Find("Boss3 ChargeDog(Clone)");
        ThreeDog = GameObject.Find("Boss3 ScopeDog(Clone)");

        if (isTwoDogAlice == true && TwoDog == null)
        {
            howManyDogDied += 1;
            isTwoDogAlice = false;
        }
        if (isThreeDogAlive == true && ThreeDog == null)
        {
            howManyDogDied += 1;
            isThreeDogAlive = false;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            FlipTowardsPlayer(player);

            if (!isAttacking)
                StartCoroutine(FireAttack(player));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            hitNumber += 1;
            OwnHP -= Chara2.magic;
            StartCoroutine(ResetHitAnimation());
            // manager.TakeDamage(Chara2.magic);
            Destroy(other.gameObject);
            animator.SetTrigger("hit");
            if (hitNumber > 2)
                TP();

            if (OwnHP <= 0)
                Die();
        }

        if (other.gameObject.CompareTag("atk"))
        {
            hitNumber += 1;
            animator.SetTrigger("hit");
            StartCoroutine(ResetHitAnimation());
            OwnHP -= Chara2.atk;
          //  manager.TakeDamage(Chara2.atk);

            if (hitNumber > 2)
                TP();

            if (OwnHP <= 0)
                Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    void TP()
    {
        if (TeleportPos == null || TeleportPos.Length == 0)
        {
            Debug.LogWarning("No TeleportPos found ¡ª Make sure TP points have tag Boss3TP.");
            return;
        }

        int index = Random.Range(0, TeleportPos.Length);
        transform.position = TeleportPos[index].position + new Vector3(0, 0.3f,0);

        hitNumber = 0;
    }

    void FlipTowardsPlayer(GameObject player)
    {
        if (player == null) return;

        float playerX = player.transform.position.x;
        float bossX = transform.position.x;

        if (playerX > bossX)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    IEnumerator FireAttack(GameObject player)
    {
        isAttacking = true;

        Vector2 direction = (player.transform.position - firePos.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (howManyDogDied == 0)
        {
            float[] angleOffsets = { 0f, 10f, 20f, -10f, -20f };
            foreach (float offset in angleOffsets)
                Instantiate(BulletPrefab, firePos.position, Quaternion.Euler(0f, 0f, angle + offset));

            yield return new WaitForSeconds(5f);
        }
        else if (howManyDogDied == 1)
        {
            float[] angleOffsets = { 0f, 5f, 10f, 20f, -5f, -10f, -20f };
            foreach (float offset in angleOffsets)
                Instantiate(BulletPrefab, firePos.position, Quaternion.Euler(0f, 0f, angle + offset));

            yield return new WaitForSeconds(3.5f);
        }
        else if (howManyDogDied == 2)
        {
            float[] angleOffsets = { 0f, 5f, 10f, 15f, 20f, -5f, -10f, -15f, -20f };
            foreach (float offset in angleOffsets)
                Instantiate(BulletPrefab, firePos.position, Quaternion.Euler(0f, 0f, angle + offset));

            yield return new WaitForSeconds(2f);
        }

        isAttacking = false;
    }
}