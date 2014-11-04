using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //basic physics properties, all in units/second
    float acceleration = 6.5f;
    float maxSpeed = 150f;
    float gravity = 8f;
    float maxfall = 220f;
    float jump = 220f;

    //a layer mask that I set in the Start() function
    int layerMask;

    //a Rectangle class has some useful tools for us
    Rect box;

    //my 2D velocity I use for most calculations
    Vector2 velocity;

    //checks
    public bool grounded = false;
    public bool falling = false;

    //variables for raycasting: how many rays, etc
    int horizontalRays = 7;
    int verticalRays = 5;
    int margin = 0;	//I don't check the very edge of the collider

    //variables for jumping
    float lastJumpDownTime = 0;
    bool jumpPressedLastFrame = false;
    float jumpPressLeeway = 0.1f;

    //angles and slopes
    float angleLeeway = 5f;

    //components
    Transform t;
    BoxCollider boxCol;

    // Platform support
    private Transform platform;
    private Vector3 platformPositionOld;
    private Vector3 deltaPlatformPos;
  
    void Start()
    {
        layerMask = Raylayers.s_onlyCollisions;

        boxCol = GetComponent<BoxCollider>();
        t = transform;
    }

    void FixedUpdate()
    {
        box = new Rect(
            boxCol.bounds.min.x,
            boxCol.bounds.min.y,
            boxCol.bounds.size.x,
            boxCol.bounds.size.y
        );

        //an elegant way to apply gravity. Subtract from y speed, with terminal velocity=maxfall
        if (!grounded)
            velocity = new Vector2(velocity.x, Mathf.Max(velocity.y - gravity, -maxfall));

        if (velocity.y < 0)
        {
            falling = true;
        }

        if (grounded || falling)
        {	//don't check anything if I'm moving up in the air

            Vector2 startPoint = new Vector2(box.xMin + margin, box.center.y);
            Vector2 endPoint = new Vector2(box.xMax - margin, box.center.y);
            RaycastHit[] hitInfos = new RaycastHit[verticalRays];

            //add half my box height since I'm starting in the centre
            float distance = box.height / 2 + (grounded ? margin : Mathf.Abs(velocity.y * Time.deltaTime));
            //this is a ternary operator that chooses how long
            //the ray will be based on whether I'm grounded
            float smallestFraction = Mathf.Infinity;
            int indexUsed = 0;

            //check if I hit anything. Starts false because if any ray connects I'm grounded
            bool connected = false;

            for (int i = 0; i < verticalRays; i++)
            {
                //verticalRays -1 because otherwise we don't get to 1.0
                float lerpAmount = (float)i / (float)(verticalRays - 1);
                Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

                Physics.Raycast(origin, -Vector2.up, out hitInfos[i], distance, Raylayers.s_downRay);
                Debug.DrawRay(origin, -Vector2.up);

                //Debug.Log(hitInfos[i].distance);
                if (hitInfos[i].distance > 0)
                {
                    connected = true;
                    if (hitInfos[i].distance < smallestFraction)
                    {
                        indexUsed = i;
                        smallestFraction = hitInfos[i].distance;
                    }
                }
            }

            if (connected)
            {
                grounded = true;
                falling = false;
                Debug.Log(hitInfos[indexUsed].distance);
                transform.Translate(Vector3.down * (hitInfos[indexUsed].distance - box.height / 2));
                velocity = new Vector2(velocity.x, 0);
            }
            else
            {
                grounded = false;
            }
        }

        //--------------------------------------------------------------------------\\
        //-----------------------------Lateral movement-----------------------------\\
        //--------------------------------------------------------------------------\\

        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        float newVelocityX = velocity.x;
        if (horizontalAxis != 0)
        {		//apply movement according to input
            newVelocityX += acceleration * horizontalAxis;
            newVelocityX = Mathf.Clamp(newVelocityX, -maxSpeed, maxSpeed);
        }
        else if (velocity.x != 0)
        {		//apply deceleration due to no input
            int modifier = velocity.x > 0 ? -1 : 1;
            newVelocityX += acceleration * modifier;
        }

        velocity = new Vector2(newVelocityX, velocity.y);

        if (velocity.x != 0)
        {			//do physics checks if I'm going to move
            Vector2 startPoint = new Vector2(box.center.x, box.yMin/* + margin*/);
            Vector2 endPoint = new Vector2(box.center.x, box.yMax/* - margin*/);

            RaycastHit[] hitInfos = new RaycastHit[horizontalRays];
            int amountConnected = 0;
            float lastFraction = 0;

            float sideRayLength = box.width / 2 + Mathf.Abs(newVelocityX * Time.deltaTime);
            Vector2 direction = newVelocityX > 0 ? Vector2.right : -Vector2.right;
            bool connected = false;

            for (int i = 0; i < horizontalRays; i++)
            {		//go through all the rays!
                float lerpAmount = (float)i / (float)(horizontalRays - 1);
                Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);

                //did I connect with the thing?
                Physics.Raycast(origin, direction, out hitInfos[i], sideRayLength, Raylayers.s_onlyCollisions);
                Debug.DrawRay(origin, direction);

                //there's a wall there don't go through it...
                if (hitInfos[i].distance > 0)
                {
                    connected = true;

                    if (lastFraction > 0)
                    {
                        float angle = Vector2.Angle(hitInfos[i].point - hitInfos[i - 1].point, Vector2.right);

                        if (Mathf.Abs(angle - 90) < angleLeeway)
                        {
                            transform.Translate(direction * (hitInfos[i].distance - box.width / 2));
                            velocity = new Vector2(0, velocity.y);
                            break;
                        }
                    }
                    amountConnected++;
                }
                lastFraction = hitInfos[i].distance;
            }

            if (connected)
            {

            }

        }
    }

    //private void VerticalCollisions(float moveY)
    //{
    //    // Check collisions above and below
    //    Debug.Log(moveY);

    //    if (character.getGroundedState() != Character.GroundedState.NONE)
    //    {
    //        for (int i = 0; i < collisionDivisionsX; i++)
    //        {
    //            float dir = Mathf.Sign(moveY);
    //            float x = (pos.x + center.x - size.x / 2) + size.x / (collisionDivisionsX - 1) * i; // Left, centre and then rightmost point of collider
    //            float y = pos.y + center.y + size.y / 2 * dir; // Bottom of collider

    //            origin = new Vector2(x, y - offset * dir);
    //            direction = new Vector2(0, dir);

    //            ray = new Ray(origin, direction);
    //            Debug.DrawRay(origin, direction, Color.red);

    //            if (Physics.Raycast(ray, out hit, Mathf.Abs(moveY) + offset + skin, collisionMask))
    //            {
    //                // Store min and max values
    //                if (dir < 0)  //&& hit.point.y < yMinValue
    //                {
    //                    float angle = Vector3.Angle(hit.normal, Vector3.down);
    //                    if (angle < 80 || angle > 100)
    //                    {
    //                        //connectedYDown = true;
    //                        //yMinValue = hit.point.y;
    //                    }

    //                }
    //                //else if (hit.point.y > yMaxValue)
    //                //{
    //                //    connectedYUp = true;
    //                //    yMaxValue = hit.point.y - size.y;
    //                //}

    //                // Moving platform support
    //                platform = hit.transform;
    //                platformPositionOld = platform.position;
    //                break;
    //            }
    //            else
    //            {
    //                platform = null;
    //            }
    //        }
    //    }
    //}

    //private void SidewaysCollisions(float moveX)
    //{
    //    Vector2 origin, direction;

    //    // Check collisions left and right
    //    if (moveX != 0)
    //    {
    //        for (int i = 0; i < collisionDivisionsY; i++)
    //        {
    //            float dir = Mathf.Sign(moveX);
    //            float x = pos.x + center.x + size.x / 2 * dir;
    //            float y = pos.y + center.y - size.y / 2 + size.y / (collisionDivisionsY - 1) * i;

    //            origin = new Vector2(x - offset * dir, y);
    //            direction = new Vector2(dir, 0);

    //            ray = new Ray(origin, direction);
    //            Debug.DrawRay(origin, direction, Color.red);

    //            if (Physics.Raycast(ray, out hit, Mathf.Abs(moveX) + offset + skin, collisionMask))
    //            {
    //                float angle = Vector3.Angle(hit.normal, Vector3.down);

    //                // Store min and max values
    //                if (angle > 60 && angle < 120)
    //                {
    //                    //if (dir < 0 && hit.point.x < xMinValue)
    //                    //{
    //                    //    connectedXLeft = true;
    //                    //    xMinValue = hit.point.x - size.x / 2 * dir;
    //                    //}
    //                    //else if (hit.point.x >= xMaxValue)
    //                    //{
    //                    //    connectedXRight = true;
    //                    //    xMaxValue = hit.point.x - size.x / 2 * dir;
    //                    //}
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}

    //public void Position(Vector2 moveAmount)
    //{
    //    pos = transform.position;

    //    if (platform)
    //        deltaPlatformPos = platform.position - platformPositionOld;
    //    else
    //        deltaPlatformPos = Vector3.zero;

    //    VerticalCollisions(moveAmount.y);
    //    SidewaysCollisions(moveAmount.x);

    //    Vector2 finalTransform = new Vector2(moveAmount.x + deltaPlatformPos.x, moveAmount.y);
    //    transform.Translate(finalTransform, Space.World);


    //    //if (connectedYDown && transform.position.y + moveAmount.y < yMinValue)
    //    //{
    //    //    transform.position = new Vector3(transform.position.x, yMinValue, 0);
    //    //    if (c.getInAirState() != Character.InAirState.JUMPING)
    //    //    {
    //    //        c.ChangeState(Character.InAirState.NONE);
    //    //    }
    //    //}
    //    //else if (connectedYUp && transform.position.y + moveAmount.y > yMaxValue)
    //    //{
    //    //    transform.position = new Vector3(transform.position.x, yMaxValue, 0);
    //    //    if (c.getInAirState() == Character.InAirState.JUMPING)
    //    //    {
    //    //        c.ChangeState(Character.InAirState.FALLING);
    //    //    }
    //    //}

    //    //if (onSlope && c.getInAirState() == Character.InAirState.NONE)
    //    //{
    //    //    transform.position = new Vector3(transform.position.x, groundValue, 0);
    //    //    c.ChangeState(Character.InAirState.NONE);
    //    //}

    //    //if (connectedXLeft && transform.position.x < xMinValue)
    //    //{
    //    //    transform.position = new Vector3(xMinValue, transform.position.y, 0);
    //    //    c.ChangeState(Character.PaceState.NONE);
    //    //}
    //    //else if (connectedXRight && transform.position.x > xMaxValue)
    //    //{
    //    //    transform.position = new Vector3(xMaxValue, transform.position.y, 0);
    //    //    c.ChangeState(Character.PaceState.NONE);
    //    //}
    //}
}