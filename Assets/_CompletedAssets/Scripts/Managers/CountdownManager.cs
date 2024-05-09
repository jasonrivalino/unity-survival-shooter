using System.Collections;
using System.Collections.Generic;
using CompleteProject;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
    public class CountdownManager : MonoBehaviour
    {
        public int countdownTime;
        public Text countdownDisplay;
        public GameObject player;
        public Riffle riffle;
        PlayerMovement playerMovement;
        AudioSource CountdownAudio;
        public AudioClip countdownAudioClip;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(CountdownToStart());
        }

        void Awake()
        {
            playerMovement = player.GetComponentInChildren<PlayerMovement>();
            CountdownAudio = this.gameObject.AddComponent<AudioSource>();
        }

        IEnumerator CountdownToStart()
        {
            CountdownAudio.volume = 4f;
            CountdownAudio.PlayOneShot(countdownAudioClip);

            while (countdownTime > 0)
            {
                riffle.enabled = false;
                playerMovement.Stop();
                countdownDisplay.text = countdownTime.ToString();
                yield return new WaitForSeconds(1.05f);
                countdownTime--;
            }
            riffle.enabled = true;
            countdownDisplay.text = "Go!";
            playerMovement.StartMove();
            yield return new WaitForSeconds(1f);
            countdownDisplay.gameObject.SetActive(false);

        }
    }
}

