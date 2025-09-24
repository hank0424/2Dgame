using UnityEngine;

public class Chara : MonoBehaviour
{
    public float moveSpeed = 5f; // �]�w���ʳt��
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ������a����J
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �p�Ⲿ�ʤ�V
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        movement.Normalize(); // ���W�ƥH�קK�﨤�u���ʳt���ܧ�

        // ���ʨ���
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // ��sAnimator����"walk"�Ѽ�
        UpdateAnimatorParameters(horizontalInput, verticalInput);
    }

    void UpdateAnimatorParameters(float horizontalInput, float verticalInput)
    {
        // �]�w"walk"�Ѽƪ���
        if (horizontalInput > 0f)
        {
            // �k����
            animator.SetInteger("walk", 0);
            animator.speed = 1f;
        }
        else if (horizontalInput < 0f)
        {
            // ������
            animator.SetInteger("walk", 3);
            animator.speed = 1f;
        }
        else if (verticalInput > 0f)
        {
            // �W����
            animator.SetInteger("walk", 1);
            animator.speed = 1f;
        }
        else if (verticalInput < 0f)
        {
            // �U����
            animator.SetInteger("walk", 2);
            animator.speed = 1f;
        }
        else
        {
          

            // ����t�׳]�m��0�H���b��e�ʵe���̫�@�V
            animator.speed = 0f;
        }
    }
}