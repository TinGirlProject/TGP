using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class ControllerParameters2D
{
    public enum JumpBehaviour
    {
        CanJumpOnGround,
        CanJumpAnywhere,
        CantJump
    }

    public Vector2 _maxVelocity = new Vector2(float.MaxValue, float.MaxValue);

    [Range(0, 90)]
    public float _slopeLimit = 30.0f;
    public float _gravity = -25.0f;

    public JumpBehaviour _jumpBehaviour;
    public float _jumpFrequency;
    public float _jumpMagnitude = 12.0f;
}