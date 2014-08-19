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
        top = transform.Find("LadderTop").gameObject;
		bottom = transform.Find("LadderBottom").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void PlayerInRange(bool inRange)
	{
        Messenger<bool>.Broadcast("ShowEPress", inRange);
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
