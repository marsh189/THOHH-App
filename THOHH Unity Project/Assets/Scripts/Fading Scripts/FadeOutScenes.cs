using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOutScenes : MonoBehaviour 
{

	public bool startFade;
	public bool endFade;
	public Image fadeIMG;	//Black Image in foreground
	public Color tempColor;		//used to Increment the Alpha of image

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
			tempColor.a += Time.deltaTime;
			fadeIMG.GetComponent<Image> ().color = tempColor;

			if(tempColor.a >= 1f)
			{
				startFade = false;
				endFade = true;
			}
		} 
	}
}
