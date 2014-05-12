using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
	private bool playerInRange;

	// Use this for initialization
	void Start () 
	{
		playerInRange = false;
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
}
