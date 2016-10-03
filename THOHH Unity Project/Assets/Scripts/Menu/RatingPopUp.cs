using UnityEngine;
using System.Collections;

public class RatingPopUp : MonoBehaviour {

	public GameObject rating;

	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.GetInt("Closed Disclaimer") > 0 && PlayerPrefs.GetInt ("Rating Pop Up") == 0)
		{
			rating.SetActive (true);
			rating.GetComponent<AudioSource> ().Play ();
		}
	}

	public void GoRate()
	{
		PlayerPrefs.SetInt ("Rating Pop Up", 2);
		rating.SetActive (false);
		Application.OpenURL ("market://detail?id=com.MrFoxBooks.TheHouseOnHauntedHollow");
	}

	public void Cancel()
	{
		PlayerPrefs.SetInt ("Rating Pop Up", 1);
		rating.SetActive (false);
	}

}
