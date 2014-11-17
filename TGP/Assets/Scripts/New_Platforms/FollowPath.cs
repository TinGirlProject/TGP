using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour 
{
    public enum FollowType
    {
        MoveTowards,
        Lerp
    }

    public FollowType _type = FollowType.MoveTowards;
    public PathDefinition _path;

    public float _speed = 1.0f;
    public float _maxDistanceToGoal = 0.1f;

    private IEnumerator<Transform> m_currentPoint;
    
    public void Start()
    {
        if (_path == null)
        {
            Debug.LogError("Path cannot be null!", gameObject);
            return;
        }

        m_currentPoint = _path.GetPathEnumerator();
        m_currentPoint.MoveNext();

        if (m_currentPoint.Current == null)
            return;

        transform.position = m_currentPoint.Current.position;
    }

    public void Update()
    {
        if (m_currentPoint == null || m_currentPoint.Current == null)
            return;

        if (_type == FollowType.MoveTowards)
        {
             transform.position = Vector3.MoveTowards(transform.position, m_currentPoint.Current.position, Time.deltaTime * _speed);
        }  
        else if (_type == FollowType.Lerp)
        {
            transform.position = Vector3.Lerp(transform.position, m_currentPoint.Current.position, Time.deltaTime * _speed);
        }

        float distanceSquared = (transform.position - m_currentPoint.Current.position).sqrMagnitude;

        if (distanceSquared < _maxDistanceToGoal * _maxDistanceToGoal)
        {
            m_currentPoint.MoveNext();
        }
    }
}