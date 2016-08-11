using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetUpButtons : MonoBehaviour {

	public Button numButton;
	public Button charButton;
	public Button alphaButton;
	public Button timedButton;

	public Sprite numOn;
	public Sprite charOn;
	public Sprite alphaOn;
	public Sprite timedOn;

	public Sprite numOff;
	public Sprite charOff;
	public Sprite alphaOff;
	public Sprite timedOff;

	void Start()
	{
		if (PlayerPrefs.GetInt ("123s") == 1)
		{
			numButton.GetComponent<Image> ().sprite = numOn;
		}
		if (PlayerPrefs.GetInt ("ABCs") == 1)
		{
			alphaButton.GetComponent<Image> ().sprite = alphaOn;
		}
		if (PlayerPrefs.GetInt ("Characters") == 1)
		{
			charButton.GetComponent<Image> ().sprite = charOn;
		}
		if (PlayerPrefs.GetInt ("Timed") == 1)
		{
			timedButton.GetComponent<Image> ().sprite = timedOn;
		}
	}

	public void ToggleNumButton()
	{
		if (PlayerPrefs.GetInt ("123s") == 1)
		{
			numButton.GetComponent<Image> ().sprite = numOff;
			PlayerPrefs.SetInt ("123s", 0);
		}
		else if(PlayerPrefs.GetInt ("123s") == 0)
		{
			numButton.GetComponent<Image> ().sprite = numOn;
			PlayerPrefs.SetInt ("123s", 1);
		}
	}
	public void ToggleAlphaButton()
	{
		if (PlayerPrefs.GetInt ("ABCs") == 1)
		{
			alphaButton.GetComponent<Image> ().sprite = alphaOff;
			PlayerPrefs.SetInt ("ABCs", 0);
		}
		else if(PlayerPrefs.GetInt ("ABCs") == 0)
		{
			alphaButton.GetComponent<Image> ().sprite = alphaOn;
			PlayerPrefs.SetInt ("ABCs", 1);
		}
	}
	public void ToggleCharButton()
	{
		if (PlayerPrefs.GetInt ("Characters") == 1)
		{
			charButton.GetComponent<Image> ().sprite = charOff;
			PlayerPrefs.SetInt ("Characters", 0);
		}
		else if(PlayerPrefs.GetInt ("Characters") == 0)
		{
			charButton.GetComponent<Image> ().sprite = charOn;
			PlayerPrefs.SetInt ("Characters", 1);
		}
	}
	public void ToggleTimedButton()
	{
		if (PlayerPrefs.GetInt ("Timed") == 1)
		{
			timedButton.GetComponent<Image> ().sprite = timedOff;
			PlayerPrefs.SetInt ("Timed", 0);
		}
		else if(PlayerPrefs.GetInt ("Timed") == 0)
		{
			timedButton.GetComponent<Image> ().sprite = timedOn;
			PlayerPrefs.SetInt ("Timed", 1);
		}
	}
}