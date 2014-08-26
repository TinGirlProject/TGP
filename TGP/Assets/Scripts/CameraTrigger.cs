using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour 
{
    public CameraScrolling cam;

    private bool passedThrough;

    private float prev_distance;
    private float prev_springiness;
    private float prev_zoomSpeed;
    private float prev_heightOffset;
    private float prev_velocityLookAhead;
    private Vector2 prev_maxLookAhead;

    public float distance = 1.0f;
    public float springiness = 1.0f;
    public float zoomSpeed = 1.0f;
    public float heightOffset = 0.0f;
    public float velocityLookAhead = 0.0f;
    public Vector2 maxLookAhead = new Vector2(0.0f, 0.0f);

    private Vector3 enterPosition;
    private Vector3 exitPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // store where player entered
            enterPosition = other.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
         if (other.tag == "Player")
        {
            Debug.Log(other.transform.position.x - enterPosition.x);
            // store where player exited
            if (Mathf.Abs(other.transform.position.x - enterPosition.x) > collider.bounds.size.x / 2)
            {
                if (passedThrough)
                {
                    // set new values
                    cam.SetZoomSpeed(prev_zoomSpeed);
                    cam.SetSpringiness(prev_springiness);
                    cam.SetDistance(prev_distance);
                    cam.SetHeightOffset(prev_heightOffset);
                    cam.SetVelocityLookAhead(prev_velocityLookAhead);
                    cam.SetMaxLookAhead(prev_maxLookAhead);

                    passedThrough = false;
                }
                else
                {
                    // store pervious values
                    prev_distance = cam.GetDistance();
                    prev_springiness = cam.GetSpringiness();
                    prev_zoomSpeed = cam.GetZoomSpeed();
                    prev_heightOffset = cam.GetHeightOffSet();
                    prev_velocityLookAhead = cam.GetVelocityLookAhead();
                    prev_maxLookAhead = cam.GetMaxLookAhead();

                    // set new values
                    cam.SetZoomSpeed(zoomSpeed);
                    cam.SetSpringiness(springiness);
                    cam.SetDistance(distance);
                    cam.SetHeightOffset(heightOffset);
                    cam.SetVelocityLookAhead(velocityLookAhead);
                    cam.SetMaxLookAhead(maxLookAhead);

                    passedThrough = true;
                }
            }
        }
    }
}
