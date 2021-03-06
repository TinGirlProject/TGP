﻿using UnityEngine;
using System.Collections;

public class ColliderGizmo : MonoBehaviour 
{
    void OnDrawGizmos()
    {
        OnDrawGizmosSelected();
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);  //center sphere
        if (transform.collider != null)
            Gizmos.DrawWireCube(transform.position, transform.collider.bounds.size);
    }
}
