using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangePages : MonoBehaviour {

	private Vector3 startClick;
	private Vector3 endClick;
	private float distance = 0f;
	private bool swiped = false;
	int indexNext;

	public RawImage pageIMG;
	public List<Texture> images;
	public Canvas endBook;
	public GameObject popUp;

	void Start()
	{
		if (PlayerPrefs.GetInt("Page Number") > 2) 
		{
			popUp.SetActive (true);
			pageIMG.GetComponent<RawImage> ().texture = images [PlayerPrefs.GetInt("Page Number")];
		} 
		else 
		{
			pageIMG.GetComponent<RawImage> ().texture = images [0];
		}
	}
	void FixedUpdate () 
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			startClick = Input.mousePosition;
		} 
		else if (Input.GetMouseButtonUp(0)) 
		{
			startClick = new Vector3 ();
			swiped = false; 
		}
	}

	void OnMouseDrag()
	{
		if (!swiped) 
		{
			endClick = Input.mousePosition;

			//distance formula
			float deltaX = startClick.x - endClick.x;
			float deltaY = startClick.y - endClick.y;
			distance = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));

			//direction
			if (distance > 10f) 
			{
				if (deltaX > 0) //next page
				{ 
					indexNext = images.IndexOf (pageIMG.GetComponent<RawImage> ().texture) + 1;
					if (indexNext < images.Count) 
					{
						pageIMG.GetComponent<RawImage> ().texture = (Texture)images [indexNext];
					} 
					else 
					{
						pageIMG.gameObject.SetActive (false);
						endBook.gameObject.SetActive (true);
					}
				} 
				else if (deltaX <= 0) //prev page
				{ //swiped right
					int indexNext = images.IndexOf (pageIMG.GetComponent<RawImage> ().texture) - 1;
					pageIMG.GetComponent<RawImage> ().texture = (Texture)images [indexNext];
				}
			}
			swiped = true;
			PlayerPrefs.SetInt ("Page Number", images.IndexOf (pageIMG.GetComponent<RawImage> ().texture)); //Saves index of list item currently on.
			PlayerPrefs.Save();
		}
	}

	public void StartOver()
	{
		PlayerPrefs.SetInt ("Page Number", 0);
		pageIMG.GetComponent<RawImage> ().texture = (Texture)images [0];
		popUp.SetActive (false);
	}
}
