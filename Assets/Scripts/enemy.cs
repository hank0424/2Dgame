using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    private Animator animator;
    private float lastPosition;
    public GameObject obj;

    public float walkSpeed = 1.0f;
    private float moveDirection = 1.0f; // 初始方向為右邊

    private float lastActionTime;
    public float minActionInterval = 3.0f;
    public float maxActionInterval = 5.0f;
    public float moveDistance = 1;
    public float jumpForce = 5.0f;

    private bool isMoving = false; // 是否正在移動
    private float distanceMoved = 0.0f; // 已經移動的距離
     Rigidbody2D box;
    private BoxCollider2D objbox;

    void Start()
    {
        objbox = GetComponent<BoxCollider2D>();
        box = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastPosition = transform.localPosition.x; // 初始化最後一次的 X 軸位置
        lastActionTime = Time.time; // 初始化最後一次行動的時間
    }

    void Update()
    {

        rotate();


        lastPosition = Mathf.Lerp(lastPosition, transform.localPosition.x, 0.3f);

        // 檢查是否到達行動時間
        if (Time.time - lastActionTime >= Random.Range(minActionInterval, maxActionInterval))
        {
            // 隨機選擇行動
            int randomAction = Random.Range(0, 3);
            if (randomAction < 3)
            {
                // 移動
                MoveDirection();

                isMoving = true; // 設定正在移動
            }
            else
            {
                // 跳躍
                Jump();
                isMoving = false; // 跳躍時停止移動
            }

            // 更新最後一次行動的時間
            lastActionTime = Time.time;

            // 重置已經移動的距離
            distanceMoved = 0.0f;
        }

        // 移動
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("atk"))
        {

            hp -= Chara2.atk;

            // 如果 hp 為正，播放動畫
            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }

            if (hp <= 0)
            {
                objbox.enabled = false;
                Destroy(box);
                Destroy(this.gameObject, 0.5f);
                animator.SetBool("died", true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查碰到的物體是否擁有 "bullet" 的 tag
        if (collision.gameObject.CompareTag("bullet"))
        {
            print("-");
            // 每次碰到時減少 hp
            hp-=Chara2.magic;

            // 如果 hp 為正，播放動畫
            if (hp > 0)
            {
                StartCoroutine(ResetHitAnimation());
            }

            if (hp <= 0)
            {
                objbox.enabled = false;
                Destroy(box);
                Destroy(this.gameObject,0.5f);
                animator.SetBool("died", true);
            }
        }
     
    }
    
    IEnumerator ResetHitAnimation()
    {
        // 設定 hit 參數為 true，開始播放 hit 動畫
        animator.SetBool("hit", true);

        // 等待0.05秒
        yield return new WaitForSeconds(0.05f);

        // 設定 hit 參數為 false
        animator.SetBool("hit", false);
    }

    void MoveDirection()
    {
        // 隨機決定移動的方向（左右）
        moveDirection = (Random.Range(0, 2) == 0) ? -1 : 1;
    }

    void Move()
    {
        // 檢查是否正在移動
        if (isMoving)
        {
            float moveDelta = moveDirection * walkSpeed * Time.deltaTime;
            distanceMoved += Mathf.Abs(moveDelta);

            // 取得全域座標位置
            Vector3 currentPosition = transform.position;

            // 移動
            transform.position = new Vector3(currentPosition.x + moveDelta, currentPosition.y, currentPosition.z);

            // 如果已經移動的距離超過指定距離，停止移動
            if (distanceMoved >= moveDistance)
            {
                isMoving = false;
            }
        }
    }


    void rotate()
    {
        float currentPosition = transform.localPosition.x;

        if (currentPosition >= lastPosition && obj.transform.rotation.y==0)
        {
            // X 軸增加，設定 rotation.y 為 180
            obj.transform.rotation = Quaternion.Euler(0, 180, 0);
   
        }
        else if (currentPosition < lastPosition && Mathf.Abs(obj.transform.rotation.eulerAngles.y - 180) < 0.1f)
        {
            // X 軸減少，設定 rotation.y 為 0
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
      
    }

    void Jump()
    {
        // 跳躍
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
