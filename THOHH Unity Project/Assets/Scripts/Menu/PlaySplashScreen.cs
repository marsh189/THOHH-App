using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySplashScreen : MonoBehaviour {

	public float wait = 15f;
	public Image fadeIMG;
	public string nextScene;
	public Color tempColor;

	float time;
	bool startFadeIn;

	// Use this for initialization
	void Start () 
	{
		startFadeIn = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startFadeIn)
		{
			if (tempColor.a <= 0f)
			{
				time += Time.deltaTime;
				if (time >= wait)
				{
					startFadeIn = false;
				}
				else if(Input.GetMouseButtonDown(0))
				{
					SceneManager.LoadScene (nextScene);
				}
			}
			else
			{
				tempColor.a -= 0.1f;
				fadeIMG.GetComponent<Image> ().color = tempColor;
			}
		}
		else
		{
			SceneManager.LoadScene (nextScene);
		}
	}
}

