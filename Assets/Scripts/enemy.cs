using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    private Animator animator;
    private float lastPosition;
    public GameObject obj;

    public float walkSpeed = 1.0f;
    private float moveDirection = 1.0f; // ��l��V���k��

    private float lastActionTime;
    public float minActionInterval = 3.0f;
    public float maxActionInterval = 5.0f;
    public float moveDistance = 1;
    public float jumpForce = 5.0f;

    private bool isMoving = false; // �O�_���b����
    private float distanceMoved = 0.0f; // �w�g���ʪ��Z��
     Rigidbody2D box;
    private BoxCollider2D objbox;

    void Start()
    {
        objbox = GetComponent<BoxCollider2D>();
        box = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastPosition = transform.localPosition.x; // ��l�Ƴ̫�@���� X �b��m
        lastActionTime = Time.time; // ��l�Ƴ̫�@����ʪ��ɶ�
    }

    void Update()
    {

        rotate();


        lastPosition = Mathf.Lerp(lastPosition, transform.localPosition.x, 0.3f);

        // �ˬd�O�_��F��ʮɶ�
        if (Time.time - lastActionTime >= Random.Range(minActionInterval, maxActionInterval))
        {
            // �H����ܦ��
            int randomAction = Random.Range(0, 3);
            if (randomAction < 3)
            {
                // ����
                MoveDirection();

                isMoving = true; // �]�w���b����
            }
            else
            {
                // ���D
                Jump();
                isMoving = false; // ���D�ɰ����
            }

            // ��s�̫�@����ʪ��ɶ�
            lastActionTime = Time.time;

            // ���m�w�g���ʪ��Z��
            distanceMoved = 0.0f;
        }

        // ����
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("atk"))
        {

            hp -= Chara2.atk;

            // �p�G hp �����A����ʵe
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
        // �ˬd�I�쪺����O�_�֦� "bullet" �� tag
        if (collision.gameObject.CompareTag("bullet"))
        {
            print("-");
            // �C���I��ɴ�� hp
            hp-=Chara2.magic;

            // �p�G hp �����A����ʵe
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
        // �]�w hit �ѼƬ� true�A�}�l���� hit �ʵe
        animator.SetBool("hit", true);

        // ����0.05��
        yield return new WaitForSeconds(0.05f);

        // �]�w hit �ѼƬ� false
        animator.SetBool("hit", false);
    }

    void MoveDirection()
    {
        // �H���M�w���ʪ���V�]���k�^
        moveDirection = (Random.Range(0, 2) == 0) ? -1 : 1;
    }

    void Move()
    {
        // �ˬd�O�_���b����
        if (isMoving)
        {
            float moveDelta = moveDirection * walkSpeed * Time.deltaTime;
            distanceMoved += Mathf.Abs(moveDelta);

            // ���o����y�Ц�m
            Vector3 currentPosition = transform.position;

            // ����
            transform.position = new Vector3(currentPosition.x + moveDelta, currentPosition.y, currentPosition.z);

            // �p�G�w�g���ʪ��Z���W�L���w�Z���A�����
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
            // X �b�W�[�A�]�w rotation.y �� 180
            obj.transform.rotation = Quaternion.Euler(0, 180, 0);
   
        }
        else if (currentPosition < lastPosition && Mathf.Abs(obj.transform.rotation.eulerAngles.y - 180) < 0.1f)
        {
            // X �b��֡A�]�w rotation.y �� 0
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
      
    }

    void Jump()
    {
        // ���D
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
