using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
    private Transform m_transform;

    private CharacterCollisions m_collisions;
    public Movement m_movement;

    private float m_speed;
    private Movement.Direction m_direction;

    public Vector2 m_moveAmount;

    private bool m_jump;

	void Start()
    {
        m_collisions = GetComponent<CharacterCollisions>();
        m_movement = GetComponent<Movement>();
        m_transform = transform;

        Move(Movement.Direction.NONE);
        Run(false);

        m_moveAmount = new Vector2();
    }
        
	void FixedUpdate() 
    {
        // apply sideways movement
        m_moveAmount.x = m_speed * (int)m_direction * Time.deltaTime;
        // apply gravity
        m_moveAmount.y -= m_movement._gravity * Time.deltaTime;

        if (m_jump)
        {
            m_moveAmount.y += m_movement._jumpHeight * Time.deltaTime;
            m_jump = false;
            SendMessage("Jumped", SendMessageOptions.DontRequireReceiver);
        }

        // check for collisions and get modified movement
        m_moveAmount = m_collisions.TestMove(m_moveAmount);

        SendMessage("SetSpeed", Mathf.Abs(m_speed * (int)m_direction), SendMessageOptions.DontRequireReceiver);

        // move the character
        FinalMove(m_moveAmount);
	}

    /// <summary>
    /// Actually translate the characcter along the x / y axis.
    /// NO COLLISIONS ARE CHECKED HERE!
    /// </summary>
    /// <param name="amount">Vector2 represting the final amount to move.</param>
    void FinalMove(Vector2 amount)
    {
        transform.Translate(amount.x, amount.y, 0, Space.World);
    }

    /// <summary>
    /// Set the direction of the player.
    /// Uses enum Movement.Direction.
    /// </summary>
    /// <param name="dir">
    /// Movement.Direction represting the current direction of the character.
    /// Valid entries are Left, Right, NONE.
    /// </param>
    void Move(Movement.Direction dir)
    {
        switch (dir)
        {
            case Movement.Direction.Left:
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                m_direction = Movement.Direction.Left;
                break;
            case Movement.Direction.Right:
                transform.localRotation = Quaternion.Euler(0, -90, 0);
                m_direction = Movement.Direction.Right;
                break;
            case Movement.Direction.NONE:
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                m_direction = Movement.Direction.NONE;
                break;
        }
    }

    /// <summary>
    /// Make the character jump.
    /// Only works if the character is currently grounded.
    /// </summary>
    void Jump()
    {
        if (m_collisions.isGrounded())
        {
            m_jump = true;
        }
    }

    /// <summary>
    /// Sets the movement speed of the character.
    /// </summary>
    /// <param name="run">
    /// true sets current speed to movement._runSpeed.
    /// false sets current speed to movement._walkSpeed.
    /// </param>
    void Run(bool run)
    {
        switch (run)
        {
            case true:
                m_speed = m_movement._runSpeed;
                break;
            case false:
                m_speed = m_movement._walkSpeed;
                break;
        }
    }
}
