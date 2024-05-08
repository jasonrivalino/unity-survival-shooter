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
        PlayerMovement playerMovement;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(CountdownToStart());
        }

        void Awake()
        {
            playerMovement = player.GetComponentInChildren<PlayerMovement>();
        }

        IEnumerator CountdownToStart()
        {

            while (countdownTime > 0)
            {
                playerMovement.Stop();
                countdownDisplay.text = countdownTime.ToString();
                yield return new WaitForSeconds(1f);
                countdownTime--;
            }
            countdownDisplay.text = "Go!";
            playerMovement.StartMove();
            yield return new WaitForSeconds(1f);
            countdownDisplay.gameObject.SetActive(false);

        }
    }
}

