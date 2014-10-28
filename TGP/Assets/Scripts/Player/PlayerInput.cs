using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    #region Mouse Constants
    public const string LEFTMOUSEDOWN = "LeftMouseDown";
    public const string LEFTMOUSE = "LeftMouse";
    public const string LEFTMOUSEUP = "LeftMouseUp";
    public const string RIGHTMOUSEDOWN = "RightMouseDown";
    public const string RIGHTMOUSE = "RightMouse";
    public const string RIGHTMOUSEUP = "RightMouseUp";
    public const string MIDDLEMOUSEDOWN = "MiddleMouseDown";
    public const string MIDDLEMOUSE = "MiddleMouse";
    public const string MIDDLEMOUSEUP = "MiddleMouseUp";
    #endregion

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
        #region Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            Messenger.Broadcast(LEFTMOUSEDOWN, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButton(0))
        {
            Messenger.Broadcast(LEFTMOUSE, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Messenger.Broadcast(LEFTMOUSEUP, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Messenger.Broadcast(RIGHTMOUSEDOWN, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButton(1))
        {
            Messenger.Broadcast(RIGHTMOUSE, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButtonUp(1))
        {
            Messenger.Broadcast(RIGHTMOUSEUP, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButtonDown(2))
        {
            Messenger.Broadcast(MIDDLEMOUSEDOWN, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButton(2))
        {
            Messenger.Broadcast(MIDDLEMOUSE, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        if (Input.GetMouseButtonUp(2))
        {
            Messenger.Broadcast(MIDDLEMOUSEUP, MessengerMode.DONT_REQUIRE_LISTENER);
        }
        #endregion
    }
}