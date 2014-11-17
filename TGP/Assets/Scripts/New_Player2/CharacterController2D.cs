using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    private const float SKIN_WIDTH = 0.01f;
    private const int HORIZONTAL_RAYS = 8;
    private const int VERTICAL_RAYS = 4;

    private static readonly float SLOPE_LIMIT_TANGENT = Mathf.Tan(75f * Mathf.Deg2Rad);

    public LayerMask _platformMask;
    public ControllerParameters2D _defaultParameters;

    public ControllerState2D _state { get; private set; }
    public Vector2 _velocity { get { return m_velocity; } }
    public bool _handleCollisions { get; set; }
    public ControllerParameters2D _parameters { get { return m_overrideParameters ?? _defaultParameters; } }
    public GameObject _standingOn { get; private set; }
    public bool _canJump 
    { 
        get 
        {
            if (_parameters._jumpBehaviour == ControllerParameters2D.JumpBehaviour.CanJumpAnywhere)
                return m_jumpIn <= 0;

            if (_parameters._jumpBehaviour == ControllerParameters2D.JumpBehaviour.CanJumpOnGround)
                return _state._isGrounded;

            return false;
        }
    }

    private Vector2 m_velocity;
    private Transform m_transform;
    private Vector3 m_localScale;
    private BoxCollider m_boxCollider;
    private ControllerParameters2D m_overrideParameters;
    private float m_jumpIn;

    private float m_verticalDistanceBetweenRays;
    private float m_horizontalDistanceBetweenRays;

    private Vector3 m_raycastTopLeft;
    private Vector3 m_raycastBottomRight;
    private Vector3 m_raycastBottomLeft;

    public void Awake()
    {
        _handleCollisions = true;

        _state = new ControllerState2D();
        m_transform = transform;
        m_localScale = transform.localScale;
        m_boxCollider = GetComponent<BoxCollider>();

        //float colliderWidth = m_boxCollider.size.x * Mathf.Abs(transform.localScale.x) - (2 * SKIN_WIDTH);
        float colliderWidth = m_boxCollider.size.x * Mathf.Abs(transform.localScale.x);
        m_horizontalDistanceBetweenRays = colliderWidth / (VERTICAL_RAYS - 1);

        float colliderHeight = m_boxCollider.size.y * Mathf.Abs(transform.localScale.y) - (2 * SKIN_WIDTH);
        m_verticalDistanceBetweenRays = colliderHeight / (HORIZONTAL_RAYS - 1);
    }

    public void LateUpdate()
    {
        m_jumpIn -= Time.deltaTime;
        m_velocity.y += _parameters._gravity * Time.deltaTime;
        Move(m_velocity * Time.deltaTime);
    }

    public void AddForce(Vector2 force)
    {
        m_velocity += force;
    }

    public void SetForce(Vector2 force)
    {
        m_velocity = force;
    }

    public void SetHorizontalForce(float x)
    {
        m_velocity.x = x;
    }

    public void SetVerticalForce(float y)
    {
        m_velocity.y = y;
    }

    public void Jump()
    {
        // TODO: Moving platform support
        AddForce(new Vector2(0, _parameters._jumpMagnitude));
        m_jumpIn = _parameters._jumpFrequency;
    }

    private void HandleMovingPlatforms()
    {

    }

    private void CalculateRayOrigins()
    {
        Vector2 size = new Vector2(m_boxCollider.size.x * Mathf.Abs(m_localScale.x), m_boxCollider.size.y * Mathf.Abs(m_localScale.y)) / 2;
        Vector2 center = new Vector2(m_boxCollider.center.x * m_localScale.x, m_boxCollider.center.y * m_localScale.y);

        //m_raycastTopLeft = m_transform.position + new Vector3(center.x - size.x + SKIN_WIDTH, center.y + size.y - SKIN_WIDTH);
        //m_raycastBottomRight = m_transform.position + new Vector3(center.x + size.x - SKIN_WIDTH, center.y - size.y + SKIN_WIDTH);
        //m_raycastBottomLeft = m_transform.position + new Vector3(center.x - size.x + SKIN_WIDTH, center.y - size.y + SKIN_WIDTH);

        m_raycastTopLeft = m_transform.position + new Vector3(center.x - size.x, center.y + size.y - SKIN_WIDTH);
        m_raycastBottomRight = m_transform.position + new Vector3(center.x + size.x, center.y - size.y + SKIN_WIDTH);
        m_raycastBottomLeft = m_transform.position + new Vector3(center.x - size.x, center.y - size.y - SKIN_WIDTH);
    }

    private void Move(Vector2 deltaMovement)
    {
        bool wasGrounded = _state._isCollidingBelow;
        _state.Reset();

        if (_handleCollisions)
        {
            HandleMovingPlatforms();
            CalculateRayOrigins();

            if (deltaMovement.y < 0 && wasGrounded)
                HandleVerticalSlope(ref deltaMovement);

            if (Mathf.Abs(deltaMovement.x) > 0.001f)
                MoveHorizontaly(ref deltaMovement);

            MoveVertically(ref deltaMovement);
        }

        m_transform.Translate(deltaMovement, Space.World);

        // TODO: Additional moving platform code

        if (Time.deltaTime > 0)
            m_velocity = deltaMovement / Time.deltaTime;

        m_velocity.x = Mathf.Min(m_velocity.x, _parameters._maxVelocity.x);
        m_velocity.y = Mathf.Min(m_velocity.y, _parameters._maxVelocity.y);

        if (_state._isMovingUpSlope)
            m_velocity.y = 0;
    }

    private void MoveHorizontaly(ref Vector2 deltaMovement)
    {
        bool isGoingRight = deltaMovement.x > 0;
        float rayDistance = Mathf.Abs(deltaMovement.x) + SKIN_WIDTH;
        Vector2 rayDirection = isGoingRight ? Vector2.right : -Vector2.right;
        Vector3 rayOrigin = isGoingRight ? m_raycastBottomRight : m_raycastBottomLeft;

        for (int i = 0; i < HORIZONTAL_RAYS; i++)
        {
            Vector2 rayVector = new Vector2(rayOrigin.x, rayOrigin.y + (i * m_verticalDistanceBetweenRays));
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            RaycastHit raycastHit;
            if (!Physics.Raycast(rayVector, rayDirection, out raycastHit, rayDistance, _platformMask))
                continue;

            if (i == 0 && HandleHorizontalSlope(ref deltaMovement, Vector2.Angle(raycastHit.normal, Vector2.up), isGoingRight))
                break;

            deltaMovement.x = raycastHit.point.x - rayVector.x;
            rayDistance = Mathf.Abs(deltaMovement.x);

            if (isGoingRight)
            {
                deltaMovement.x -= SKIN_WIDTH;
                _state._isCollidingRight = true;
            }
            else
            {
                deltaMovement.x += SKIN_WIDTH;
                _state._isCollidingLeft = true;
            }

            if (rayDistance < SKIN_WIDTH + 0.001f)
                break;
        }
    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {
        bool isGoingUp = deltaMovement.y > 0;
        float rayDistance = Mathf.Abs(deltaMovement.y) + SKIN_WIDTH;
        Vector2 rayDirection = isGoingUp ? Vector2.up : -Vector2.up;
        Vector3 rayOrigin = isGoingUp ? m_raycastTopLeft : m_raycastBottomLeft;

        rayOrigin.x += deltaMovement.x;

        float standingOnDistance = float.MaxValue;

        for (int i = 0; i < VERTICAL_RAYS; i++)
        {
            Vector2 rayVector = new Vector2(rayOrigin.x + (i * m_horizontalDistanceBetweenRays), rayOrigin.y);
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            RaycastHit raycastHit;
            if (!Physics.Raycast(rayVector, rayDirection, out raycastHit, rayDistance, _platformMask))
                continue;

            if (!isGoingUp)
            {
                float verticalDistanceToHit = m_transform.position.y - raycastHit.point.y;
                if (verticalDistanceToHit < standingOnDistance)
                {
                    standingOnDistance = verticalDistanceToHit;
                    _standingOn = raycastHit.collider.gameObject;
                }
            }

            deltaMovement.y = raycastHit.point.y - rayVector.y;
            rayDistance = Mathf.Abs(deltaMovement.y);

            if (isGoingUp)
            {
                deltaMovement.y -= SKIN_WIDTH;
                _state._isCollidingAbove = true;
            }
            else
            {
                deltaMovement.y += SKIN_WIDTH;
                _state._isCollidingBelow = true;
            }

            if (!isGoingUp && deltaMovement.y > 0.001f)
                _state._isMovingUpSlope = true;

            if (rayDistance < SKIN_WIDTH + 0.001f)
                break;
        }
    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {
        float center = (m_raycastBottomLeft.x + m_raycastBottomRight.x) / 2;
        Vector2 direction = -Vector2.up;

        float slopeDistance = SLOPE_LIMIT_TANGENT * (m_raycastBottomRight.x - center);
        if (slopeDistance <= 0)
            return;

        Vector2 slopeRayVector = new Vector2(center, m_raycastBottomLeft.y);

        Debug.DrawRay(slopeRayVector, direction * slopeDistance, Color.yellow);

        RaycastHit raycastHit;
        if (!Physics.Raycast(slopeRayVector, direction, out raycastHit, slopeDistance, _platformMask))
            return;

        bool isMovingDownSlope = Mathf.Sign(raycastHit.normal.x) == Mathf.Sign(deltaMovement.x);
        if (!isMovingDownSlope)
            return;

        float angle = Vector2.Angle(raycastHit.normal, Vector2.up);
        if (Mathf.Abs(angle) < 0.001f)
            return;

        _state._isMovingDownSlope = true;
        _state._slopeAngle = angle;

        deltaMovement.y = raycastHit.point.y - slopeRayVector.y;
    }

    private bool HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {
        if (Mathf.RoundToInt(angle) == 90)
            return false;

        if (angle > _parameters._slopeLimit)
        {
            deltaMovement.x = 0;
            return true;
        }

        if (deltaMovement.y > 0.07f)
            return true;

        deltaMovement.x += isGoingRight ? -SKIN_WIDTH : SKIN_WIDTH;
        deltaMovement.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * deltaMovement.x);
        _state._isMovingUpSlope = true;
        _state._isCollidingBelow = true;

        return true;
    }
}