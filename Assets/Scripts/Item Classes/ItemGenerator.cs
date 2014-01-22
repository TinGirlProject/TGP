using UnityEngine;

public class ItemGenerator 
{
	private const string CONSUMABLES_PATH = "Item Icons/Consumables/";
	private const string WEAPONS_PATH = "Item Icons/Weapons/";
	private const string QUEST_PATH = "Item Icons/Quest Items/";

	private string[] _consumableNameList = new string[4] {"Small Health Potion", "Large Health Potion", "Small Mana Potion", "Large Mana Potion"};

	public static Item CreateItem()
	{
		Item temp = new Item();

		return temp;
	}

	public static Item CreatePotion(string whatToMake)
	{
		Item temp = new Item();

		return temp;
//		if (whatToMake == "random")
//		{
//
//		}
//		else if (whatToMake == "
	}
}
