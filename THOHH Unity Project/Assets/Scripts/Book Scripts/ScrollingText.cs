using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{

	public bool startScroll = false;
	public Canvas creditCanvas;
	public GameObject text;
	public Text url;
	public float speed = 0.2f;
	public float wait = 3f;
	public Transform endPos;

	float time;
	Color tempColor;

	// Use this for initialization
	void Start () 
	{
		startScroll = false;
		tempColor = url.GetComponent<Text> ().color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startScroll)
		{
			GetComponent<FadeOutScenes> ().startFade = true;
			creditCanvas.gameObject.SetActive (true);

			if (GetComponent<FadeOutScenes>().endFade) 
			{
				PlayerPrefs.SetInt ("Page Number", 0);

				if (text.transform.position.y >= endPos.position.y) 
				{
					if (speed > 0)
					{
						speed--;
						text.transform.Translate (Vector3.up * Time.deltaTime * speed);
					}
					else
					{
						if (time < wait)
						{
							time += Time.deltaTime;
						}
						else
						{
							if (url.GetComponent<Text> ().color.a > 0f)
							{
								tempColor.a -= Time.deltaTime;
								url.GetComponent<Text> ().color = tempColor;
							}
							else
							{
								SceneManager.LoadScene ("Main Menu");
							}
						}
					}
				}
				else
				{
					text.transform.Translate (Vector3.up * Time.deltaTime * speed);
				}
			}
		}
	}
}
