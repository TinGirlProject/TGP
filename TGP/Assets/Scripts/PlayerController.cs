using UnityEngine;
using System.Collections;

public class PlayerController : Character 
{
	void Start() 
    {
        base.Start();

		animator.SetLayerWeight(1, 1);
		enterLadderBottom = new Timer(1.0f);
		enterLadderTopLeft = new Timer(1.0f);
		enterLadderTopRight = new Timer(1.0f);
		exitLadderBottom = new Timer(1.0f);
		exitLadderTopLeft = new Timer(1.0f);
		exitLadderTopRight = new Timer(1.0f);

		GameManager.listOfTimers.Add(enterLadderBottom);
		GameManager.listOfTimers.Add(enterLadderTopLeft);
		GameManager.listOfTimers.Add(enterLadderTopRight);
		GameManager.listOfTimers.Add(exitLadderBottom);
		GameManager.listOfTimers.Add(exitLadderTopLeft);
		GameManager.listOfTimers.Add(exitLadderTopRight);
	}
	
	void Update()
	{
		#region Timer Handling
		/*
		 * Entering Ladders
		 */
		if (enterLadderTopLeft.IsTimeComplete)
		{
			enterLadderTopLeft.ResetTimer();
			animator.SetBool("OnLadder", true);
			transform.position = new Vector3(curLadder.transform.position.x, curLadder.transform.position.y + curLadder.transform.localScale.y * 2.5f, 0);
			// TEMP
			transform.Rotate(Vector3.up, 270, Space.Self);

			ladderState = LadderState.MIDDLE;
			inAirState = InAirState.CLIMBING;
			canMove = true;
		}
		else if (enterLadderTopRight.IsTimeComplete)
		{
			enterLadderTopRight.ResetTimer();
			animator.SetBool("OnLadder", true);
			transform.position = new Vector3(curLadder.transform.position.x, curLadder.transform.position.y + curLadder.transform.localScale.y * 2.5f, 0);
			// TEMP
			transform.Rotate(Vector3.up, 90, Space.Self);

			ladderState = LadderState.MIDDLE;
			inAirState = InAirState.CLIMBING;
			canMove = true;
		}
		else if (enterLadderBottom.IsTimeComplete)
		{
			enterLadderBottom.ResetTimer();
			animator.SetBool("OnLadder", true);
			transform.position = new Vector3(curLadder.transform.position.x, curLadder.transform.position.y - curLadder.transform.localScale.y * 3.5f, 0);
			// TEMP
			if (transform.eulerAngles.y > 0)
				transform.Rotate(Vector3.up, 270, Space.Self);
			else
				transform.Rotate(Vector3.up, 90, Space.Self);

			ladderState = LadderState.MIDDLE;
			inAirState = InAirState.CLIMBING;
			canMove = true;
		}

		/*
		 * Exiting Ladders
		 */
		if (exitLadderTopLeft.IsTimeComplete)
		{
			exitLadderTopLeft.ResetTimer();
			animator.SetBool("OnLadder", false);
			transform.position = curLadderTopLeft.transform.position;
			// TEMP
			transform.Rotate(Vector3.up, -90, Space.Self);

			ladderState = LadderState.NONE;
			inAirState = InAirState.NONE;
			canMove = true;
		}
		else if (exitLadderTopRight.IsTimeComplete)
		{
			exitLadderTopRight.ResetTimer();
			animator.SetBool("OnLadder", false);
			transform.position = curLadderTopRight.transform.position;
			// TEMP
			transform.Rotate(Vector3.up, 90, Space.Self);

			ladderState = LadderState.NONE;
			inAirState = InAirState.NONE;
			canMove = true;
		}
		else if (exitLadderBottom.IsTimeComplete)
		{
			exitLadderBottom.ResetTimer();
			animator.SetBool("OnLadder", false);
			transform.position = curLadderBottom.transform.position;
			// TEMP
			transform.Rotate(Vector3.up, 90, Space.Self);

			ladderState = LadderState.NONE;
			inAirState = InAirState.NONE;
			canMove = true;
		}
		#endregion

		base.Update();

		if (canMove)
		{
			CheckMovement();
			CheckJump();
			CheckSlide();
		}

		if (curLadder != null && Input.GetKeyDown(KeyCode.E))
		{
			switch (ladderState)
			{
			case LadderState.ENTERTOPLEFT:
				canMove = false;
				animator.SetTrigger("EnterLadderTopLeft");
				enterLadderTopLeft.StartTimer();
				break;
			case LadderState.ENTERTOPRIGHT:
				canMove = false;
				animator.SetTrigger("EnterLadderTopRight");
				enterLadderTopRight.StartTimer();
				break;
			case LadderState.ENTERBOTTOM:
				if (inAirState != InAirState.CLIMBING)
				{
					canMove = false;
					animator.SetTrigger("EnterLadderBottom");
					enterLadderBottom.StartTimer();
				}
			break;
			}
		}

		if (ladderState == LadderState.TOP)
		{
			ladderState = LadderState.EXITING;
			if (enteredLeft)
			{
				canMove = false;
				animator.SetTrigger("ExitLadderTopLeft");
				exitLadderTopLeft.StartTimer();
			}
			else if (enteredRight)
			{
				canMove = false;
				animator.SetTrigger("ExitLadderTopRight");
				exitLadderTopRight.StartTimer();
			}
			else if (enteredBottom)
			{
				canMove = false;
				if (curLadderTopRight)
				{
					animator.SetTrigger("ExitLadderTopRight");
					exitLadderTopRight.StartTimer();
				}
				else if (curLadderTopLeft)
				{
					animator.SetTrigger("ExitLadderTopLeft");
					exitLadderTopLeft.StartTimer();
				}
			}
		}
		else if (ladderState == LadderState.BOTTOM)
		{
			ladderState = LadderState.EXITING;
			canMove = false;
			animator.SetTrigger("ExitLadderBottom");
			exitLadderBottom.StartTimer();
		}
	}

    void CheckMovement()
    {
        // Speed of player and movement
        if (Input.GetButton("Run"))
        {
            Run();
        }
        else
        {
            Walk();
        }

        // Horizontal and vertical input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Move(horizontalInput, verticalInput);
    }

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void CheckSlide()
    {
        // Slide Input
        if (Input.GetButtonDown("Slide"))
        {
			Debug.Log("Slide");
			Slide(true);
        }
        else if (Input.GetButtonUp("Slide"))
        {
            Slide(false);
        }
    }
}