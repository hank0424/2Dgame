using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDmg : MonoBehaviour
{
    public Text damageTxt;
    public float moveUpSpeed = 1f;
    public float fadeOutTime;
    public float delayBeforeFade;
    private float timer = 0f;
    private Color textColor;

    void Start()
    {
        textColor = damageTxt.color;
    }

    void Update()
    {
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        // ¶}©l²H¥X
        if (timer > delayBeforeFade)
        {
            float t = (timer - delayBeforeFade) / fadeOutTime;
            textColor.a = Mathf.Lerp(1f, 0f, t);
            damageTxt.color = textColor;

            if (t >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(int damage)
    {
        damageTxt.text = "-" + damage.ToString();
    }
}
