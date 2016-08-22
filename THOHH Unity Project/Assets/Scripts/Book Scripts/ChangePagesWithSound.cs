using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangePagesWithSound : MonoBehaviour 
{
	private Vector3 startClick;	//when click starts
	private Vector3 endClick;	//when click ends
	private ScrollingText sText;	//to start the credits
	int indexNext;			//next photo to be shown

	public int animIndex = 1;	//page number for animations
	public RawImage pageIMG;	//The image being changed
	public List<Texture> images;	//pages with images
	public List<Texture> blankImages;	//pages without images for animations
	public GameObject popUp;	//pop-up to continue reading
	public MediaPlayerCtrl videoCtrl;	//controls videos

	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.GetInt ("Page Number") > 0) //book has been started
		{
			popUp.SetActive (true);	//show pop-up

			//check if awesome version is bought and page animations is on
			if (PlayerPrefs.GetInt("Awesome Bought") == 1 && PlayerPrefs.GetInt("Page Animations") == 1)
			{
				videoCtrl.gameObject.SetActive (true);
				pageIMG.GetComponent<RawImage> ().texture = blankImages [PlayerPrefs.GetInt ("Page Number")];

				animIndex = PlayerPrefs.GetInt ("Page Number") + 1;
				videoCtrl.Load("page" + animIndex + "animation.mp4");
				videoCtrl.m_bAutoPlay = true;
				videoCtrl.m_bLoop = true;
			} 
			else //only show images of pages not animations
			{
				videoCtrl.gameObject.SetActive (false);
				pageIMG.GetComponent<RawImage> ().texture = images [PlayerPrefs.GetInt ("Page Number")];
			}
		}
		else //book is at the beginning
		{
			if (PlayerPrefs.GetInt("Awesome Bought") == 1 && PlayerPrefs.GetInt("Page Animations") == 1)
			{
				pageIMG.GetComponent<RawImage> ().texture = blankImages [0];
				videoCtrl.gameObject.SetActive(true);
				animIndex = 1;
				videoCtrl.Load("page" + animIndex + "animation.mp4");
				videoCtrl.m_bAutoPlay = true;
				videoCtrl.m_bLoop = true;
				GetComponent<BookSounds> ().PlayAudio (0);
			} 
			else 
			{
				videoCtrl.gameObject.SetActive (false);
				pageIMG.GetComponent<RawImage> ().texture = images [0];
				GetComponent<BookSounds> ().PlayAudio (0);
			}
		}

		sText = GetComponent<ScrollingText> ();
	}

	void Update () 
	{
		if(!popUp.activeInHierarchy)
		{
			if(Input.GetMouseButtonDown(0)) //finds position of mouse/finger when pressed
			{
				startClick = Input.mousePosition;
			}
			else if(Input.GetMouseButtonUp(0)) //finds positin of mouse/finger when let go
			{
				endClick = Input.mousePosition;
				float deltaX = startClick.x - endClick.x; //change in the x position of mouse/finger

				if(deltaX > 5f) //next page
				{ 
					if(PlayerPrefs.GetInt("Awesome Bought") == 1 && PlayerPrefs.GetInt("Page Animations") == 1)
					{
						indexNext = blankImages.IndexOf(pageIMG.GetComponent<RawImage>().texture) + 1;
						if(indexNext < images.Count)
						{
							animIndex++;
							pageIMG.GetComponent<RawImage>().texture = (Texture)blankImages[indexNext];
							videoCtrl.Load("page" + animIndex + "animation.mp4");
							videoCtrl.m_bAutoPlay = true;
							videoCtrl.m_bLoop = true;
							GetComponent<BookSounds> ().PlayAudio (indexNext);
						}
						else
						{
							sText.startScroll = true;
						}
					}
					else
					{
						indexNext = images.IndexOf(pageIMG.GetComponent<RawImage>().texture) + 1;
						if(indexNext < images.Count)
						{
							pageIMG.GetComponent<RawImage>().texture = (Texture)images[indexNext];
							GetComponent<BookSounds> ().PlayAudio (indexNext);
						}
						else
						{
							sText.startScroll = true;
						}
					}
				}
				else if(deltaX <= -5f) //prev page
				{
					if(PlayerPrefs.GetInt("Awesome Bought") == 1 && PlayerPrefs.GetInt("Page Animations") == 1)
					{
						int indexNext = blankImages.IndexOf(pageIMG.GetComponent<RawImage>().texture) - 1;
						if(indexNext >= 0) //checks if already on first page
						{
							animIndex--;
							videoCtrl.Load("page" + animIndex + "animation.mp4");
							videoCtrl.m_bAutoPlay = true;
							videoCtrl.m_bLoop = true;
						}
						else //keeps animation on first page
						{
							animIndex = 1;
							videoCtrl.Load("page1animation.mp4");
							videoCtrl.m_bAutoPlay = true;
							videoCtrl.m_bLoop = true;
						}
						pageIMG.GetComponent<RawImage>().texture = (Texture)blankImages[indexNext];
						GetComponent<BookSounds> ().PlayAudio (indexNext);
					}
					else
					{
						int indexNext = images.IndexOf (pageIMG.GetComponent<RawImage> ().texture) - 1;
						if (indexNext >= 0) //checks if already on first page
						{
							pageIMG.GetComponent<RawImage> ().texture = (Texture)images [indexNext];
							GetComponent<BookSounds> ().PlayAudio (indexNext);
						}
						else
						{
							indexNext = 0;
							pageIMG.GetComponent<RawImage> ().texture = (Texture)images [indexNext];
							GetComponent<BookSounds> ().PlayAudio (indexNext);
						}
					}
				}
				if(PlayerPrefs.GetInt("Awesome Bought") == 1 && PlayerPrefs.GetInt("Page Animations") == 1)
				{
					PlayerPrefs.SetInt("Page Number", blankImages.IndexOf(pageIMG.GetComponent<RawImage>().texture)); //Saves index of list item currently on.
					PlayerPrefs.Save();
				}
				else //Saves page index to continue reading later
				{
					PlayerPrefs.SetInt("Page Number", images.IndexOf(pageIMG.GetComponent<RawImage>().texture)); //Saves index of list item currently on.
					PlayerPrefs.Save();
				}
			}
		}
	}

	public void StartOver() //if start over button is clicked, go back to page 1
	{
		PlayerPrefs.SetInt ("Page Number", 0);
		if (PlayerPrefs.GetInt("Awesome Bought") == 1 && PlayerPrefs.GetInt("Page Animations") == 1)
		{
			pageIMG.GetComponent<RawImage> ().texture = (Texture)blankImages [0];
			videoCtrl.gameObject.SetActive (true);
			videoCtrl.Load ("page1animation.mp4");
			videoCtrl.m_bAutoPlay = true;
			videoCtrl.m_bLoop = true;
			animIndex = 1;
			GetComponent<BookSounds> ().PlayAudio (0);
		}
		else 
		{
			pageIMG.GetComponent<RawImage> ().texture = (Texture)images [0];
			videoCtrl.gameObject.SetActive (true);
			GetComponent<BookSounds> ().PlayAudio (0);
		}
		popUp.SetActive (false); //close pop-up
	}

	public void Continue()
	{
		GameObject.Find ("Pop-Up").SetActive (false);
		GetComponent<BookSounds> ().PlayAudio (PlayerPrefs.GetInt ("PageNumber") + 1);
	}
}
