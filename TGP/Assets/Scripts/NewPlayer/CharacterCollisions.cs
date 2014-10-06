using UnityEngine;
using System.Collections;

public class CharacterCollisions : MonoBehaviour 
{
    //a layer mask that I set in the Start() function
    private int layerMask;

    // a Rectangle class has some useful tools for us
    private Rect box;

    // variables for raycasting: how many rays, etc
    public int _horizontalRays = 7;
    public int _verticalRays = 5;
    public int _rayLength = 5;
    public float _margin = 0.2f;	// I don't check the very edge of the collider

    // angles and slopes
    float angleLeeway = 5f;

    // components
    Transform m_trans;
    BoxCollider m_boxCol;

    Vector2 m_pos;
    Vector2 m_center;
    Vector2 m_size;

    // Use this for initialization
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

    void VerticalCollisions()
    {
        Vector2 origin;
        Vector2 direction;
        Ray ray;
        RaycastHit hit;

        // check top and bottom of player
        for (int dir = -1; dir < 2; dir+=2)
        {
            RaycastHit[] raysHit = new RaycastHit[_verticalRays];
            for (int i = 0; i < _verticalRays; i++)
            {
                // Left, centre and then rightmost point of collider
                float x = (m_trans.position.x + m_boxCol.center.x - m_boxCol.size.x / 2) + m_size.x / (_verticalRays - 1) * i;
                // Bottom or top of collider (depending on dir)
                float y = m_pos.y + m_center.y + m_size.y / 2 * dir;

                Log.BLUE(y);

                origin = new Vector2(x, y - _margin * dir);
                direction = new Vector2(0, dir);

                ray = new Ray(origin, direction);
                Debug.DrawRay(origin, direction, Color.red);

                if (Physics.Raycast(ray, out hit, _rayLength, layerMask))
                {
                    raysHit[i] = hit;
                }
            }

            switch (dir)
            {
                case -1:
                    SendMessage("HitTop", raysHit, SendMessageOptions.DontRequireReceiver);
                    break;
                case 1:
                    SendMessage("HitBottom", raysHit, SendMessageOptions.DontRequireReceiver);
                    break;
            }
        }
    }

    void HorizontalCollisions()
    {
        Vector2 origin;
        Vector2 direction;
        Ray ray;
        RaycastHit hit;

        // check top and bottom of player
        for (int dir = -1; dir < 2; dir += 2)
        {
            RaycastHit[] raysHit = new RaycastHit[_horizontalRays];
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

                if (Physics.Raycast(ray, out hit, _rayLength, layerMask))
                {
                    raysHit[i] = hit;
                }
            }

            switch (dir)
            {
                case -1:
                    SendMessage("HitLeft", raysHit, SendMessageOptions.DontRequireReceiver);
                    break;
                case 1:
                    SendMessage("HitRight", raysHit, SendMessageOptions.DontRequireReceiver);
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the characters position
        m_pos = m_trans.position;

        HorizontalCollisions();
        VerticalCollisions();
    }
}
