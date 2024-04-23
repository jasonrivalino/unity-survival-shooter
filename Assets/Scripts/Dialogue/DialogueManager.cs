using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    AudioSource dialogueAudio;

    public Animator animator;
    private Queue<Dialogue> dialogueQueue;

    [Header("Audio")]
    [SerializeField] private AudioClip dialogueTypingSoundClip;

    void Awake()
    {
        dialogueAudio = this.gameObject.AddComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogueQueue = new Queue<Dialogue>();
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
        animator.SetBool("IsOpen", false);
    }
}
