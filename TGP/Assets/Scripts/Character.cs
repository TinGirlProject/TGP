using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterCollision))]
public class Character : MonoBehaviour
{
    // Character health
    protected float health;
    protected float maxHealth;

    //protected Weapon equippedWeapon;

    private CharacterCollision cp;
    public GameObject ragdoll;

    // Character Handling
    public class Movement
    {
        public float gravity = 20;
        public float walkSpeed = 8;
        public float runSpeed = 12;
        public float acceleration = 30;
        public float jumpHeight = 8;
        public float slideDeceleration = 10;
        public float initiateSlideThreshold = 9;
    }
    public Movement movement;

    // Character Speeds
    private float animationSpeed;

    private float currentSpeedX;
    private float currentSpeedY;
    private float targetSpeedX;
    private float targetSpeedY;

	protected bool canMove;

    // Move directions
    private float moveDirX;
    private float moveDirY;

    // Collider values
    private Vector3 slideColliderSize = new Vector3(10.3f, 1.5f, 3);
    private Vector3 slideColliderCentre = new Vector3(.35f, .75f, 0);

	// Climbing
	protected GameObject curLadder;
    protected GameObject curLadderTop;
	protected GameObject curLadderTopLeft;
	protected GameObject curLadderTopRight;
	protected GameObject curLadderBottom;
	protected Timer enterLadderTopRight;
	protected Timer enterLadderTopLeft;
	protected Timer enterLadderBottom;
	protected Timer exitLadderTopRight;
	protected Timer exitLadderTopLeft;
	protected Timer exitLadderBottom;
	protected bool enteredLeft;
	protected bool enteredRight;
	protected bool enteredBottom;

    #region States
    // Character
    public enum InAirState { JUMPING, FALLING, WALLHOLDING, CLIMBING, NONE };
    public InAirState inAirState;

    public enum GroundedState { SLIDING, STANDING, CROUCHING, NONE };
    private GroundedState groundedState;

    public enum PaceState { WALKING, RUNNING, NONE };
    private PaceState paceState;

    // Wall Jumping
    private WallJumpState wallJumpState;
    private enum WallJumpState { HOLDING, JUMPING, NONE }

    // Ladders
    public LadderState ladderState;
	public enum LadderState { TOP, BOTTOM, MIDDLE, NONE, ENTERTOPLEFT, ENTERTOPRIGHT, ENTERBOTTOM, EXITING }
    #endregion

    // Components
    protected Animator animator;

    protected void Start()
    {
        cp = GetComponent<CharacterCollision>();
        movement = new Movement();

        animator = GetComponent<Animator>();
        
        ChangeState(InAirState.NONE);
        ChangeState(GroundedState.STANDING);
        ChangeState(PaceState.WALKING);
		ladderState = LadderState.NONE;
		wallJumpState = WallJumpState.NONE;

		canMove = true;
		enteredLeft = false;
		enteredRight = false;
		enteredBottom = false;
    }

    #region Health
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Ragdoll r = (Instantiate(ragdoll, transform.position, transform.rotation) as GameObject).GetComponent<Ragdoll>();
        r.CopyPose(transform);
        Destroy(this.gameObject);
    }
    #endregion

	void OnTriggerEnter(Collider other)
	{
		if (inAirState == InAirState.CLIMBING)
		{
			if (other.tag == "LadderTop")
				ladderState = LadderState.TOP;
			else if (other.tag == "LadderBottom")
				ladderState = LadderState.BOTTOM;
		}
	}

	void OnTriggerStay(Collider other)
    {
		//if (other.tag == "Ladder")
		//{
		//	ladderState = LadderState.MIDDLE;
		//}
		//else if (other.tag == "LadderTop")
		//{
		//	ladderState = LadderState.TOP;
		//}
		//else if (other.tag == "LadderBottom")
		//{
		//	ladderState = LadderState.BOTTOM;
		//}
    }

    void OnTriggerExit(Collider other)
    {
		//if (other.tag == "Ladder")
		//{
		//	ladderState = LadderState.NONE;

		//	if (inAirState == InAirState.CLIMBING)
		//	{
		//		ChangeState(InAirState.FALLING);
		//	}
		//}
		//else if (other.tag == "LadderTop")
		//{
		//	ladderState = LadderState.NONE;
		//}
		//else if (other.tag == "LadderBottom")
		//{
		//	ladderState = LadderState.NONE;
		//}
    }

    public void ChangeState(System.Enum newState)
    {
        if (newState is InAirState)
        {
            // Previous In Air States
            switch (inAirState)
            {
                case InAirState.JUMPING:
                    animator.SetBool("Jumping", false);
                    break;
                case InAirState.WALLHOLDING:
                    animator.SetBool("Wall Hold", false);
                    break;
            }

            // New In Air States
            switch ((InAirState)newState)
            {
                case InAirState.CLIMBING:
                    break;
                case InAirState.JUMPING:
                    // Set jump height
                    currentSpeedY = movement.jumpHeight;

                    ChangeState(GroundedState.NONE);
                    animator.SetBool("Jumping", true);
                    break;
                case InAirState.FALLING:
                    if (currentSpeedY > 0)
                    {
                        currentSpeedY = 0;
                    }

                    animator.SetBool("Jumping", false);
                    break;
                case InAirState.WALLHOLDING:
                    animator.SetBool("Wall Hold", true);
                    break;
                case InAirState.NONE:
                    // Stop jump
                    currentSpeedY = 0;

                    ChangeState(GroundedState.STANDING);
                    break;
            }

            inAirState = (InAirState)newState;
        }
        else if (newState is GroundedState)
        {
            // Previous Grounded States
            switch (groundedState)
            {
                case GroundedState.SLIDING:
                    cp.ResetCollider();
                    animator.SetBool("Sliding", false);
                    break;
            }

            // New Grounded States
            switch ((GroundedState)newState)
            {
                case GroundedState.CROUCHING:
                    break;
                case GroundedState.SLIDING:
                    targetSpeedX = 0;
					Debug.Log("Change to Slide state");
                    cp.SetCollider(slideColliderSize, slideColliderCentre);
                    animator.SetBool("Sliding", true);
                    break;
                case GroundedState.STANDING:
                    break;
                case GroundedState.NONE:
                    break;
            }

            groundedState = (GroundedState)newState;
        }
        else if (newState is PaceState)
        {
            switch ((PaceState)newState)
            {
                case PaceState.RUNNING:
                    break;
                case PaceState.WALKING:
                    break;
                case PaceState.NONE:
                    break;
            }

            paceState = (PaceState)newState;
        }
    }

    public InAirState getInAirState() { return inAirState; }
    public GroundedState getGroundedState() { return groundedState; }
    public PaceState getPaceState() { return paceState; }

    protected void Move(float directionX, float directionY)
    {
		moveDirX = directionX;
		moveDirY = directionY;

        if (moveDirX == 0)
        {
            ChangeState(PaceState.NONE);
        }
    }

    protected void Run()
    {
        targetSpeedY = movement.runSpeed;
        ChangeState(PaceState.RUNNING);
    }

    protected void Walk() 
    {
        targetSpeedY = movement.walkSpeed;
        ChangeState(PaceState.WALKING);
    }

    protected void Slide(bool start)
    {
        if (groundedState != GroundedState.NONE)
        {
            if (!start)
            {
                ChangeState(GroundedState.STANDING);
            }
            else if (Mathf.Abs(currentSpeedX) > movement.initiateSlideThreshold)
            {
                ChangeState(GroundedState.SLIDING);
            }
        }
    }

    protected void Jump()
    {
        if (groundedState == GroundedState.SLIDING)
        {
            ChangeState(GroundedState.STANDING);
        }
        // Allow jumping in only these states
        else if ((groundedState != GroundedState.NONE) || 
					inAirState == InAirState.WALLHOLDING)
        {
			ChangeState(InAirState.JUMPING);
        }
    }

    protected void WallHold()
    {
        if (inAirState != InAirState.WALLHOLDING && groundedState == GroundedState.NONE)
        {
            ChangeState(InAirState.WALLHOLDING);
        }
    }

    protected void Update()
    {
        // Handle movement speed
        switch (paceState)
        {
            case PaceState.NONE:
                targetSpeedX = 0;
                currentSpeedX = 0;
                break;
            case PaceState.WALKING:
                targetSpeedX = movement.walkSpeed;
                break;
            case PaceState.RUNNING:
                targetSpeedX = movement.runSpeed;
                break;
        }

        HandleHorizontalMovement();
        HandleVerticalMovement();

        Vector2 amountToMove;

        // Set amount to move
        amountToMove.x = currentSpeedX;
        amountToMove.y = currentSpeedY;

        if (canMove)
            if (amountToMove.x != 0 || amountToMove.y != 0)
                cp.Position(amountToMove * Time.deltaTime);
    }

    private void HandleHorizontalMovement()
    {
		if (inAirState != InAirState.CLIMBING)
		{
			// If character is touching the ground
			if (groundedState != GroundedState.NONE)
			{
				// Slide logic
				if (groundedState == GroundedState.SLIDING)
				{
					if (Mathf.Abs(currentSpeedX) < .25f)
					{
						ChangeState(GroundedState.STANDING);
					}
				}
			}

			// Set animator parameters
			animationSpeed = MathP.IncrementTowards(animationSpeed, Mathf.Abs(targetSpeedX), movement.acceleration);
			animator.SetFloat("Speed", animationSpeed);

			// Left and right movement
			if (groundedState != GroundedState.SLIDING)
			{
				currentSpeedX = MathP.IncrementTowards(currentSpeedX, moveDirX * targetSpeedX, movement.acceleration);

				// Face Direction
				if (moveDirX != 0 && inAirState != InAirState.WALLHOLDING)
				{
					transform.eulerAngles = (moveDirX > 0) ? Vector3.up * 180 : Vector3.zero;
				}
			}
			else
			{
				currentSpeedX = MathP.IncrementTowards(currentSpeedX, moveDirX * targetSpeedX, movement.slideDeceleration);
			}
		}
    }

    private void HandleVerticalMovement()
    {
        if (groundedState != GroundedState.NONE)
        {
            currentSpeedY = 0;
        }

        // Set state to falling if character is falling
        if (currentSpeedY < 0 && (inAirState != InAirState.FALLING && inAirState != InAirState.WALLHOLDING
            && inAirState != InAirState.CLIMBING))
        {
            ChangeState(InAirState.FALLING);
        }

        // If player is next to a ladder
        if (ladderState != LadderState.NONE)
        {
            if (((moveDirY == 1) || (moveDirY == -1 && groundedState != GroundedState.NONE))
                && ladderState == LadderState.MIDDLE /*|| ladderState == LadderState.BOTTOM*/)
            {
                ChangeState(InAirState.CLIMBING);
            }

            if (!((ladderState == LadderState.TOP && moveDirY <= 0)
                /*|| (ladderState == LadderState.BOTTOM && moveDirY >= 0)*/
                || ladderState == LadderState.MIDDLE))
            {
                moveDirY = 0;
            }
        }

        // Deal with vertical movement
        if (inAirState == InAirState.WALLHOLDING)
        {
            currentSpeedX = 0;
            if (moveDirY != -1)
            {
                currentSpeedY = 0;
            }
        }
        else if (inAirState == InAirState.CLIMBING)
        {
            //currentSpeedY = MathP.IncrementTowards(currentSpeedY, moveDirY * targetSpeedX, movement.acceleration);
            currentSpeedY = moveDirY * targetSpeedY;
        }

        // Apply gravity
        if (inAirState != InAirState.CLIMBING)
        {
            currentSpeedY -= movement.gravity * Time.deltaTime;
        }
    }

	private void SetCurLadder(GameObject ladder)
	{
		Ladder theLadder = ladder.transform.parent.GetComponent<Ladder>();

		if (ladder.name == "LadderTopLeft")
		{
			ladderState = LadderState.ENTERTOPLEFT;
			enteredLeft = true;
			enteredRight = false;
			enteredBottom = false;
            curLadderTop = theLadder.Top;
			curLadderTopLeft = ladder;
			curLadderTopRight = theLadder.TopRight;
			curLadderBottom = theLadder.Bottom;
		}
		else if (ladder.name == "LadderTopRight")
		{
			ladderState = LadderState.ENTERTOPRIGHT;
			enteredLeft = false;
			enteredRight = true;
            enteredBottom = false;
            curLadderTop = theLadder.Top;
			curLadderTopLeft = theLadder.TopLeft;
			curLadderTopRight = ladder;
			curLadderBottom = theLadder.Bottom;
		}
		else if (ladder.name == "LadderBottom")
		{
			if (inAirState != InAirState.CLIMBING)
			{
				ladderState = LadderState.ENTERBOTTOM;
				enteredLeft = false;
				enteredRight = false;
                enteredBottom = true;
                curLadderTop = theLadder.Top;
				curLadderTopLeft = theLadder.TopLeft;
				curLadderTopRight = theLadder.TopRight;
				curLadderBottom = ladder;
			}
		}
		curLadder = ladder.transform.parent.gameObject;
	}

	private void ResetCurLadder()
	{
		if (ladderState != LadderState.MIDDLE)
		{
            curLadder = null;
            curLadderTop = null;
			curLadderTopLeft = null;
			curLadderTopRight = null;
			curLadderBottom = null;
			enteredLeft = false;
			enteredRight = false;
			enteredBottom = false;
			ladderState = LadderState.NONE;
		}
	}
}