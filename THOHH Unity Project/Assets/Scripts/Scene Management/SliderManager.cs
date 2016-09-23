using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour {

	public Slider music;
	public Slider autoTurn;
	public Slider musicVolume;
	public Slider narrationVolume;
	public Slider narration;

	public Sprite musicOff;
	public Sprite autoPageOff;
	public Sprite musicOn;
	public Sprite autoPageOn;

	public Sprite narLeverOn;
	public Sprite musLeverOn;
	public Sprite autoLeverOn;
	public Sprite narLeverOff;
	public Sprite musLeverOff;
	public Sprite autoLeverOff;

	public GameObject musLight;
	public GameObject autoLight;
	public GameObject narHandle;
	public GameObject musicHandle;
	public GameObject autoHandle;

	void Start()
	{
		music.value = (float)PlayerPrefs.GetInt ("Music");
		autoTurn.value = (float)PlayerPrefs.GetInt ("Auto Turn");
		narration.value = (float)PlayerPrefs.GetInt ("Narration");
		musicVolume.value = PlayerPrefs.GetFloat ("Music Volume");
		narrationVolume.value = PlayerPrefs.GetFloat ("Narration Volume");
		if (PlayerPrefs.GetInt ("Music") == 1)
		{
			musLight.GetComponent<Image> ().sprite = musicOn;
			musicHandle.GetComponent<Image> ().sprite = musLeverOn;
		}
		else
		{
			musLight.GetComponent<Image> ().sprite = musicOff;
		}
		if (PlayerPrefs.GetInt ("Auto Turn") == 1)
		{
			autoLight.GetComponent<Image> ().sprite = autoPageOn;
			autoHandle.GetComponent<Image> ().sprite = autoLeverOn;
		}
		else
		{
			musLight.GetComponent<Image> ().sprite = autoPageOff;
		}
		if (PlayerPrefs.GetInt ("Narration") == 1)
		{
			narHandle.GetComponent<Image> ().sprite = narLeverOn;
		}
	}


	public void NarrationsToggle () 
	{
		PlayerPrefs.SetInt ("Narration", (int)narration.value);
		PlayerPrefs.Save ();

		if (PlayerPrefs.GetInt ("Narration") == 1)
		{
			narHandle.GetComponent<Image> ().sprite = narLeverOn;
			PlayerPrefs.SetString ("Narrator", "Super Spooky");
		}
		else
		{
			narHandle.GetComponent<Image> ().sprite = narLeverOff;
			PlayerPrefs.SetString ("Narrator", "Spooky");
		}
	}

	public void NarrationsVolume()
	{
		PlayerPrefs.SetFloat ("Narration Volume", narrationVolume.value);
		PlayerPrefs.Save ();
	}

	public void MusicToggle()
	{
		PlayerPrefs.SetInt ("Music", (int)music.value);
		PlayerPrefs.Save ();

		if (PlayerPrefs.GetInt ("Music") == 1)
		{
			musicHandle.GetComponent<Image> ().sprite = musLeverOn;
			musLight.GetComponent<Image> ().sprite = musicOn;
		}
		else
		{
			musicHandle.GetComponent<Image> ().sprite = musLeverOff;
			musLight.GetComponent<Image> ().sprite = musicOff;
		}
	}

	public void MusicVolume()
	{
		PlayerPrefs.SetFloat("Music Volume", musicVolume.value);
		PlayerPrefs.Save ();
	}

	public void AutoTurnToggle()
	{
		PlayerPrefs.SetInt ("Auto Turn", (int)autoTurn.value);
		PlayerPrefs.Save ();

		if (PlayerPrefs.GetInt ("Auto Turn") == 1)
		{
			autoHandle.GetComponent<Image> ().sprite = autoLeverOn;
			autoLight.GetComponent<Image> ().sprite = autoPageOn;
		}
		else
		{
			autoHandle.GetComponent<Image> ().sprite = autoLeverOff;
			autoLight.GetComponent<Image> ().sprite = autoPageOff;
		}
	}
}
