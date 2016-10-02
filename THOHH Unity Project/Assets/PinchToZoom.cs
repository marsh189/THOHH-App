using UnityEngine;
using System.Collections;

public class PinchToZoom : MonoBehaviour
{
	public float persZoomSpeed = 0.5f;        
	public float orthoZoomSpeed = 0.5f;  


	void Update()
	{
		// Counts 2 touches on the phone
		if (Input.touchCount == 2)
		{
			Touch touch0 = Input.GetTouch(0);
			Touch touch1 = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
			Vector2 touch1Prev = touch1.position - touch1.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTDM = (touch0Prev - touch1Prev).magnitude;
			float TDM = (touch0.position - touch1.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaDiff = prevTDM - TDM;


			if (Camera.current.orthographic)
			{
				Camera.current.orthographicSize += deltaDiff * orthoZoomSpeed;

				Camera.current.orthographicSize = Mathf.Max(Camera.current.orthographicSize, 0.1f);
			}
			else
			{
				Camera.current.fieldOfView += deltaDiff * persZoomSpeed;

				Camera.current.fieldOfView = Mathf.Clamp(Camera.current.fieldOfView, 0.1f, 179.9f);
			}
		}
	}
}
