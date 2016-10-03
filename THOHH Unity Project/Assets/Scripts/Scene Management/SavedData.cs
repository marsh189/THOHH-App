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
	public int numOn;
	public int charOn;
	public int alphaOn;
	public int timedOn;
	public string lastScene;
	public int rating;

	void Update()
	{
		pageNumber = PlayerPrefs.GetInt ("Page Number");
		closedDisclaimer = PlayerPrefs.GetInt ("Closed Disclaimer");
		awesomeBought = PlayerPrefs.GetInt ("Awesome Bought");
		musicOn = PlayerPrefs.GetInt ("Music");
		narration = PlayerPrefs.GetString ("Narrator");
		numOn = PlayerPrefs.GetInt ("123s");
		charOn = PlayerPrefs.GetInt ("Characters");
		alphaOn = PlayerPrefs.GetInt ("ABCs");
		timedOn = PlayerPrefs.GetInt ("Timed");
		lastScene = PlayerPrefs.GetString ("Last Scene");
		rating = PlayerPrefs.GetInt ("Rating Pop Up");
	}
}