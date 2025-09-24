using UnityEngine;

public class Chara : MonoBehaviour
{
    public float moveSpeed = 5f; // 設定移動速度
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 獲取玩家的輸入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 計算移動方向
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        movement.Normalize(); // 正規化以避免對角線移動速度變快

        // 移動角色
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // 更新Animator中的"walk"參數
        UpdateAnimatorParameters(horizontalInput, verticalInput);
    }

    void UpdateAnimatorParameters(float horizontalInput, float verticalInput)
    {
        // 設定"walk"參數的值
        if (horizontalInput > 0f)
        {
            // 右移動
            animator.SetInteger("walk", 0);
            animator.speed = 1f;
        }
        else if (horizontalInput < 0f)
        {
            // 左移動
            animator.SetInteger("walk", 3);
            animator.speed = 1f;
        }
        else if (verticalInput > 0f)
        {
            // 上移動
            animator.SetInteger("walk", 1);
            animator.speed = 1f;
        }
        else if (verticalInput < 0f)
        {
            // 下移動
            animator.SetInteger("walk", 2);
            animator.speed = 1f;
        }
        else
        {
          

            // 播放速度設置為0以停在當前動畫的最後一幀
            animator.speed = 0f;
        }
    }
}