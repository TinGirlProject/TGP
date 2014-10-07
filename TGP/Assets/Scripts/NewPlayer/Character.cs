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

	void Start () 
    {
        m_collisions = GetComponent<CharacterCollisions>();
        m_movement = GetComponent<Movement>();
        m_transform = transform;

        m_speed = m_movement._walkSpeed;
        m_moveAmount = new Vector2();
    }
        
	void Update () 
    {
        m_moveAmount.x = m_speed * m_direction;

        m_moveAmount.y -= m_movement._gravity * Time.deltaTime;

        m_moveAmount = m_collisions.TestMove(m_moveAmount);

        DoMove(m_moveAmount);
	}

    void DoMove(Vector2 amount)
    {
        Log.GREEN(amount.x);
        m_transform.position += new Vector3(amount.x, amount.y, 0);
    }

    void Move(Movement.Direction dir)
    {
        Log.BLUE(dir);
        switch (dir)
        {
            case Movement.Direction.Left:
                transform.eulerAngles = Vector3.zero;
                m_direction = -1;
                break;
            case Movement.Direction.Right:
                transform.eulerAngles = Vector3.up * 180;
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
            m_moveAmount.y += m_movement._jumpHeight;
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
