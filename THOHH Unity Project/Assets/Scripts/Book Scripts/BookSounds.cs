using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookSounds : MonoBehaviour {

	public List<AudioClip> spookyNarrator;
	public List<AudioClip> superNarrator;
	public AudioClip musicAudio;

	public AudioSource narratorSource;
	public AudioSource musicSource;

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

	public void PlayAudio(int num) 
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1)
		{
		
			if (PlayerPrefs.GetString ("Narrator") == "Spooky")
			{
				narratorSource.clip = spookyNarrator [num];

				narratorSource.Play ();
			}
			else
			if (PlayerPrefs.GetString ("Narrator") == "Super Spooky")
			{
				narratorSource.clip = superNarrator [num];
				narratorSource.Play ();
			}
		}
		else
		{
			narratorSource.clip = null;
		}
	}
}
