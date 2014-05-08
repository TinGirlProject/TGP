using UnityEngine;
using System.Collections;

public class PlayerController : Character 
{
	void Start() 
    {
        base.Start();

		animator.SetLayerWeight(1, 1);
	}
	
	void Update() 
    {
        base.Update();

        CheckMovement();
        CheckJump();
        CheckSlide();
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