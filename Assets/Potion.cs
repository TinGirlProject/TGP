using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour 
{
	void Start()
	{
		transform.name = "HealthPotionLarge";
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			Item pot = new Item(transform.name, "Large Potion", 99, 1);

			pot.Icon = Resources.Load("Item Icons/Consumables/" + pot.Name) as Texture2D;

			if (PlayerInventory.AddItem(pot))
			{
				Destroy(this.gameObject);
			}
		}
	}
}
