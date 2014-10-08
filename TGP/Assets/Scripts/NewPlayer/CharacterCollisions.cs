using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class CharacterCollisions : MonoBehaviour 
{
    private bool m_grounded;
    private bool m_sideBlocked;

    //a layer mask that I set in the Start() function
    private int layerMask;

    // a Rectangle class has some useful tools for us
    private Rect box;

    // variables for raycasting: how many rays, etc
    public int _horizontalRays = 7;
    public int _verticalRays = 5;
    private float _margin = 0.4f;	// I don't check the very edge of the collider

    // angles and slopes
    float angleLeeway = 5f;

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
    private float VerticalCollisions(float moveY)
    {
        Vector2 origin;
        Vector2 direction;
        Ray ray;
        RaycastHit hit;

        int dir = (int)Mathf.Sign(moveY);
        
        // the amount to move after collisions
        float newMoveY = moveY;
        bool connected = false;

        // check top or bottom of player
        for (int i = 0; i < _verticalRays; i++)
        {
            // Left, centre and then rightmost point of collider
            float x = (m_trans.position.x + m_boxCol.center.x - m_boxCol.size.x / 2) + m_size.x / (_verticalRays - 1) * i;
            // Bottom or top of collider (depending on dir)
            float y = m_pos.y + m_center.y + m_size.y / 2 * dir;

            origin = new Vector2(x, y - _margin * dir);
            direction = new Vector2(0, dir);

            ray = new Ray(origin, direction);

            if (Physics.Raycast(ray, out hit, Mathf.Abs(moveY) + _margin, layerMask))
            {
                // get the smallest between the ray hit point and the character
                float hitDistance = Vector2.Distance(new Vector2(hit.point.x, y), hit.point);

                //Log.BOLD(hitDistance);
                if (hitDistance < Mathf.Abs(newMoveY))
                {
                    newMoveY = hitDistance * dir;
                    connected = true;
                }

                Debug.DrawRay(origin, direction, Color.red, Mathf.Abs(moveY));
            }
            else
            {
                Debug.DrawRay(origin, direction, Color.white, Mathf.Abs(moveY));
            }
        }

        // check and set if grounded before setting the y movement
        if (!connected)
        {
            m_grounded = false;
        }

        // dont move when the character is grounded
        if (m_grounded)
        {
            newMoveY = 0;
        }

        // if the character just connected with the ground, position it precisely on the ground
        // and set it to be grounded.
        if (connected && dir == -1)
        {
            m_grounded = true;
        }

        return newMoveY;
    }
    
    /// <summary>
    /// Check for horizontal collisions on the side of the character.  Check the side in the direction
    /// of moveX.  
    /// </summary>
    /// <param name="moveX">Amount to move along the x axis</param>
    /// <returns>A float with corrected move value on x axis.</returns>
    private float HorizontalCollisions(float moveX)
    {
        if (moveX == 0)
            return 0;

        // origin and direction of the current ray
        Vector2 origin;
        Vector2 direction;

        // ray and raycast hit being used
        Ray ray;
        RaycastHit[] hits = new RaycastHit[_horizontalRays];
        
        // current direction on the x axis
        int dir = (int)Mathf.Sign(moveX);

        float length = Mathf.Abs(moveX) + _margin + m_boxCol.size.x / 2;

        // the amount to move after collisions
        float newMoveX = moveX;

        // if a ray connects, this is set to true
        bool connected = false;
        // used for slope calculations
        bool connectedBefore = false;

        // check left or right of player
        for (int i = 0; i < _horizontalRays; i++)
        {
            // Left or right of collider (depending on dir)
            float x = m_pos.x + m_center.x + m_size.x / 2 * dir;

            // Top, middle and then bottommost point of collider
            float y = (m_trans.position.y + m_boxCol.center.y - m_boxCol.size.y / 2) + m_size.y / (_horizontalRays - 1) * i;

            origin = new Vector2(x - _margin * dir, y);
            direction = new Vector2(dir, 0);

            ray = new Ray(origin, direction);
            Debug.DrawRay(origin, direction, Color.red);

            if (Physics.Raycast(ray, out hits[i], length, layerMask))
            {
                //// get the smallest between the ray hit point and the character
                //float hitDistance = Vector2.Distance(hit.point, m_pos);
                //if (hitDistance < Mathf.Abs(newMoveX))
                //{
                //    newMoveX = dir * hitDistance;

                //    switch (dir)
                //    {
                //        case -1:
                //            m_leftBlocked = true;
                //            break;
                //        case 1:
                //            m_rightBlocked = true;
                //            break;
                //    }
                //}

                // the character has connected with something!
                

                float angle = Vector2.Angle(hits[i].normal, Vector2.up);
                if (Mathf.Abs(angle -90) < angleLeeway)
                {
                    connected = true;
                }
                else
                {
                    
                }
                Log.BLUE(Mathf.Abs(angle - 90));

                // check if the previous ray collided as well as the current one
                if (connectedBefore)
                {
                    // check the normal of the thing we are colliding with to the up vector
                    float angle2 = Vector2.Angle(hits[i].point - hits[i - 1].point, Vector2.right);

                    Log.RED(angle2);
                }
                connectedBefore = true;

                Debug.DrawRay(origin, direction, Color.red, Mathf.Abs(moveX));
            }
            else
            {
                Debug.DrawRay(origin, direction, Color.white, Mathf.Abs(moveX));
            }
        }

        // set if blocked on either side
        m_sideBlocked = connected;

        if (m_sideBlocked)
        {
            newMoveX = 0;
        }

        return newMoveX;
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

        amount.x = HorizontalCollisions(amount.x);
        amount.y = VerticalCollisions(amount.y);

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
