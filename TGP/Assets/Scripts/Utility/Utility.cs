using UnityEngine;
using UnityEditor;
using System.Collections;

public class Utility 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="color"></param>
    public static void DrawOutline(Rect rect, Color color)
    {
        Handles.color = color;
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMin, 0), new Vector3(rect.xMax, rect.yMin, 0));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMin, 0), new Vector3(rect.xMax, rect.yMax, 0));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMax, 0), new Vector3(rect.xMin, rect.yMax, 0));
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMax, 0), new Vector3(rect.xMin, rect.yMin, 0));
    }
}
