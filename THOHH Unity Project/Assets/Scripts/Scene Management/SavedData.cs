using UnityEngine;
using System.Collections;

public class SavedData : MonoBehaviour 	//not sure if needed, update this script with all
{										//PlayerPrefs we have for a refenrence

	public int closedDisclaimer;	//if 1, disclaimer was closed and should not come up again
	public int pageNumber; //Page index currently on (must subtract 2 to get actual page number)
	public int awesomeBought;//if 1, Awesome version was bought;
	public int pageAnimations; //if 1, page animation toggle is on
	public int musicOn;			//if 1, music is on
	public string narration;	//name of narrator turned on; if off, no narrations

	void Update()
	{
		pageNumber = PlayerPrefs.GetInt ("Page Number");
		closedDisclaimer = PlayerPrefs.GetInt ("Closed Disclaimer");
		awesomeBought = PlayerPrefs.GetInt ("Awesome Bought");
		pageAnimations = PlayerPrefs.GetInt ("Page Animations");
		musicOn = PlayerPrefs.GetInt ("Music");
		narration = PlayerPrefs.GetString ("Narrator");
	}
}