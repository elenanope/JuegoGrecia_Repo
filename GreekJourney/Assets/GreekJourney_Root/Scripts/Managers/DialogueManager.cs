using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{

    [Header("Text References")]
    [SerializeField] GameObject avisoDialogo;
    [SerializeField] GameObject otherPanel;
    [SerializeField] TMPro.TMP_Text otherText;

    float typingTime;
    bool playerInRange;
    bool didDialogueStart;
    bool dialogueOver;
    int lineIndex;

    [Header("Dialogo Arrays")]
    [SerializeField, TextArea(2, 4)] string[] dialogueLines;


    // Update is called once per frame
    void Update()
    {
        //if (buttonPressed&&playerInRange) 
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerInRange = true; avisoDialogo.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerInRange = false; avisoDialogo.SetActive(false);
    }

    void StartDialogue()
    {
        didDialogueStart= true;
        otherPanel.SetActive(true);
        avisoDialogo.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex<dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialogueOver = true;
            otherPanel.SetActive(false);
            avisoDialogo.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        otherText.text = string.Empty;

        foreach (char ch  in dialogueLines[lineIndex])
        {
            otherText.text+= ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && playerInRange&&!dialogueOver)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (otherText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                otherText.text = dialogueLines[lineIndex];
            }
        }
        if (context.performed && playerInRange && dialogueOver)
        {
            if (!didDialogueStart)
            {
                didDialogueStart = true;
                otherPanel.SetActive(true);
                avisoDialogo.SetActive(false);
                lineIndex = dialogueLines.Length - 1;
                Time.timeScale = 0f;
                StartCoroutine(ShowLine());
            }
            else if (otherText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                otherText.text = dialogueLines[lineIndex];
            }


        }
        //if(dialogueCanStart)IR viendo por que paso va
    }
        
}
