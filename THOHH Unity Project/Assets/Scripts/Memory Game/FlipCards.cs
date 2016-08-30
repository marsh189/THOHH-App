using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FlipCards : MonoBehaviour {

	public Sprite tileFront;
	public ScoreKeeper score;

	public void Change()
	{
		if (score.firstTurned == null)
		{
			score.firstTurned = this.gameObject;
			gameObject.GetComponent<Image> ().sprite = tileFront;
		}
		else if (score.secondTurned == null)
		{
			score.secondTurned = this.gameObject;
			gameObject.GetComponent<Image> ().sprite = tileFront;
		}
	}
}

