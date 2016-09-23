using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookSounds : MonoBehaviour {

	public List<AudioClip> spookyNarrator;
	public List<AudioClip> superNarrator;
	public AudioClip musicAudio;

	public AudioSource narratorSource;
	public AudioSource musicSource;

	public int waitTime = 3;
	float time;

	void Start()
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1)
		{
			if (PlayerPrefs.GetInt ("Music") == 1)
			{
				musicSource.clip = musicAudio;
				musicSource.volume = PlayerPrefs.GetFloat ("Music Volume");
				musicSource.Play ();
			}
			else
			{
				musicSource.clip = null;
			}
		}

	}
	void Update()
	{
		if (PlayerPrefs.GetInt ("Auto Turn") == 1)
		{
			if (!narratorSource.isPlaying)
			{
				time += Time.deltaTime;
				if (time >= waitTime)
				{
					GetComponent<ChangePagesWithSound> ().NextPage ();
					time = 0;
				}
			}
		}
	}

	public void PlayAudio(int num) 
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1)
		{
			musicSource.volume = PlayerPrefs.GetFloat ("Narration Volume");
			if (PlayerPrefs.GetString ("Narrator") == "Spooky")
			{
				narratorSource.clip = spookyNarrator [num];

				narratorSource.Play ();
			}
			else if (PlayerPrefs.GetString ("Narrator") == "Super Spooky")
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
