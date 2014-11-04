using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public static string[] s_InputStrings = {  "Mouse_Left_Down", "Mouse_Left", "Mouse_Left_Up",
                                            "Mouse_Right_Down", "Mouse_Right", "Mouse_Right_Up",
                                            "Mouse_Middle_Down", "Mouse_Middle", "Mouse_Middle_Up",
                                            "Key_E_Down", "Key_E", "Key_E_Up" };

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
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_LEFTDOWN], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButton(0))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_LEFT], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_LEFTUP], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_RIGHTDOWN], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButton(1))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_RIGHT], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_RIGHTUP], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButtonDown(2))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_MIDDLEDOWN], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButton(2))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_MIDDLE], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetMouseButtonUp(2))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.MOUSE_MIDDLEUP], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        #endregion

        #region Keyboard Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.KEY_EDOWN], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.KEY_E], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Messenger.Broadcast(s_InputStrings[(int)InputMessage.KEY_EUP], MessengerMode.DONT_REQUIRE_LISTENER);
        }
        #endregion
    }
}

/// <summary>
/// Enum to allow in editor changes to controls.
/// </summary>
public enum InputMessage
{
    MOUSE_LEFTDOWN,
    MOUSE_LEFT,
    MOUSE_LEFTUP,
    MOUSE_RIGHTDOWN,
    MOUSE_RIGHT,
    MOUSE_RIGHTUP,
    MOUSE_MIDDLEDOWN,
    MOUSE_MIDDLE,
    MOUSE_MIDDLEUP,
    KEY_EDOWN,
    KEY_E,
    KEY_EUP
}