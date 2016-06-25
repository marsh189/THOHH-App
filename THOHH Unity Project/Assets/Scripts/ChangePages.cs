using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangePages : MonoBehaviour {

	private Vector3 startClick;
	private Vector3 endClick;
	private float distance = 0f;
	private bool swiped = false;
	public RawImage pageIMG;
	public List<Texture> images;


	void Start()
	{
		pageIMG.GetComponent<RawImage> ().texture = images [0];
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
				if (deltaX > 0) 
				{ //swiped left
					int indexNext = images.IndexOf (pageIMG.GetComponent<RawImage> ().texture) + 1;
					pageIMG.GetComponent<RawImage> ().texture = (Texture)images [indexNext];
				} 
				else if (deltaX <= 0) 
				{ //swiped right
					int indexNext = images.IndexOf (pageIMG.GetComponent<RawImage> ().texture) - 1;
					pageIMG.GetComponent<RawImage> ().texture = (Texture)images [indexNext];
				}
			}
			swiped = true;
		}
	}
}
