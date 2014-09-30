using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Character))]
public class CharacterCollision : MonoBehaviour 
{
    private Character c;

    #region Collisions
    // Layer for wall/ground/ceiling collisions
    public LayerMask collisionMask;

    private BoxCollider collider;
    private Vector3 size;
    private Vector3 center;

    private Vector3 originalSize;
    private Vector3 originalCentre;
    private float colliderScale;

    // Amount of raycasts
    private int collisionDivisionsX = 5;
    private int collisionDivisionsY = 10;

    private float groundValue = -Mathf.Infinity;

    // Offset
    private float offset = .5f;

    // Distance to raycast infront
    private float skin = .005f;

	[SerializeField]
    private float xMinValue;
	[SerializeField]
    private float xMaxValue;
	[SerializeField]
    public float yMinValue;
	[SerializeField]
    private float yMaxValue;
    #endregion

	[SerializeField]
    private bool onSlope = false;
	[SerializeField]
    bool connectedXRight = false;
	[SerializeField]
    bool connectedXLeft = false;
	[SerializeField]
    bool connectedYUp = false;
	[SerializeField]
    bool connectedYDown = false;

    // Platform support
    private Transform platform;
    private Vector3 platformPositionOld;
    private Vector3 deltaPlatformPos;

    Vector2 pos;
    Ray ray;
    RaycastHit hit;

	void Start () 
    {
        c = GetComponent<Character>();

        collider = GetComponent<BoxCollider>();
        colliderScale = transform.localScale.x;

        // Set original size of collider and store variables
        originalSize = collider.size;
        originalCentre = collider.center;
        SetCollider(originalSize, originalCentre);
	}

    private void VerticalCollisions(float moveY)
    {
        // Check collisions above and below
        //c.ChangeState(Character.GroundedState.NONE);

        Vector2 origin = new Vector2(transform.position.x, transform.position.y + size.y / 2);
        Vector2 direction = new Vector2(0, -1);

        ray = new Ray(origin, direction);
        Debug.DrawRay(origin, direction * (size.y / 2 + skin), Color.green);

        if (Physics.Raycast(ray, out hit, size.y + skin, collisionMask))
        {
            groundValue = hit.point.y;

            float angle = Vector3.Angle(hit.normal, Vector3.down);
            if (Mathf.Approximately(angle, 0) || Mathf.Approximately(angle, 180))
            {
                onSlope = false;
            }
            else
            {
                onSlope = true;
            }
        }
        else
        {
            onSlope = false;
        }
        Debug.Log(moveY);
        //if (moveY != 0)
        //{
            for (int i = 0; i < collisionDivisionsX; i++)
            {
                float dir = Mathf.Sign(moveY);
                float x = (pos.x + center.x - size.x / 2) + size.x / (collisionDivisionsX - 1) * i; // Left, centre and then rightmost point of collider
                float y = pos.y + center.y + size.y / 2 * dir; // Bottom of collider

                origin = new Vector2(x, y - offset * dir);
                direction = new Vector2(0, dir);

                ray = new Ray(origin, direction);
                Debug.DrawRay(origin, direction, Color.red);

                if (Physics.Raycast(ray, out hit, Mathf.Abs(moveY) + offset + skin, collisionMask))
                {
                    // Store min and max values
                    if (dir < 0 && hit.point.y < yMinValue)
                    {
                        float angle = Vector3.Angle(hit.normal, Vector3.down);
                        if (angle < 80 || angle > 100)
                        {
                            connectedYDown = true;
                            yMinValue = hit.point.y;
                        }
                    }
                    else if (dir > 0 && hit.point.y > yMaxValue)
                    {
                        Debug.Log(dir);
                        connectedYUp = true;
                        yMaxValue = hit.point.y - size.y;
                    }

                    // Moving platform support
                    platform = hit.transform;
                    platformPositionOld = platform.position;
                    break;
                }
                else
                {
                    platform = null;
                }
            }
       // }
    }

    private void SidewaysCollisions(float moveX)
    {
        Vector2 origin, direction;

        // Check collisions left and right
        if (moveX != 0)
        {
            for (int i = 0; i < collisionDivisionsY; i++)
            {
                float dir = Mathf.Sign(moveX);
                float x = pos.x + center.x + size.x / 2 * dir;
                float y = pos.y + center.y - size.y / 2 + size.y / (collisionDivisionsY - 1) * i;

                origin = new Vector2(x - offset * dir, y);
                direction = new Vector2(dir, 0);

                ray = new Ray(origin, direction);
                Debug.DrawRay(origin, direction, Color.red);

                if (Physics.Raycast(ray, out hit, Mathf.Abs(moveX) + offset + skin, collisionMask))
                {
                    float angle = Vector3.Angle(hit.normal, Vector3.down);

                    // Store min and max values
                    if (angle > 60 && angle < 120)
                    {
                        if (dir < 0 && hit.point.x < xMinValue)
                        {
                            connectedXLeft = true;
                            xMinValue = hit.point.x - size.x / 2 * dir;
                        }
                        else if (hit.point.x >= xMaxValue)
                        {
                            connectedXRight = true;
                            xMaxValue = hit.point.x - size.x / 2 * dir;
                        }
                    }
                    break;
                }
            }
        }
    }

    public void Position(Vector2 moveAmount)
    {
        // Store whether character hit wall
        //connectedXRight = false;
        //connectedXLeft = false;
        //connectedYUp = false;
        //connectedYDown = false;

        pos = transform.position;

        // Used to determine the min or max value
        yMinValue = Mathf.Infinity;
        yMaxValue = -Mathf.Infinity;
        xMinValue = Mathf.Infinity;
        xMaxValue = -Mathf.Infinity;

        if (platform)
            deltaPlatformPos = platform.position - platformPositionOld;
        else
            deltaPlatformPos = Vector3.zero;

        VerticalCollisions(moveAmount.y);
        SidewaysCollisions(moveAmount.x);

        if (!connectedYDown)
        {
            if (c.getInAirState() != Character.InAirState.JUMPING)
                c.ChangeState(Character.InAirState.FALLING);
        }

        if (connectedYDown && transform.position.y + moveAmount.y < yMinValue)
        {
            transform.position = new Vector3(transform.position.x, yMinValue, 0);
            if (c.getInAirState() != Character.InAirState.JUMPING)
            {
                c.ChangeState(Character.InAirState.NONE);
            }
        }
        else if (connectedYUp && transform.position.y + moveAmount.y > yMaxValue)
        {
            transform.position = new Vector3(transform.position.x, yMaxValue, 0);
            if (c.getInAirState() == Character.InAirState.JUMPING)
            {
                c.ChangeState(Character.InAirState.FALLING);
            }
        }

        if (c.getInAirState() == Character.InAirState.NONE)
        {
            transform.position = new Vector3(transform.position.x, groundValue, 0);
            c.ChangeState(Character.InAirState.NONE);
        }

        if (connectedXLeft && transform.position.x < xMinValue)
        {
            transform.position = new Vector3(xMinValue, transform.position.y, 0);
            c.ChangeState(Character.PaceState.NONE);
        }
        else if (connectedXRight && transform.position.x > xMaxValue)
        {
            transform.position = new Vector3(xMaxValue, transform.position.y, 0);
            c.ChangeState(Character.PaceState.NONE);
        }

        Vector2 finalTransform = new Vector2(moveAmount.x + deltaPlatformPos.x, moveAmount.y);
        transform.Translate(finalTransform, Space.World);
    }

    // Set collider
    public void SetCollider(Vector3 newSize, Vector3 newCenter)
    {
        //collider.size = newSize;
        //collider.center = newCenter;

        size = newSize * colliderScale;
        center = newCenter * colliderScale;
    }

    public void ResetCollider()
    {
        SetCollider(originalSize, originalCentre);
    }
}
