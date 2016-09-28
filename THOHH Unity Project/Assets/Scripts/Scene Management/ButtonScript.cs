using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour		//All code to navigate through game are here 
{
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
		}
	}
	public void MainMenu() //sends player to menu scene
	{
		SceneManager.LoadScene ("Main Menu");
	}

	public void ChangeScene(string name)	//sends player to the book
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1 || name == "Book" || name == "Main Menu")
		{
			SceneManager.LoadScene (name);
		}
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

	public void SaveLastScene(string name) //saves the scene you left to go back to later
	{
		Scene last = SceneManager.GetActiveScene();
		string lastSceneName = last.name;

		if (PlayerPrefs.GetInt ("Awesome Bought") == 1)
		{
			PlayerPrefs.SetString ("Last Scene", lastSceneName);
			SceneManager.LoadScene (name);
		}
		else
		{
			//Buy Awesome Version pop up
		}
	}

	public void Back() //Back to the last scene
	{
		if (PlayerPrefs.GetString ("Last Scene") == "Main Menu")
		{
			SceneManager.LoadScene (PlayerPrefs.GetString ("Last Scene"));
		}
		else
		{
			GetComponent<LoadingScreen> ().SceneChange (PlayerPrefs.GetString ("Last Scene"));
		}
	}
		
	public void Reset()	//Resets all data (may not need this in game, may help for testing purposes)
	{
		PlayerPrefs.DeleteAll ();
	}

	public void SetInactive(GameObject hide) //Sets Active to false on objects
	{
		hide.gameObject.SetActive (false);
	}

	public void Awesome()
	{
		PlayerPrefs.SetInt ("Awesome Bought", 1);
		PlayerPrefs.SetFloat ("Narration Volume", 1f);
		PlayerPrefs.SetString ("Narrator", "Spooky");

	}

}
