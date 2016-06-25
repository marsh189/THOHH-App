using UnityEngine;
using System.Collections;

public class SavedData : MonoBehaviour 	//not sure if needed, update this script with all
{										//PlayerPrefs we have for a refenrence

	public int closedDisclaimer;	//if 1, disclaimer was closed and should not come up again
	public int pageNumber; //Page index currently on (must subtract 2 to get actual page number)
	public bool awesomeBought; //true if Awesome version was bought;

	void Update()
	{
		pageNumber = PlayerPrefs.GetInt ("Page Number");
		closedDisclaimer = PlayerPrefs.GetInt ("Closed Disclaimer");
	}

}
