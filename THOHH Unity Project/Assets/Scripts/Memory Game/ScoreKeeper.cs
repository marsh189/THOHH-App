using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public GameObject firstTurned;
	public GameObject secondTurned;
	public Sprite back;
	public int matches;
	public Text matchCount;
	public float wait = 3f;
	public Text timer;

	float t;
	float waitTime;

	// Update is called once per frame
	void Update () 
	{
		if (PlayerPrefs.GetInt ("Timed") == 1)
		{
			t += Time.deltaTime;
			int minutes = Mathf.FloorToInt(t / 60F);
			int seconds = Mathf.FloorToInt(t - minutes * 60);
			string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

			timer.text = "Time " + niceTime;
		}
		else
		{
			timer.gameObject.SetActive (false);
		}

		if (secondTurned != null)
		{
			if (firstTurned.GetComponent<FlipCards> ().tileFront == secondTurned.GetComponent<FlipCards> ().tileFront)
			{
				if (waitTime >= wait)
				{
					waitTime = 0;
					matches += 1;
					firstTurned.SetActive (false);
					secondTurned.SetActive (false);
					firstTurned = null;
					secondTurned = null;
				}
				else
				{
					waitTime += Time.deltaTime;
				}
			}
			else
			{
				if (waitTime >= wait)
				{
					waitTime = 0;
					firstTurned.GetComponent<Image> ().sprite = back;
					secondTurned.GetComponent<Image> ().sprite = back;
					firstTurned = null;
					secondTurned = null;
				}
				else
				{
					waitTime += Time.deltaTime;
				}
			}
		}

		matchCount.text = "Match Count " + matches;
	}
}
