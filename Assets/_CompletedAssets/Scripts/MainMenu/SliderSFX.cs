using UnityEngine;
using UnityEngine.UI;

public class SliderSFXController : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    public AudioClip sfxClip;

    private bool isPlaying = false;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    private void OnSliderValueChanged()
    {
        if (!isPlaying)
        {
            audioSource.clip = sfxClip;
            audioSource.Play();
            isPlaying = true;
            Invoke("StopSFX", sfxClip.length);
        }
    }

    private void StopSFX()
    {
        audioSource.Stop();
        isPlaying = false;
    }
}
