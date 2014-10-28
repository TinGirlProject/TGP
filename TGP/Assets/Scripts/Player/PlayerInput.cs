using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public const string LEFTMOUSEDOWN = "LeftMouseDown";
    public const string LEFTMOUSE = "LeftMouse";
    public const string LEFTMOUSEUP = "LeftMouseUp";
    public const string RIGHTMOUSEDOWN = "RightMouseDown";
    public const string RIGHTMOUSE = "RightMouse";
    public const string RIGHTMOUSEUP = "RightMouseUp";

    // Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SendMessage("Move", Movement.Direction.Left);
        }
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			if (!Input.GetKey(KeyCode.RightArrow))
			{
				SendMessage("Move", Movement.Direction.NONE);
			}
		}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SendMessage("Move", Movement.Direction.Right);
        }
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			if (!Input.GetKey(KeyCode.LeftArrow))
			{
				SendMessage("Move", Movement.Direction.NONE);
			}
		}
		if (Input.GetButtonDown("Jump"))
		{
			SendMessage("Jump");
		}

        if (Input.GetMouseButtonUp(0))
        {
            Messenger.Broadcast(LEFTMOUSEUP);
        }

        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButton(0))
        {

        }
	}
}