using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour
{
    public GameObject monster;
    private bool ismove=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ismove==false)
        {
            StartCoroutine(MoveDown());
        }
        
    }

    IEnumerator MoveDown()
    {
        ismove=true;
        Vector2 startPos = monster.transform.position;
        Vector2 endPos = new Vector2(startPos.x, startPos.y - 1.5f);
        float elapsed = 0f;
        float duration = 0.5f; // ≥÷¿m 0.5 √Î

        while (elapsed < duration)
        {
            monster.transform.position = Vector2.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        monster.transform.position = endPos;
    }
}
