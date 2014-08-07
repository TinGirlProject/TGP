/// <summary>
/// Potion.cs
/// 
/// @Galen Manuel
/// @Feb. 24th, 2014
/// 
/// This class will add a potion to the player's inventory if the player has space.
/// </summary>
using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour
{
	Item pot;

	void Start()
	{
        pot = new Item("Small Health Potion", "A potion that recovers\na small amount of health.", 99, 1, true);
        pot.icon = Resources.Load("Item Icons/Consumables/" + pot.name) as Texture2D;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			if (PlayerInventoryOLD.AddItem(pot))
			{
				Destroy(this.gameObject);
			}
		}
	}
}
