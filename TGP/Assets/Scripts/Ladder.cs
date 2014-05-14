using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
	private bool playerInRange;
	private GameObject topRight;
	private GameObject topLeft;
	private GameObject bottom;

	// Use this for initialization
	void Start () 
	{
		playerInRange = false;
		topRight = transform.Find("LadderTopRight").gameObject;
		topLeft = transform.Find("LadderTopLeft").gameObject;
		bottom = transform.Find("LadderBottom").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void PlayerInRange(bool inRange)
	{
		playerInRange = inRange;
	}

	void OnGUI()
	{
		if (playerInRange)
		{
			GUI.Box(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.20f, 80, 30), "Press E");
		}
	}

	public GameObject TopLeft
	{
		get { return topLeft; }
	}

	public GameObject TopRight
	{
		get { return topRight; }
	}

	public GameObject Bottom
	{
		get { return bottom; }
	}
}
