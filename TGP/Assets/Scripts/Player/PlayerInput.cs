using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
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
	}
}
