﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MemoryLoad : MonoBehaviour {

	public GameObject background;
	public GameObject text;
	public GameObject progressBar;
	public GameObject Canvas;
	public GameObject errorText;

	int loadProgress = 0;

	// Use this for initialization
	void Start () 
	{
		background.SetActive (false);
		text.SetActive (false);
		progressBar.SetActive (false);
		errorText.SetActive (false);
	}

	public void SceneChange(string levelToLoad)
	{
		if (PlayerPrefs.GetInt ("123s") == 0 && PlayerPrefs.GetInt ("ABCs") == 0 && PlayerPrefs.GetInt ("Characters") == 0)
		{
			errorText.SetActive (true);
		}
		else
		{
		StartCoroutine (DisplayLoadingScreen (levelToLoad));
		}
	}

	IEnumerator DisplayLoadingScreen(string level)
	{
		Canvas.SetActive (false);
		background.SetActive (true);
		text.SetActive (true);
		progressBar.SetActive (true);

		progressBar.transform.localScale = new Vector3 (loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
		text.GetComponent<GUIText>().text = "Loading Progress " + loadProgress + "%";
		AsyncOperation async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone)
		{
			loadProgress = (int)(async.progress * 100);
			text.GetComponent<GUIText>().text = "Loading Progress " + loadProgress + "%";
			progressBar.transform.localScale = new Vector3 (async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
			yield return null;
		}
	}
}