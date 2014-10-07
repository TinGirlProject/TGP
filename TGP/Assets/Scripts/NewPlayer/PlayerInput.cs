using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
	void Update () 
    {
        // check for left and right movement
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                SendMessage("Move", Movement.Direction.Left);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                SendMessage("Move", Movement.Direction.Right);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                SendMessage("Move", Movement.Direction.NONE);
            }
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                SendMessage("Move", Movement.Direction.Left);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                SendMessage("Move", Movement.Direction.Right);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                SendMessage("Move", Movement.Direction.NONE);
            }
        }

        // check for jump
        if (Input.GetButtonDown("Jump"))
        {
            SendMessage("Jump");
        }

        if (Input.GetButtonDown("Run"))
        {
            SendMessage("Run", true);
        }
        else if (Input.GetButtonUp("Run"))
        {
            SendMessage("Run", false);
        }
	}
}
