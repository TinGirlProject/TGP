﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    
    private float _walkSpeed = 4;
    private float _runMultiplier = 2;
    private float _gravity = 9.8f;
    private float _airTime = 0;
    private float _fallTime = 0.5f;
    private float _jumpHeight = 4;
    private float _jumpTime = 1.5f;

    private Transform _myTransform;
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private CollisionFlags _collisionFlags;
    private Animator _animator;
    private Direction _direction;

    private bool _run = false;
    private bool _jump = false;

    void Awake()
    {
        _myTransform = transform;
        _controller = GetComponent<CharacterController>();
        //_animator = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () 
    {
        _moveDirection = Vector3.zero;
        _run = false;
        _jump = false;
        _direction = Direction.Left;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_controller.isGrounded)
        {

        }
        else
        {
            if ((_collisionFlags & CollisionFlags.CollidedBelow) == 0)
            {
                _airTime += Time.deltaTime;

                if (_airTime > _fallTime)
                {

                }
            }
        }

        // Apply gravity
        _moveDirection.y -= _gravity * Time.deltaTime;

        // Move controller
        _collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);
	}

    public void Run()
    {
        _run = !_run;
    }

    public void Jump()
    {
        if (_controller.isGrounded)
            _jump = true;
    }

    public void Move(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                if (_direction == Direction.Right)
                {
                    transform.Rotate(0, -180, 0);
                    _direction = Direction.Left;
                }
                break;
            case Direction.Right:
                if (_direction == Direction.Left)
                {
                    transform.Rotate(0, 180, 0);
                    _direction = Direction.Right;
                }
                break;
            default:
                break;
        }
    }
}
