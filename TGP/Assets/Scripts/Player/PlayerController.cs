using UnityEngine;
using System.Collections;

public class PlayerController : Character 
{
    private bool _slingshotDrawn;
    public GameObject slingshotGO;

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

		GameManager.s_listOfTimers.Add(enterLadderBottom);
		GameManager.s_listOfTimers.Add(enterLadderTopLeft);
		GameManager.s_listOfTimers.Add(enterLadderTopRight);
		GameManager.s_listOfTimers.Add(exitLadderBottom);
		GameManager.s_listOfTimers.Add(exitLadderTopLeft);
		GameManager.s_listOfTimers.Add(exitLadderTopRight);

        _slingshotDrawn = false;
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
			transform.position = new Vector3(curLadder.transform.position.x, curLadderTop.transform.position.y - transform.localScale.y * 3, 0);
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
            transform.position = new Vector3(curLadder.transform.position.x, curLadderTop.transform.position.y - transform.localScale.y * 3, 0);
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
            transform.position = new Vector3(curLadder.transform.position.x, curLadderBottom.transform.position.y + transform.localScale.y, 0);
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
            curLadder.SendMessage("PlayerInRange", true, SendMessageOptions.RequireReceiver);

			ladderState = LadderState.ENTERBOTTOM;
			inAirState = InAirState.NONE;
			canMove = true;
		}
		#endregion

		base.Update();

		if (canMove)
		{
			CheckMovement();
			CheckJump();
			//CheckSlide();
        }
        #region Ladder Handling
        if (curLadder != null && Input.GetKeyDown(KeyCode.E) && inAirState != InAirState.CLIMBING)
		{
			animator.SetFloat("Speed", 0);
            switch (ladderState)
			{
			case LadderState.ENTERTOPLEFT:
				canMove = false;
                curLadder.SendMessage("PlayerInRange", false, SendMessageOptions.RequireReceiver);
				animator.SetTrigger("EnterLadderTopLeft");
				enterLadderTopLeft.StartTimer();
			    inAirState = InAirState.CLIMBING;
				break;
			case LadderState.ENTERTOPRIGHT:
				canMove = false;
				animator.SetTrigger("EnterLadderTopRight");
				enterLadderTopRight.StartTimer();
			    inAirState = InAirState.CLIMBING;
				break;
			case LadderState.ENTERBOTTOM:
				if (inAirState != InAirState.CLIMBING)
				{
                    Debug.Log("Enter Bottom");
                    canMove = false;
                    curLadder.SendMessage("PlayerInRange", false, SendMessageOptions.RequireReceiver);
					animator.SetTrigger("EnterLadderBottom");
                    enterLadderBottom.StartTimer();
                    inAirState = InAirState.CLIMBING;
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
        #endregion

        if (PlayerInventory.HasSlingshot)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                if (!_slingshotDrawn)
                {
                    slingshotGO.SetActive(true);
                    _slingshotDrawn = true;
                    slingshotGO.SendMessage("SlingshotDrawn", _slingshotDrawn, SendMessageOptions.RequireReceiver);
                }
                else
                {
                    _slingshotDrawn = false;
                    slingshotGO.SendMessage("SlingshotDrawn", _slingshotDrawn, SendMessageOptions.RequireReceiver);
                    slingshotGO.SetActive(false);
                }
            }

            if (_slingshotDrawn && PlayerInventory.CanFireSlingshot)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    slingshotGO.SendMessage("Shoot", SendMessageOptions.RequireReceiver);
                }
            }
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