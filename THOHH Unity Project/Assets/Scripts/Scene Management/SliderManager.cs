using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour {

	public Slider awesome;
	public Slider anim;
	public Slider mus;

	void Start()
	{
		awesome.value = (float)PlayerPrefs.GetInt ("Awesome Bought");

		if (awesome.value == 1) 
		{
			anim.interactable = true;
			mus.interactable = true;
			anim.value = (float)PlayerPrefs.GetInt ("Page Animations");
			mus.value = (float)PlayerPrefs.GetInt ("Music");
		} 
		else 
		{	
			anim.interactable = false;
			mus.interactable = false;
			anim.value = 0f;
			mus.value = 0f;
		}
	}
	void Update()
	{
		if (awesome.value == 1) 
		{
			anim.interactable = true;
			mus.interactable = true;
			anim.value = (float)PlayerPrefs.GetInt ("Page Animations");
			mus.value = (float)PlayerPrefs.GetInt ("Music");
		} 
		else 
		{	
			anim.interactable = false;
			mus.interactable = false;
			anim.value = 0f;
			mus.value = 0f;
		}
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

	public void Music(float val)
	{
		PlayerPrefs.SetInt ("Music", (int)val);
		PlayerPrefs.Save ();
	}
}
