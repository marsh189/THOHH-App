using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookSounds : MonoBehaviour {

	public List<AudioClip> spookyNarrator;
	public List<AudioClip> superNarrator;
	public AudioClip musicAudio;

	public GameObject popUp;

	public AudioSource narratorSource;
	public AudioSource musicSource;

	public int waitTime = 3;
	float time = 0;

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
			if (!popUp.activeInHierarchy)
			{
				if (!narratorSource.isPlaying || narratorSource.clip == null)
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
	}

	public void PlayAudio(int num) 
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1)
		{
			narratorSource.volume = PlayerPrefs.GetFloat ("Narration Volume");

			if (!popUp.activeInHierarchy)
			{
				if (PlayerPrefs.GetString ("Narrator") == "Spooky")
				{
					if (num < spookyNarrator.Count)
					{
						narratorSource.clip = spookyNarrator [num];
					}
					else
					{
						narratorSource.clip = null;
					}

					narratorSource.Play ();
				}
				else if (PlayerPrefs.GetString ("Narrator") == "Super Spooky")
				{
					if (num < superNarrator.Count)
					{
						narratorSource.clip = superNarrator [num];
					}
					else
					{
						narratorSource.clip = null;
					}
					narratorSource.Play ();
				}
			}	
		}
		else
		{
			narratorSource.clip = null;
		}
	}
}
