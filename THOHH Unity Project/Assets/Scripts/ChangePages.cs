using UnityEngine;
using System.Collections;

public class ChangePages : MonoBehaviour {

	private Touch initialTouch = new Touch ();
	private float distance = 0f;
	private bool swiped = false;

	void FixedUpdate () 
	{
		foreach (Touch t in Input.touches) 
		{
			if (t.phase == TouchPhase.Began) 
			{
				initialTouch = t;
			} 
			else if (t.phase == TouchPhase.Moved && !swiped) 
			{
				//distance formula
				float deltaX = initialTouch.position.x - t.position.x;
				float deltaY = initialTouch.position.y - t.position.y;
				distance = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
				//direction
				if (distance > 100f) 
				{
					if (deltaX > 0) //swiped left
					{
						this.gameObject.SetActive (false);
						//BackPage ();
					} 
					else if(deltaX <= 0) //swiped right
					{
						this.gameObject.SetActive (true);
						//NextPage ();
					}
				}

				swiped = true;
			}
			else if (t.phase == TouchPhase.Ended) 
			{
				initialTouch = new Touch ();
				swiped = false;
			}
		}
	}
}
