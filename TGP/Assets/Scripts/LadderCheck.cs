using UnityEngine;
using System.Collections;

public class LadderCheck : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SendMessageUpwards("PlayerInRange", true, SendMessageOptions.RequireReceiver);
			other.SendMessage("SetCurLadder", transform.gameObject, SendMessageOptions.RequireReceiver);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SendMessageUpwards("PlayerInRange", false, SendMessageOptions.RequireReceiver);
			other.SendMessage("ResetCurLadder", SendMessageOptions.RequireReceiver);
		}
	}
}
