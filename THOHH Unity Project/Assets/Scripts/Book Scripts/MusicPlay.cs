using UnityEngine;
using System.Collections;

public class MusicPlay : MonoBehaviour {

	public AudioSource musicSource;
	public AudioClip musicAudio;

	void Start()
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1)
		{
			if (PlayerPrefs.GetInt ("Music") == 1)
			{
				musicSource.clip = musicAudio;
				musicSource.Play ();
			}
			else
			{
				musicSource.clip = null;
			}
		}

	}
}
