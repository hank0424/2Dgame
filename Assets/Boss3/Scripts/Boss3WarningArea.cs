using UnityEngine;
using System.Collections;

public class Boss3WarningArea : MonoBehaviour
{
    [Header("WarningArea")]
    public float warningDuration = 2f;
    public Color startColor = new Color(1, 1, 1, 0.3f);
    public Color endColor = new Color(1, 0, 0, 0.8f);

    private SpriteRenderer spriteRenderer;
    private float timer = 0f;
    private bool isWarning = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartWarning();
    }

    void Update()
    {
        if (isWarning)
        {
            UpdateWarningEffect();
        }
    }

    public void StartWarning()
    {
        isWarning = true;
        timer = 0f;
        spriteRenderer.color = startColor;
        StartCoroutine(WarningCountdown());
    }

    private void UpdateWarningEffect()
    {
        timer += Time.deltaTime;
        float progress = timer / warningDuration;

        spriteRenderer.color = Color.Lerp(startColor, endColor, progress);

        float flashSpeed = Mathf.Lerp(1f, 8f, progress);
        float alpha = Mathf.PingPong(Time.time * flashSpeed, 0.5f) + 0.5f;
        Color currentColor = spriteRenderer.color;
        currentColor.a = alpha;
        spriteRenderer.color = currentColor;
    }

    private IEnumerator WarningCountdown()
    {
        yield return new WaitForSeconds(warningDuration);
        EndWarning();
    }

    private void EndWarning()
    {
        isWarning = false;
        OnWarningComplete();

        Destroy(gameObject);
    }

    private void OnWarningComplete()
    {
        Debug.Log("Start Attack¡I");
    }
}