using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
    public enum Direction
    {
        Left = -1,
        NONE = 0,
        Right = 1
    }

    public float _gravity = 20;
    public float _walkSpeed = 8;
    public float _runSpeed = 12;
    public float _jumpHeight = 8;
}
