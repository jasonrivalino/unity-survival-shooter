using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Xml.Serialization;
using UnityEngine.UI;

public class MixLevels : MonoBehaviour {

	public AudioMixer masterMixer;

	[SerializeField] private Slider sfxLvl;
	[SerializeField] private Slider musicLvl;

	void Awake() 
	{
		sfxLvl.value = PlayerPrefs.GetFloat("sfxVol", 1f);
		musicLvl.value = PlayerPrefs.GetFloat("musicVol", 1f);
		SetSfxLvl(sfxLvl.value);
		SetMusicLvl(musicLvl.value);
	}

	public void SetSfxLvl(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", sfxLvl);
		PlayerPrefs.SetFloat("sfxVol", sfxLvl);
		PlayerPrefs.Save();
		Debug.Log("SFX Level: " + sfxLvl);
	}

	public void SetMusicLvl (float musicLvl)
	{
		masterMixer.SetFloat ("musicVol", musicLvl);
		PlayerPrefs.SetFloat ("musicVol", musicLvl);
		PlayerPrefs.Save ();
		Debug.Log ("Music Level: " + musicLvl);
	}

	// caroutine save vol in pref
}
