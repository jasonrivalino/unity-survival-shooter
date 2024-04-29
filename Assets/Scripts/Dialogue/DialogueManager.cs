using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    AudioSource dialogueAudio;

    public Animator animator;
    private Queue<Dialogue> dialogueQueue;

    GameObject blurpanel;

    GameObject blankimage;

    public UnityEvent interactAction;

    [Header("Audio")]
    [SerializeField] private AudioClip dialogueTypingSoundClip;

    void Awake()
    {
        dialogueQueue = new Queue<Dialogue>();
        dialogueAudio = this.gameObject.AddComponent<AudioSource>();
        blurpanel = GameObject.Find("BlurPanel");
        blankimage = GameObject.Find("BlankImage");

        blurpanel?.SetActive(false);
        blankimage?.SetActive(true);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(List<Dialogue> dialogues)
    {
        animator.SetBool("IsOpen", true);
        dialogueQueue.Clear();
        foreach (Dialogue dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue nextDialogue = dialogueQueue.Dequeue();
        nameText.text = nextDialogue.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(nextDialogue.sentences));

        if (nextDialogue.name == "Jirna")
        {
            blurpanel?.SetActive(false);
            blankimage?.SetActive(false);
        }
        if (nextDialogue.name == "Tulisan misterius")
        {
            blurpanel?.SetActive(true);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueAudio.PlayOneShot(dialogueTypingSoundClip);
            dialogueText.text += letter;
            yield return new WaitForSeconds((float)0.015);
        }
    }

    void EndDialogue()
    {
        interactAction.Invoke();
        animator.SetBool("IsOpen", false);
    }
}
