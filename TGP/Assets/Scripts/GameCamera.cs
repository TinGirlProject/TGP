using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{	
	private Transform target;
	public float trackSpeed = 25;	
	
	// Set target
	public void SetTarget(Transform t) 
    {
		target = t;
	}
	
	// Track target
	void LateUpdate() 
    {
		if (target) 
        {
            float x = MathP.IncrementTowards(transform.position.x, target.position.x, trackSpeed);
            float y = MathP.IncrementTowards(transform.position.y, target.position.y, trackSpeed);
			transform.position = new Vector3(x, y, transform.position.z);
		}
	}
}