using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class CharacterCollisions : MonoBehaviour 
{
    private bool m_grounded;
    private bool m_sideBlocked;
    private bool m_onSlope;

    //a layer mask that I set in the Start() function
    private int layerMask;

    // a Rectangle class has some useful tools for us
    private Rect box;

    // variables for raycasting: how many rays, etc
    public int _horizontalRays = 7;
    public int _verticalRays = 5;
    private float _margin = 0.4f;	// I don't check the very edge of the collider

    // angles and slopes
    float angleLeeway = 70f;

    // components
    Transform m_trans;
    BoxCollider m_boxCol;

    Vector2 m_pos;
    Vector2 m_center;
    Vector2 m_size;

    void Start()
    {
        layerMask = Raylayers.s_onlyCollisions;
        
        // store collider
        m_boxCol = GetComponent<BoxCollider>();

        // save transform, saves from fetching everytime
        m_trans = transform;
        // save size and center of the character
        m_center = m_boxCol.center;
        m_size = m_boxCol.size;
    }

    /// <summary>
    /// Check for vertical collisions on the top and bottom of the character.  
    /// Check the side in the direction of moveY.  
    /// </summary>
    /// <param name="moveX">Amount to move along the y axis</param>
    /// <returns>A float with corrected move value on y axis.</returns>
    private Vector2 VerticalCollisions(Vector2 amount)
    {
        Vector2 origin;
        Vector2 direction;
        Ray ray;
        RaycastHit hit;

        int dir = (int)Mathf.Sign(amount.y);
        float length = Mathf.Abs(amount.y) + _margin + m_boxCol.size.x / 2;
        
        // the amount to move after collisions
        Vector2 newAmount = amount;
        float inGround = 0;

        bool connected = false;

        // check top or bottom of player
        for (int i = 0; i < _verticalRays; i++)
        {
            // Left, centre and then rightmost point of collider
            float x = (m_trans.position.x + m_boxCol.center.x - m_boxCol.size.x / 2) + m_size.x / (_verticalRays - 1) * i;
            // Bottom or top of collider (depending on dir)
            float y = m_pos.y + m_center.y + m_size.y / 2 * dir;

            // origin and direction of the current ray
            origin = new Vector2(x, y - _margin * dir);
            direction = new Vector2(0, dir);

            ray = new Ray(origin, direction);

            if (Physics.Raycast(ray, out hit, Mathf.Abs(amount.y) + _margin, layerMask))
            {
                // get the smallest between the ray hit point and the character
                float hitDistance = Vector2.Distance(new Vector2(hit.point.x, y), hit.point);

                // move the player the exact amount onto the floor
                if (hitDistance < Mathf.Abs(newAmount.y))
                {
                    newAmount.y = hitDistance * dir;
                    connected = true;
                }

                if (m_grounded)
                {
                    // get the amount to move on the y axis if walking over a small bump
                    float bottom = m_trans.position.y;
                    float hitPoint = hit.point.y;
                    if (hitPoint - bottom > inGround && dir == -1)
                    {
                        // move the player up!!
                        inGround = (hitPoint - bottom);
                    }

                    // calculate the angle of the slope
                    float angle = Vector2.Angle(hit.normal, Vector2.up);
                    Log.BLUE(angle);
                    if (Mathf.Abs(angle) != 0)
                    {
                        // check if the character is on a slope
                        m_onSlope = true;
                    }
                    else
                    {
                        m_onSlope = false;
                    }
                }
                Debug.DrawRay(origin, direction, Color.red, Mathf.Abs(amount.y));
            }
            else
            {
                Debug.DrawRay(origin, direction, Color.white, Mathf.Abs(amount.y));
            }
        }
       
        // position the character up a step
        if (inGround > 0 && m_grounded)
        {
            transform.Translate(0, inGround, 0);
        }

        if (m_onSlope)
        {
            m_trans.position = new Vector3(m_trans.position.x, hit.point.y, 0);
        }

        // check and set if grounded before setting the y movement
        if (!connected)
        {
            m_grounded = false;
            m_onSlope = false;
        }

        // dont move when the character is grounded
        if (m_grounded && newAmount.y < 0)
        {
            newAmount.y = 0;
        }

        // if the character just connected with the ground, position it precisely on the ground
        // and set it to be grounded.
        if (connected && dir == -1)
        {
            m_grounded = true;
        }

        return newAmount;
    }
    
    /// <summary>
    /// Check for horizontal collisions on the side of the character.  Check the side in the direction
    /// of moveX.  
    /// </summary>
    /// <param name="moveX">Amount to move along the x axis</param>
    /// <returns>A float with corrected move value on x axis.</returns>
    private Vector2 HorizontalCollisions(Vector2 amount)
    {
        if (amount.x == 0)
            return amount;

        // origin and direction of the current ray
        Vector2 origin;
        Vector2 direction;

        // ray and raycast hit being used
        Ray ray;
        RaycastHit hit;
        
        // current direction on the x axis
        int dir = (int)Mathf.Sign(amount.x);
        float length = Mathf.Abs(amount.x) + _margin + m_boxCol.size.x / 2;

        // the amount to move after collisions
        Vector2 newAmount = amount;

        bool connected = false;

        // check left or right of player
        for (int i = 0; i < _horizontalRays; i++)
        {
            // Left or right of collider (depending on dir)
            float x = m_pos.x + m_center.x + m_size.x / 2 * dir;

            // Top, middle and then bottommost point of collider
            float y = (m_trans.position.y + m_boxCol.center.y + m_boxCol.size.y / 2) - m_size.y / (_horizontalRays - 1) * i;

            origin = new Vector2(x - _margin * dir, y);
            direction = new Vector2(dir, 0);

            ray = new Ray(origin, direction);
            Debug.DrawRay(origin, direction, Color.red);

            if (Physics.Raycast(ray, out hit, length, layerMask))
            {
                float angle = Vector2.Angle(hit.normal, Vector2.up);
                if (Mathf.Abs(angle) > angleLeeway)
                {
                    // if the character is grounded allow him to walk over things he hits with the bottom ray
                    // otherwise stop him
                    if (m_grounded)
                    {
                        if (i != _horizontalRays - 1 && !connected)
                            connected = true;
                    }
                    else
                    {
                        connected = true;
                    }
                }
            }
            else
            {
                Debug.DrawRay(origin, direction, Color.white, Mathf.Abs(amount.x));
            }
        }

        // only prevent movement when the character connects with more than one ray
        if (!connected)
        {
            m_sideBlocked = false;
        }

        // stop movement if sideblocked 
        if (m_sideBlocked)
        {
            newAmount.x = 0;
        }

        // check and set if grounded before setting the y movement
        if (connected)
        {
            m_sideBlocked = true;
        }

        return newAmount;
    }

    /// <summary>
    /// Modify the amount the character can move because of collisions.
    /// </summary>
    /// <param name="amount">Vector2 represting where the character wishes to move.</param>
    /// <returns>Vector2 representing the changed movement.</returns>
    public Vector2 TestMove(Vector2 amount)
    {
        // get the characters position
        m_pos = m_trans.position;

        amount = HorizontalCollisions(amount);
        amount = VerticalCollisions(amount);

        return amount;
    }

    /// <summary>
    /// Check whether the character is grounded or not.
    /// </summary>
    /// <returns>
    /// true if the character is grounded.
    /// false if not.
    /// </returns>
    public bool isGrounded()
    {
        return m_grounded;
    }
}
