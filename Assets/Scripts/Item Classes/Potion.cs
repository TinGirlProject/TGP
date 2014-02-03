﻿using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour
{
	void Start()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			Item pot = new Item("Small Health Potion", "A potion that recovers\na small amount of health.", 99, 1, true);

			pot.Icon = Resources.Load("Item Icons/Consumables/" + pot.Name) as Texture2D;

			if (PlayerInventory.AddItem(pot))
			{
				Destroy(this.gameObject);
			}
		}
	}
}