using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour {

	public Slider awesome;
	public Slider anim;

	void Start()
	{
		Debug.Log ("HERE");
		awesome.value = (float)GetComponent<SavedData> ().awesomeBought;
		anim.value = (float)GetComponent<SavedData> ().pageAnimations;
	}

	public void Awesome (float val) 
	{
		PlayerPrefs.SetInt ("Awesome Bought", (int)val);
		PlayerPrefs.Save ();
	}
	public void PageAnims(float val)
	{
		PlayerPrefs.SetInt ("Page Animations", (int)val);
		PlayerPrefs.Save ();
	}
}
