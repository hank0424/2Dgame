using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class DialogueManager : MonoBehaviour
{
    public Text textLegacy;
    public float typingSpeed = 0.05f;
    public List<string> dialogueLines;
    public GameObject ui;

    private GameObject[] uiObjects;
    private int currentLineIndex = 0;
    private bool isTyping = false;




    void Start()
    {
        HideDialogue();
        uiObjects = GameObject.FindGameObjectsWithTag("ui");
    }

    private void OnTriggerStay2D(Collider2D other)
    {


        if (Input.GetKey(KeyCode.E))
        {
            StopAllCoroutines();
            ShowDialogue();
            
            textLegacy.text = dialogueLines[0];

        }
    }

    void Update()
    {
        
        // Other update logic...

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {

            if (isTyping)
            {
                StopAllCoroutines();
                isTyping = false;
                textLegacy.text = dialogueLines[currentLineIndex];
            }
            else
            {
                currentLineIndex++;
                if (currentLineIndex < dialogueLines.Count)
                {
                    StartCoroutine(TypeDialogue());
                }
                else
                {
                    HideDialogue();

                }
            }
        }

        if (!isTyping && currentLineIndex >= dialogueLines.Count &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            HideDialogue();
        }
    }

    IEnumerator TypeDialogue()
    {
       
        isTyping = true;
        textLegacy.text = "";

        foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
        {
            textLegacy.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void ShowDialogue()
    {
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("ui");
        textLegacy.gameObject.SetActive(true);
        ui.gameObject.SetActive(true);
        Chara2.moveSpeed = 0f;
        Chara2.jumpForce = 0f;


    }
  
        
    
  
    void HideDialogue()
    {
        textLegacy.gameObject.SetActive(false);
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("ui");
        ui.gameObject.SetActive(false);
        Chara2.moveSpeed = 4f;
        Chara2.jumpForce = 4f;
        currentLineIndex = 0;
    }
}