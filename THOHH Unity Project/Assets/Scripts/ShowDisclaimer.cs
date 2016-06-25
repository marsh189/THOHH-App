using UnityEngine;
using System.Collections;

public class ShowDisclaimer : MonoBehaviour 
{
	public GameObject dis;

	// Update is called once per frame
	void Update () 
	{
		if (GetComponent<SavedData>().closedDisclaimer == 0) 
		{
			dis.SetActive (true);
		} 
		else if(GetComponent<SavedData>().closedDisclaimer == 1)
		{
			dis.SetActive (false);
		}
	}
}
