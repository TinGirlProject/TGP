using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
    private Transform m_transform;

    private CharacterCollisions m_collisions;
    public Movement m_movement;

    private float m_speed;
    private int m_direction;

    public Vector2 m_moveAmount;

    private bool m_jump;

	void Start()
    {
        m_collisions = GetComponent<CharacterCollisions>();
        m_movement = GetComponent<Movement>();
        m_transform = transform;

        m_speed = m_movement._walkSpeed;
        m_moveAmount = new Vector2();
    }
        
	void FixedUpdate() 
    {
        // apply sideways movement
        m_moveAmount.x = m_speed * m_direction * Time.deltaTime;
        // apply gravity
        m_moveAmount.y -= m_movement._gravity * Time.deltaTime;

        if (m_jump)
        {
            m_moveAmount.y += m_movement._jumpHeight * Time.deltaTime;
            m_jump = false;
        }

        // check for collisions and get modified movement
        m_moveAmount = m_collisions.TestMove(m_moveAmount);

        // move the character
        DoMove(m_moveAmount);
	}

    void DoMove(Vector2 amount)
    {
        m_transform.position += new Vector3(amount.x, amount.y, 0);
    }

    void Move(Movement.Direction dir)
    {
        switch (dir)
        {
            case Movement.Direction.Left:
                if (_previousDirection == Direction.Right)
                {
                    transform.Rotate(0, -180, 0);
                }
                m_direction = -1;
                break;
            case Movement.Direction.Right:
                transform.rotation = new Quaternion(0, 180, 0, 0);
                m_direction = 1;
                break;
            case Movement.Direction.NONE:
                m_direction = 0;
                break;
        }
    }

    void Jump()
    {
        if (m_collisions.isGrounded())
        {
            m_jump = true;
        }
    }

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
