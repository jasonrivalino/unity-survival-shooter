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
        public GameObject riffle1;
        public GameObject riffle2;
        public GameObject riffle3;
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
                riffle1.SetActive(false);
                riffle2.SetActive(false);
                riffle3.SetActive(false);
                playerMovement.Stop();
                countdownDisplay.text = countdownTime.ToString();
                yield return new WaitForSeconds(1.05f);
                countdownTime--;
            }
            riffle1.SetActive(true);
            riffle2.SetActive(true);
            riffle3.SetActive(true);
            countdownDisplay.text = "Go!";
            playerMovement.StartMove();
            yield return new WaitForSeconds(1f);
            countdownDisplay.gameObject.SetActive(false);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Pet");
            foreach (GameObject obj in enemies)
            {
                HealerMovement healerPetMovement = obj.GetComponent<HealerMovement>();
                if (healerPetMovement != null)
                {
                    healerPetMovement.StartMove();
                }
            }
        }
    }
}

