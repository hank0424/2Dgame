using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class atk_detect : MonoBehaviour
{
    public GameObject slime;
    public GameObject bullet;
    public float fireInterval = 2f; // �ӏ��l���g���r�g
    private float timer = 0f;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange)
        {
            timer += Time.deltaTime;

            if (timer >= fireInterval)
            {
                Instantiate(bullet, slime.transform.position, Quaternion.identity);
                timer = 0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            timer = 0f; // ����Ӌ�r���������x�_���R�������
        }
    }
}