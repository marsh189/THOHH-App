using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour		//All code to navigate through game are here 
{

	public void MainMenu() //sends player to menu scene
	{
		SceneManager.LoadScene ("Main Menu");
	}

	public void ChangeScene(string name)	//sends player to the book
	{
		SceneManager.LoadScene (name);
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
