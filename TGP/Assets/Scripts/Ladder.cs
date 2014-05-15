using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
	private bool playerInRange;
    private GameObject top;
    [SerializeField]
	private GameObject topRight;
    [SerializeField]
	private GameObject topLeft;
	private GameObject bottom;

	// Use this for initialization
	void Start () 
	{
		playerInRange = false;
        top = transform.Find("LadderTop").gameObject;
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

    public GameObject Top
    {
        get { return top; }
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
