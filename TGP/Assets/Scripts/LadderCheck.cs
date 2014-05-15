using UnityEngine;
using System.Collections;

public class LadderCheck : MonoBehaviour 
{
	private bool msgSent;
	private bool bottomTrigger;
	private int facingAngle;

	private float curPlayerDir;
	private float prevPlayerDir;

	private Transform playerTransform;

	void Start()
	{
		msgSent = false;
		bottomTrigger = false;
		facingAngle = 0;
		curPlayerDir = 0;
		prevPlayerDir = 0;

		if (transform.name == "LadderTopLeft")
			facingAngle = 180;
		else if (transform.name == "LadderTopRight")
			facingAngle = 0;
		else
			bottomTrigger = true;

		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (!bottomTrigger)
			{
                if (Mathf.Approximately(playerTransform.eulerAngles.y, facingAngle))
				{
                    SendMessageUpwards("PlayerInRange", true, SendMessageOptions.RequireReceiver);
                    other.SendMessage("SetCurLadder", transform.gameObject, SendMessageOptions.RequireReceiver);
				}
			}
			else
			{
                if (other.GetComponent<PlayerController>().inAirState != Character.InAirState.CLIMBING)
                {
                    SendMessageUpwards("PlayerInRange", true, SendMessageOptions.RequireReceiver);
                    other.SendMessage("SetCurLadder", transform.gameObject, SendMessageOptions.RequireReceiver);
                }
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (!bottomTrigger)
			{
				curPlayerDir = playerTransform.eulerAngles.y;
				if (curPlayerDir != prevPlayerDir)
				{
					msgSent = false;
					prevPlayerDir = curPlayerDir;
				}

				if (!msgSent)
				{
					if (!Mathf.Approximately(playerTransform.eulerAngles.y, facingAngle))
					{
						msgSent = true;
						SendMessageUpwards("PlayerInRange", false, SendMessageOptions.RequireReceiver);
						other.SendMessage("ResetCurLadder", SendMessageOptions.RequireReceiver);
					}
					else
					{
                        if (other.GetComponent<PlayerController>().inAirState != Character.InAirState.CLIMBING)
                        {
                            msgSent = true;
                            SendMessageUpwards("PlayerInRange", true, SendMessageOptions.RequireReceiver);
                            other.SendMessage("SetCurLadder", transform.gameObject, SendMessageOptions.RequireReceiver);
                        }
					}
				}
			}
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
