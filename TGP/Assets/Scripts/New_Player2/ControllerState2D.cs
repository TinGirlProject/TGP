using UnityEngine;
using System.Collections;

public class ControllerState2D
{
    public bool _isCollidingRight { get; set; }
    public bool _isCollidingLeft { get; set; }
    public bool _isCollidingAbove { get; set; }
    public bool _isCollidingBelow { get; set; }

    public bool _isMovingDownSlope { get; set; }
    public bool _isMovingUpSlope { get; set; }

    public bool _isGrounded { get { return _isCollidingBelow; } }

    public float _slopeAngle { get; set; }

    public bool _hasCollisions
    { 
        get { return _isCollidingRight || _isCollidingLeft || _isCollidingAbove || _isCollidingBelow; }  
    }

    public void Reset()
    {
        _isMovingUpSlope =
            _isMovingDownSlope =
            _isCollidingLeft =
            _isCollidingRight =
            _isCollidingAbove =
            _isCollidingBelow = false;

        _slopeAngle = 0;
    }

    public override string ToString()
    {
        return string.Format("{controller: r:{0} l:{1} a:{2} b:{3} down-slope:{4} up-slope:{5} angle:{6}",
            _isCollidingRight,
            _isCollidingLeft,
            _isCollidingAbove,
            _isCollidingBelow,
            _isMovingDownSlope,
            _isMovingUpSlope,
            _slopeAngle);
    }
}