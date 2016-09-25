using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColoringPagePick : MonoBehaviour 
{
	int curPosition = 0; //keeps track of the which page is being chosen
	public Button pageButton;
	public Button left;
	public Button right;

	public Sprite[] coloringPages;


	// Use this for initialization
	void Start () 
	{
		checkLowerPosition (left);
		checkUpperPosition (right);
	}

	public void nextButton() // increments the counter and changes picture accordingly
	{
		checkLowerPosition (left);
		checkUpperPosition (right);

		curPosition += 1;

		switch (curPosition)
		{
		case(0):
			pageButton.image.sprite = coloringPages[0];
			break;

		case(1):
			pageButton.image.sprite = coloringPages[1];
			break;

		case(2):
			pageButton.image.sprite = coloringPages[2];
			break;

		case(3):
			pageButton.image.sprite = coloringPages[3];
			break;

		case(4):
			pageButton.image.sprite = coloringPages[4];
			break; 

		case(5):
			pageButton.image.sprite = coloringPages[5];
			break; 

		case(6):
			pageButton.image.sprite = coloringPages[6];
			break; 

		case(7):
			pageButton.image.sprite = coloringPages[7];
			break;

		case(8):
			pageButton.image.sprite = coloringPages[8];
			break;

		default:
			break;
		}

		checkLowerPosition (left);
		checkUpperPosition (right);
	}

	public void prevButton() //changes counter and picture
	{
		checkLowerPosition (left);
		checkUpperPosition (right);

		curPosition = curPosition - 1; // decrements the counter 

		switch (curPosition)
		{
		case(0):
			pageButton.image.sprite = coloringPages[0];
			break;

		case(1):
			pageButton.image.sprite = coloringPages[1];
			break;

		case(2):
			pageButton.image.sprite = coloringPages[2];
			break;

		case(3):
			pageButton.image.sprite = coloringPages[3];
			break;

		case(4):
			pageButton.image.sprite = coloringPages[4];
			break; 

		case(5):
			pageButton.image.sprite = coloringPages[5];
			break; 

		case(6):
			pageButton.image.sprite = coloringPages[6];
			break; 

		case(7):
			pageButton.image.sprite = coloringPages[7];
			break;

		case(8):
			pageButton.image.sprite = coloringPages[8];
			break;

		default:
			break;
		}

		checkLowerPosition (left);
		checkUpperPosition (right);
	}

	public void pictureSelect() //Saves the page picked
	{
		PlayerPrefs.SetInt ("Page picked", curPosition);
		PlayerPrefs.Save ();
	}

	public void checkUpperPosition(Button name) // checks for the end of the list
	{
		if (curPosition.Equals (8))
		{
			name.GetComponent<Button> ().interactable = false;
		}
		else
		{
			name.GetComponent<Button> ().interactable = true;
		}
	}

	public void checkLowerPosition(Button name) // checks for the beginning of the list
	{
		if (curPosition.Equals (0))
		{
			name.GetComponent<Button> ().interactable = false;
		}
		else
		{
			name.GetComponent<Button> ().interactable = true;	
		}
	}

}
