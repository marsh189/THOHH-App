using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySplashScreen : MonoBehaviour {

	public float wait = 6f;
	public Image fadeIMG;
	float time;
	Color tempColor;

	// Use this for initialization
	void Start () 
	{
		//((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
		tempColor = fadeIMG.GetComponent<Image> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (fadeIMG.GetComponent<Image>().color.a);
		if (time >= wait) 
		{
			tempColor.a += Time.deltaTime;
			fadeIMG.GetComponent<Image> ().color = tempColor;
			if (fadeIMG.GetComponent<Image>().color.a >= 1f) 
			{
				SceneManager.LoadScene ("Main Menu");
			}
		} 
		else 
		{
			time += Time.deltaTime;
		}
	}
}
