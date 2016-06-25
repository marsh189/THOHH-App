using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOutScenes : MonoBehaviour 
{

	public bool startFade;
	public Image fadeIMG;	//Black Image in foreground
	Color tempColor;		//used to Decrement the Alpha of image

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		if (startFade) 
		{
			fadeIMG.gameObject.SetActive (true);
			if (fadeIMG.GetComponent<Image> ().color.a < 1f) 
			{
				tempColor.a += Time.deltaTime;
				fadeIMG.GetComponent<Image> ().color = tempColor;
			} 
			else 
			{

			}
		} 
		else 
		{
			tempColor = fadeIMG.GetComponent<Image> ().color;
		}
	}
}
