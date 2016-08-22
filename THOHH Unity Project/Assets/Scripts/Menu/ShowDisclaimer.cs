using UnityEngine;
using System.Collections;

public class ShowDisclaimer : MonoBehaviour 
{
	public GameObject dis;

	void Start () 
	{
		if(PlayerPrefs.GetInt ("Closed Disclaimer") == 1)
		{
			dis.SetActive (false);
		}
		else if (PlayerPrefs.GetInt ("Closed Disclaimer") == 0) 
		{
			dis.SetActive (true);
		} 
	}

	// Update is called once per frame
	void Update () 
	{
		if(PlayerPrefs.GetInt ("Closed Disclaimer") == 1)
		{
			dis.SetActive (false);
		}
		else if (PlayerPrefs.GetInt ("Closed Disclaimer") == 0) 
		{
			dis.SetActive (true);
		} 
	}
}
