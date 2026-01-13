using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Boss3ScopeDog : MonoBehaviour
{
    [Header("Basic")]
    //public HPSharing manager;
    public static int OwnHP = 70;
    public Transform firePos;
    public Transform WaringAreaPos;
    public Transform[] TeleportPos;
    public GameObject OneDog;
    public GameObject TwoDog;
    public bool isAttacking = false;
    public bool isOneDogAlice = true;
    public bool isTwoDogAlive = true;
    public int howManyDogDied = 0;
    private Rigidbody2D rb;
    public Animator animator;
    [Header("Prefabs")]
    public GameObject BulletPrefab;
    public GameObject WaringAreaPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // manager = GameObject.Find("Boss3 Stage2 HP Sharing Gamemager").GetComponent<HPSharing>();
        TP();
       
        TeleportPos = GameObject.FindGameObjectsWithTag("boss3tp")
                        .Select(o => o.transform).ToArray();
    }

    void Update()
    {
        if (boss3area.start2== false)
        {
            Destroy(this.gameObject);
        }
        OneDog = GameObject.Find("Boss3 ShootDog(Clone)");
        TwoDog = GameObject.Find("Boss3 ChargeDog(Clone)");
        if (isOneDogAlice == true && OneDog == null)
        {
            howManyDogDied += 1;
            isOneDogAlice = false;
        }
        if (isTwoDogAlive == true && TwoDog == null)
        {
            howManyDogDied += 1;
            isTwoDogAlive = false;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            FlipTowardsPlayer(player);
            if (!isAttacking)
            {
                StartCoroutine(Attack(player));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            StartCoroutine(ResetHitAnimation());
            OwnHP -= Chara2.magic;
            //manager.TakeDamage(Chara2.magic);
            Destroy(other.gameObject);
            TP();
            if (OwnHP <= 0)
            {
                Die();
            }
        }
        if (other.gameObject.CompareTag("atk"))
        {
            OwnHP -= Chara2.atk;
            //manager.TakeDamage(Chara2.atk);
            StartCoroutine(ResetHitAnimation());
            TP();
            if (OwnHP <= 0)
            {
                Die();
            }
        }
    }
    IEnumerator ResetHitAnimation()
    {
        animator.SetBool("hit", true);

        yield return new WaitForSeconds(0.05f);


        animator.SetBool("hit", false);
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

        Vector3 pos = TeleportPos[index].position;
        pos.y += 0.5f;          // Y ÝS + 0.5

        transform.position = pos;
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
    IEnumerator Attack(GameObject player)
    {
        isAttacking = true;
        if (howManyDogDied == 0)
        {
            Vector3 playerPos = player.transform.position;
            WaringAreaPos.position = playerPos;
            firePos.position = new Vector3(WaringAreaPos.position.x, WaringAreaPos.position.y - 20f, WaringAreaPos.position.z);
            Instantiate(WaringAreaPrefab, WaringAreaPos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Instantiate(BulletPrefab, firePos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
        else if (howManyDogDied == 1)
        {
            Vector3 playerPos = player.transform.position;
            WaringAreaPos.position = playerPos;
            firePos.position = new Vector3(WaringAreaPos.position.x, WaringAreaPos.position.y - 20f, WaringAreaPos.position.z);
            Instantiate(WaringAreaPrefab, WaringAreaPos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Instantiate(BulletPrefab, firePos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(7f);
        }
        else if (howManyDogDied == 2)
        {
            Vector3 playerPos = player.transform.position;
            WaringAreaPos.position = playerPos;
            firePos.position = new Vector3(WaringAreaPos.position.x, WaringAreaPos.position.y - 20f, WaringAreaPos.position.z);
            Instantiate(WaringAreaPrefab, WaringAreaPos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Instantiate(BulletPrefab, firePos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(4f);
        }
        isAttacking = false;
    }
}