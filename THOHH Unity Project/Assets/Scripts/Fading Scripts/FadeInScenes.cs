using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInScenes : MonoBehaviour {

	public Image fadeIMG;	//Black Image in foreground
	Color tempColor;		//used to Decrement the Alpha of image

	// Use this for initialization
	void Start () {
		fadeIMG.gameObject.SetActive (true);
		tempColor = fadeIMG.GetComponent<Image> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeIMG.GetComponent<Image> ().color.a > 0f) 
		{
			tempColor.a -= Time.deltaTime;
			fadeIMG.GetComponent<Image> ().color = tempColor;
		} 
		else 
		{
			fadeIMG.gameObject.SetActive (false);
		}
	}
}
