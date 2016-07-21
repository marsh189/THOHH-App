using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeToScene : MonoBehaviour //Fades out to a given scene when button is clicked
{
	[HideInInspector]
	public string sceneName = "Main Menu";  //App will load this scene after fade

	public Color col;  //Current Color
	public bool startFade = false;  //if true, color will start to fade
	public bool endFade = false; //if true, scene will change to sceneName
	public Image fadeIMG;	//Black Image in foreground

	// Update is called once per frame
	void Update () 
	{
		if (startFade) 
		{
			fadeIMG.gameObject.SetActive (true);
			col.a += Time.deltaTime;
			fadeIMG.GetComponent<Image> ().color = col;

			if (col.a >= 1f) 
			{
				endFade = true;
			}
		} 
		if(endFade) 
		{
			SceneManager.LoadScene (sceneName);
		}
	}

	public void StartToFade(string name)
	{
		sceneName = name;
		startFade = true;
	}
}
