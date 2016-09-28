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
			this.GetComponent<Button> ().interactable = false;
			GetComponent<AudioSource> ().Play ();
		}
		else if (score.firstTurned.name != this.name && score.secondTurned == null)
		{
			score.secondTurned = this.gameObject;
			gameObject.GetComponent<Image> ().sprite = tileFront;
			this.GetComponent<Button> ().interactable = false;
			GetComponent<AudioSource> ().Play ();
		}
	}
}

