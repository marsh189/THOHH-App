using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{

	public bool startScroll = false;
	public Canvas creditCanvas;
	public GameObject text;
	public float speed = 0.2f;
	public Transform endPos;

	// Use this for initialization
	void Start () 
	{
		startScroll = false;
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
					SceneManager.LoadScene("Main Menu");
				}
				else
				{
					text.transform.Translate (Vector3.up * Time.deltaTime * speed);
				}
			}
		}
	}
}
