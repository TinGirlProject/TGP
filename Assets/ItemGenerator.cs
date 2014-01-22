using UnityEngine;

public class ItemGenerator : MonoBehaviour 
{
	private const string CONSUMABLES_PATH = "Item Icons/Consumables";

	public static Item CreateItem()
	{
		Item temp = new Item();

		return temp;
	}
}
