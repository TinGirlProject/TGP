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

		GameManager.listOfTimers.Add(enterLadderBottom);
		GameManager.listOfTimers.Add(enterLadderTopLeft);
		GameManager.listOfTimers.Add(enterLadderTopRight);
	}
	
	void Update()
	{
		#region Timer Handling
		if (enterLadderTopLeft.IsTimeComplete)
		{
			enterLadderTopLeft.ResetTimer();
			animator.SetBool("OnLadder", true);
			canMove = true;
		}
		if (enterLadderTopRight.IsTimeComplete)
		{
			enterLadderTopRight.ResetTimer();
			animator.SetBool("OnLadder", true);
			canMove = true;
		}
		if (enterLadderBottom.IsTimeComplete)
		{
			enterLadderBottom.ResetTimer();
			animator.SetBool("OnLadder", true);
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
					canMove = false;
					animator.SetTrigger("EnterLadderBottom");
					enterLadderBottom.StartTimer();
					break;
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