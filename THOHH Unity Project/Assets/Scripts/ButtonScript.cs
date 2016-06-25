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
		SceneManager.LoadScene ("Main Menu");
	}

	public void BookScene()	//sends player to the book
	{
		SceneManager.LoadScene ("Book");
	}

	public void CloseDisclaimer() //closes the disclaimer pop-up
	{
		GameObject.Find ("Disclaimer").SetActive (false);
		PlayerPrefs.SetInt ("Closed Disclaimer", 1);
	}

	public void CloseWindow() //closes window when button is clicked
	{
		GameObject.Find ("Pop-Up").SetActive (false);
	}
		
	public void Reset()	//Resets all data (may not need this in game, may help for testing purposes)
	{
		PlayerPrefs.DeleteAll ();
	}
}
