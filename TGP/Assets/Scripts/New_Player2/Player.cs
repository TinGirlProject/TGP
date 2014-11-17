using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    private bool m_isFacingRight;
    private CharacterController2D m_controller;
    private float m_normalizedHorizontalSpeed;

    public float _maxSpeed = 8.0f;
    public float _speedAccelerationOnGround = 10.0f;
    public float _speedAccelerationInAir = 5.0f;

    public void Start()
    {
        m_controller = GetComponent<CharacterController2D>();
        m_isFacingRight = transform.localScale.x > 0;
    }

    public void Update()
    {
        HandleInput();

        float movementFactor = m_controller._state._isGrounded ? _speedAccelerationOnGround : _speedAccelerationInAir;

        m_controller.SetHorizontalForce(Mathf.Lerp(m_controller._velocity.x, m_normalizedHorizontalSpeed * _maxSpeed, Time.deltaTime * movementFactor));
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            m_normalizedHorizontalSpeed = 1;
            if (!m_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_normalizedHorizontalSpeed = -1;
            if (m_isFacingRight)
                Flip();
        }
        else
        {
            m_normalizedHorizontalSpeed = 0;
        }

        if (m_controller._canJump && Input.GetKeyDown(KeyCode.Space))
        {
            m_controller.Jump();
        }
    }

    private void Flip()
    {
        //if (m_isFacingRight)
        //    transform.localRotation = Quaternion.Euler(0, 90, 0);
        //else
        //    transform.localRotation = Quaternion.Euler(0, -90, 0);

        //m_isFacingRight = transform.localRotation.y < 0;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        m_isFacingRight = transform.localScale.x > 0;
    }
}