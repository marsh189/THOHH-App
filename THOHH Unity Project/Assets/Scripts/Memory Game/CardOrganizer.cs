using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardOrganizer : MonoBehaviour {

	public List<Button> tileList;
	List<Sprite> tempTiles = new List<Sprite> ();

	List<Sprite> cardFronts = new List<Sprite>();

	public List<Sprite> abcTiles;
	public List<Sprite> numTiles;
	public List<Sprite> charTiles;

	int category;

	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.GetInt ("ABCs") == 1)
		{
			if (PlayerPrefs.GetInt ("Characters") == 1 && PlayerPrefs.GetInt ("ABCs") == 1)
			{
				for (int i = 0; i < 9; i++)
				{
					category = Random.Range (1, 4);
					if (category == 1)
					{
						int index = Random.Range (0, abcTiles.Count);
						if (abcTiles [index] != null)
						{
							cardFronts.Add (abcTiles [index]);
							cardFronts.Add (abcTiles [index]);
							abcTiles.RemoveAt (index);
						}
					}
					else if (category == 2)
					{
						int index = Random.Range (0, numTiles.Count);
						if (numTiles [index] != null)
						{
							cardFronts.Add (numTiles [index]);
							cardFronts.Add (numTiles [index]);
							numTiles.RemoveAt (index);
						}
					}
					else if (category == 3)
					{
						int index = Random.Range (0, charTiles.Count);
						if (charTiles [index] != null)
						{
							cardFronts.Add (charTiles [index]);
							cardFronts.Add (charTiles [index]);
							charTiles.RemoveAt (index);
						}
					}
				}
			}
			else if (PlayerPrefs.GetInt ("123s") == 1)
			{
				for (int i = 0; i < 9; i++)
				{
					category = Random.Range (1, 3);
					if (category == 1)
					{
						int index = Random.Range (0, abcTiles.Count);
						if (abcTiles [index] != null)
						{
							cardFronts.Add (abcTiles [index]);
							cardFronts.Add (abcTiles [index]);
							abcTiles.RemoveAt (index);
						}
					}
					else if (category == 2)
					{
						int index = Random.Range (0, numTiles.Count);
						if (numTiles [index] != null)
						{
							cardFronts.Add (numTiles [index]);
							cardFronts.Add (numTiles [index]);
							numTiles.RemoveAt (index);
						}
					}
				}
			}
			else if (PlayerPrefs.GetInt ("Characters") == 1)
			{
				for (int i = 0; i < 9; i++)
				{
					category = Random.Range (1, 3);
					if (category == 1)
					{
						int index = Random.Range (0, abcTiles.Count);
						if (abcTiles [index] != null)
						{
							cardFronts.Add (abcTiles [index]);
							cardFronts.Add (abcTiles [index]);
							abcTiles.RemoveAt (index);
						}
					}
					else if (category == 2)
					{
						int index = Random.Range (0, charTiles.Count);
						if (charTiles [index] != null)
						{
							cardFronts.Add (charTiles [index]);
							cardFronts.Add (charTiles [index]);
							charTiles.RemoveAt (index);
						}
					}
				}
			}
			else if (PlayerPrefs.GetInt ("123s") == 0 && PlayerPrefs.GetInt ("Characters") == 0)
			{

				for (int i = 0; i < 9; i++)
				{
					int index = Random.Range (0, abcTiles.Count);
					if (abcTiles [index] != null)
					{
						cardFronts.Add (abcTiles [index]);
						cardFronts.Add (abcTiles [index]);
						abcTiles.RemoveAt (index);
					}
				}
			}
		}
		else if (PlayerPrefs.GetInt ("123s") == 1)
		{
			if (PlayerPrefs.GetInt ("Characters") == 1)
			{
				for (int i = 0; i < 9; i++)
				{
					category = Random.Range (1, 3);
					if (category == 1)
					{
						int index = Random.Range (0, numTiles.Count);
						if (numTiles [index] != null)
						{
							cardFronts.Add (numTiles [index]);
							cardFronts.Add (numTiles [index]);
							numTiles.RemoveAt (index);
						}
					}
					else if (category == 2)
					{
						int index = Random.Range (0, charTiles.Count);
						if (charTiles [index] != null)
						{
							cardFronts.Add (charTiles [index]);
							cardFronts.Add (charTiles [index]);
							charTiles.RemoveAt (index);
						}
					}
				}
			}
			else if (PlayerPrefs.GetInt ("ABCs") == 0 && PlayerPrefs.GetInt ("Characters") == 0)
			{

				for (int i = 0; i < 9; i++)
				{
					int index = Random.Range (0, numTiles.Count);
					if (numTiles [index] != null)
					{
						cardFronts.Add (numTiles [index]);
						cardFronts.Add (numTiles [index]);
						numTiles.RemoveAt (index);
					}
				}
			}
		}
		else if (PlayerPrefs.GetInt ("Characters") == 1)
		{
			for (int i = 0; i < 9; i++)
			{
				int index = Random.Range (0, charTiles.Count);
				if (charTiles [index] != null)
				{
					cardFronts.Add (charTiles [index]);
					cardFronts.Add (charTiles [index]);
					charTiles.RemoveAt (index);
				}
			}
		}

		tempTiles = cardFronts;
		for (int i = 0; i < tileList.Count; i++)
		{
			int rand = Random.Range (0, tempTiles.Count);
			tileList [i].GetComponent<FlipCards> ().tileFront = tempTiles [rand];
			tempTiles.RemoveAt (rand);
		}
	}

	public void ChangeCard()
	{
	}
}

