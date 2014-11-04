using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlatformerController))]
public class PlatformerMechAnim : MonoBehaviour 
{
    Animator anim;
    PlatformerController platControl;

    private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int walkState = Animator.StringToHash("Base Layer.Walk");			
    static int runState = Animator.StringToHash("Base Layer.Run");			
    static int jumpState = Animator.StringToHash("Base Layer.Jump");				
    

	void Start() 
    {
        anim = GetComponent<Animator>();

        platControl = GetComponent<PlatformerController>();
	}
	
	void FixedUpdate() 
    {
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

        // Set speed of player
        anim.SetFloat("Speed", platControl.GetSpeed());

        // Determine if player is running or not
        anim.SetBool("Run", Input.GetButton("Run"));

        if (currentBaseState.nameHash == jumpState)
        {
            //  ..and not still in transition..
            if (!anim.IsInTransition(0))
            {
                // reset the Jump bool so we can jump again, and so that the state does not loop 
                //anim.SetBool("Jump", false);
            }
        }
	}

    void DidJump()
    {
        anim.SetBool("Jump", true);
    }

    void DidJumpReachApex()
    {
        anim.SetBool("JumpFall", true);
    }

    void DidLand()
    {
        anim.SetBool("Jump", false);
        anim.SetBool("JumpFall", false);
    }
}
