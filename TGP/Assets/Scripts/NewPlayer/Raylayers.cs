using UnityEngine;
using System.Collections;

public class Raylayers
{
    public static readonly int onlyCollisions;
    public static readonly int upRay;
    public static readonly int downRay;

    public static readonly string s_COLLLISIONS_NORMAL = "CollisionsNormal";
    public static readonly string s_COLLISIONS_SOFT_TOP = "CollisionsSoftTop";
    public static readonly string s_COLLISIONS_SOFT_BOTTOM = "CollisionsSoftBottom";

    static Raylayers()
    {
        onlyCollisions = 1 << LayerMask.NameToLayer(s_COLLLISIONS_NORMAL)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_TOP)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_BOTTOM);

        upRay = 1 << LayerMask.NameToLayer(s_COLLLISIONS_NORMAL)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_TOP);

        downRay = 1 << LayerMask.NameToLayer(s_COLLLISIONS_NORMAL)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_BOTTOM);
    }
}