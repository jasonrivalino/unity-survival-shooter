using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccomplishmentLoader : MonoBehaviour
{
    public float transitionTime = 0f;
    public Image Image1;
    public Image Image2;
    public TMP_Text text;
    public AudioClip AccomplishSoundClip;
    AudioSource AccomplishAudio;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAccomplishment());
    }

    void Awake()
    {
        AccomplishAudio = this.gameObject.AddComponent<AudioSource>();
    }

    IEnumerator LoadAccomplishment()
    {
        yield return new WaitForSeconds((float)0.2);
        AccomplishAudio.PlayOneShot(AccomplishSoundClip);
        yield return new WaitForSeconds(transitionTime);
        Image1.CrossFadeAlpha(0, (float)0.50, true);
        Image2.CrossFadeAlpha(0, (float)0.50, true);
        text.CrossFadeAlpha(0, (float)0.50, true);
    }
}
