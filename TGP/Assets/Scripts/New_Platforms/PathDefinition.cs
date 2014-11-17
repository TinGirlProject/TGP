using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PathDefinition : MonoBehaviour 
{
    public Transform[] _points;

    public IEnumerator<Transform> GetPathEnumerator()
    {
        if (_points == null || _points.Length < 1)
            yield break;

        int direction = 1;
        int index = 0;

        while (true)
        {
            yield return _points[index];

            if (_points.Length == 1)
                continue;

            if (index <= 0)
                direction = 1;
            else if (index >= _points.Length - 1)
                direction = -1;

            index = index + direction;
        }
    }

    public void OnDrawGizmos()
    {
        if (_points == null || _points.Length < 2)
            return;

        for (int i = 1; i < _points.Length; i++)
        {
            Gizmos.DrawLine(_points[i - 1].position, _points[i].position);
        }
    }
}