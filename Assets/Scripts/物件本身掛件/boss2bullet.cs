using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2bullet : MonoBehaviour
{
    [Header("BZA Curve")]
    public float flyTime = 10f;
    public float curveHeight = 3f;

    private Vector3 startPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;
    private float startTime;
    private bool isInitialized = false;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (!isInitialized) return;

        float timeSinceStarted = Time.time - startTime;
        float t = timeSinceStarted / flyTime;
        if (t <= 1f)
        {
            transform.position = CalculateBezierPoint(t, startPoint, controlPoint, endPoint);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        startPoint = transform.localPosition;
        endPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        startTime = Time.time;
        CalculateControlPoint();
        isInitialized = true;
    }

    void CalculateControlPoint()
    {
        Vector3 midPoint = (startPoint + endPoint) / 2f;
        float direction = (endPoint.x > startPoint.x) ? 1f : -1f;
        controlPoint = midPoint + new Vector3(direction * curveHeight * 0.5f, curveHeight, 0f);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0;
        point += 2 * u * t * p1;
        point += tt * p2;

        return point;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
            print("destory");
        }
    }
    void OnDrawGizmos()
    {
        if (!isInitialized) return;

        Gizmos.color = Color.red;
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Vector3 gizmoPosition = CalculateBezierPoint(i, startPoint, controlPoint, endPoint);
            Gizmos.DrawSphere(gizmoPosition, 0.1f);
        }
        
    }
    
}