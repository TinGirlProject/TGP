using UnityEngine;
using System.Collections;

public class MathP : MonoBehaviour
{
    // Increase n towards target by speed
    public static float IncrementTowards(float n, float target, float a)
    {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
}