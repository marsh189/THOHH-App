using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour {

	public Toggle superToggle;
	public Toggle spookyToggle;
	public Toggle OffToggle;

	void Update()
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1) 
		{
			superToggle.interactable = true;
			spookyToggle.interactable = true;
			OffToggle.interactable = true;
		} 
		else 
		{
			superToggle.interactable = false;
			spookyToggle.interactable = false;
			OffToggle.interactable = false;
			superToggle.isOn = false;
			spookyToggle.isOn = false;
			OffToggle.isOn = true;
		}

		if (superToggle.isOn) 
		{
			PlayerPrefs.SetString ("Narrator", "Super Spooky");
			PlayerPrefs.Save ();
		}
		else if (spookyToggle.isOn) 
		{
			PlayerPrefs.SetString ("Narrator", "Spooky");
			PlayerPrefs.Save ();
		}
		else if (OffToggle.isOn) 
		{
			PlayerPrefs.SetString ("Narrator", "Off");
			PlayerPrefs.Save ();
		}
	}

	void Start()
	{
		if (PlayerPrefs.GetInt ("Awesome Bought") == 1) 
		{
			if (PlayerPrefs.GetString ("Narrator") == "Super Spooky") 
			{
				superToggle.isOn = true;
				spookyToggle.isOn = false;
				OffToggle.isOn = false;
			} 
			else if (PlayerPrefs.GetString ("Narrator") == "Spooky") 
			{
				superToggle.isOn = false;
				spookyToggle.isOn = true;
				OffToggle.isOn = false;
			} 
			else 
			{
				superToggle.isOn = false;
				spookyToggle.isOn = false;
				OffToggle.isOn = true;
			}
		} 
		else 
		{
			superToggle.interactable = false;
			spookyToggle.interactable = false;
			OffToggle.interactable = false;
			superToggle.isOn = false;
			spookyToggle.isOn = false;
			OffToggle.isOn = true;
		}
	}

}
