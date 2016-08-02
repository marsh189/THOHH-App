using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookSounds : MonoBehaviour {

	public List<AudioClip> spookyNarrator;
	public List<AudioClip> superNarrator;
	public AudioClip musicAudio;

	AudioSource narratorSource;
	AudioSource musicSource;

	void Start()
	{
		narratorSource = GetComponent<AudioSource> ();
		musicSource = GetComponent<AudioSource> ();
	}

	void Update () 
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1)
		{
			if (PlayerPrefs.GetInt ("Music") == 1)
			{
				musicSource.clip = musicAudio;
				musicSource.Play ();
			}

			if (PlayerPrefs.GetString ("Narrator") == "Spooky")
			{
				narratorSource.clip = spookyNarrator [PlayerPrefs.GetInt ("Page Number")];
				narratorSource.Play ();
			}
			else
			if (PlayerPrefs.GetString ("Narrator") == "Super Spooky")
			{
				narratorSource.clip = superNarrator [PlayerPrefs.GetInt ("Page Number")];
				narratorSource.Play ();
			}
		}
		else
		{
			narratorSource.clip = null;
			musicSource.clip = null;
		}
	}
}
