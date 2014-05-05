using UnityEngine;
using System.Collections;

// This script must be attached to a game object to tell Unity where the Moving Platforms should move to.
public class PlatformTarget : MonoBehaviour 
{
    // We'll draw a gizmo in the scene view, so it can be found....
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "platformIcon.tif");
    }
}