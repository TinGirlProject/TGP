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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SendMessage("Move", Movement.Direction.Right);
        }
	}
}
