using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;        // The player's score.

        public int scoreObjective;

        public UnityEvent interactAction;


        Text text;                      // Reference to the Text component.


        void Awake()
        {
            // Set up the reference.
            text = GetComponent<Text>();

            // Reset the score.
            score = 0;
        }


        void Update()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Score: " + score;
            if (score == scoreObjective)
            {
                interactAction.Invoke();
            }
        }
    }
}