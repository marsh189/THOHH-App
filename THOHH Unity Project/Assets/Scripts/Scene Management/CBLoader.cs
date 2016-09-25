using UnityEngine;
using System.Collections;
using PaintCraft.Controllers;
using PaintCraft.Canvas.Configs;

public class CBLoader : MonoBehaviour 
{
	public CanvasController controller;

	public PageConfig witch;
	public PageConfig jLantern;
	public PageConfig chris;
	public PageConfig jason;
	public PageConfig zoey;
	public PageConfig daniel;
	public PageConfig puppy;
	public PageConfig parents;
	public PageConfig hohh;

	public int picked;

	//public string[] configList = ["Witch", "Jack-O-Lantern", "Chris", "Jason", "Zoey", "Daniel", "Puppy", "Parents", "HOHH"];
	// Use this for initialization
	void Awake ()
	{
		picked = PlayerPrefs.GetInt ("Page picked");

		switch (picked)
		{
		case(0):
			controller.PageConfig = witch;
			break;

		case(1):
			controller.PageConfig = jLantern;
			break;

		case(2):
			controller.PageConfig = chris;
			break;

		case(3):
			controller.PageConfig = jason;
			break;

		case(4):
			controller.PageConfig = zoey;
			break;

		case(5):
			controller.PageConfig = daniel;
			break;

		case(6):
			controller.PageConfig = puppy;
			break;

		case(7):
			controller.PageConfig = parents;
			break;

		case(8):
			controller.PageConfig = hohh;
			break;

		default:
			break;
		}
	}
}
