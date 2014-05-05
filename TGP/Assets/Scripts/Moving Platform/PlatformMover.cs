using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour 
{
    public GameObject targetA;
    public GameObject targetB;

    public float speed = 0.1f;

    void FixedUpdate()
    {
        float weight = Mathf.Cos(Time.time * speed * 2 * Mathf.PI) * 0.5f + 0.5f;
        transform.position = targetA.transform.position * weight +
                             targetB.transform.position * (1 - weight);
    }
}