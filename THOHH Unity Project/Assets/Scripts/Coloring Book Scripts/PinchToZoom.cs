using UnityEngine;
using System.Collections;

public class PinchToZoom : MonoBehaviour {

	public Camera cam;
	public float orthoZoomSpeed = 0.5f;

	// Update is called once per frame
	void Update () 
	{
		//If there are 3 touches on device...
		if (Input.touchCount == 2)
		{
			//store both touches
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch (1);

			//Find position in previous frame of each touch
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			//Find magnitude of distance vector
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			//Find difference in distance between each frame
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			//Camera is Orhtographic
			if (cam.orthographic)
			{
				//change size
				cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

				//make sure size never drops below zero
				cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);

				if (cam.orthographicSize >= 512)
				{
					cam.orthographicSize = 512;
				}
					
			}
		}
	}
}
