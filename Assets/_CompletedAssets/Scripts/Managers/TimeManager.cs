using UnityEngine;
using UnityEngine.UI;

public class SceneStopwatch : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private float elapsedTime;
    private bool isTimerRunning = false;

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    void Start()
    {
        // Start the stopwatch when the scene starts
        StartStopwatch();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            // Calculate the elapsed time since the stopwatch started
            elapsedTime = Time.time - startTime;

            // Update the timer text
            UpdateTimerText(elapsedTime);
        }
    }

    public void StartStopwatch()
    {
        // Start the stopwatch
        startTime = Time.time;
        isTimerRunning = true;
    }

    public void StopStopwatch()
    {
        // Stop the stopwatch
        isTimerRunning = false;
    }

    void UpdateTimerText(float time)
    {
        // Format the time into minutes and seconds
        string hours = ((int)time / 3600).ToString("00");
        string minutes = ((int)time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        // Update the timer text
        timerText.text = hours + ":" + minutes + ":" + seconds;
    }
}
