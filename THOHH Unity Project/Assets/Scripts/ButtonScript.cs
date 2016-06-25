using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour		//All code to navigate through game are here 
{
	public Image fadeIMG;	//Black Image in foreground
	Color tempColor;		//used to Decrement the Alpha of image

	void Start()
	{
		tempColor = fadeIMG.GetComponent<Image> ().color;
	}

	public void MainMenu() //sends player to menu scene
	{
		StartFade ("Main Menu");
	}

	public void Reset()	//Resets all data (may not need this in game, may help for testing purposes)
	{
		PlayerPrefs.DeleteAll ();
	}

	void StartFade(string scene)
	{
		tempColor = fadeIMG.GetComponent<Image> ().color;

		fadeIMG.gameObject.SetActive (true);
		while (fadeIMG.GetComponent<Image> ().color.a < 1f) 
		{
			tempColor.a += Time.deltaTime;
			fadeIMG.GetComponent<Image> ().color = tempColor;
		}  

		SceneManager.LoadScene (scene);
	}
}
