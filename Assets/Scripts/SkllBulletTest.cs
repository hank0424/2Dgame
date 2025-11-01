using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkllBulletTest : MonoBehaviour
{
    public float speed = 6.0f;
    private Vector3 targetPos;
    private bool isLaunched = false;

    // Update is called once per frame
    void Update()
    {
        if (isLaunched)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    public void FindTargetAndShoot(Vector3 target)
    {
        targetPos = target;
        StartCoroutine(DelayAndLaunch());
    }

    IEnumerator DelayAndLaunch()
    {
        yield return new WaitForSeconds(0.5f);
        isLaunched = true;
    }
}

