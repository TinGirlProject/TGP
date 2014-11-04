using UnityEngine;
using System.Collections;

public class Raylayers
{
    public static readonly int s_onlyCollisions;
    public static readonly int s_upRay;
    public static readonly int s_downRay;

    private const string s_COLLLISIONS_NORMAL = "CollisionsNormal";
    private const string s_COLLISIONS_SOFT_TOP = "CollisionsSoftTop";
    private const string s_COLLISIONS_SOFT_BOTTOM = "CollisionsSoftBottom";

    static Raylayers()
    {
        s_onlyCollisions = 1 << LayerMask.NameToLayer(s_COLLLISIONS_NORMAL)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_TOP)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_BOTTOM);

        s_upRay = 1 << LayerMask.NameToLayer(s_COLLLISIONS_NORMAL)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_TOP);

        s_downRay = 1 << LayerMask.NameToLayer(s_COLLLISIONS_NORMAL)
            | 1 << LayerMask.NameToLayer(s_COLLISIONS_SOFT_BOTTOM);
    }
}