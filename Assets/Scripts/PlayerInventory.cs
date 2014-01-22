/// <summary>
/// Player inventory.cs
/// Written By Galen Manuel
/// Last modified January 22nd, 2014.
/// 
/// This class will hold the player's inventory including money and
/// all functions relating to the inventory.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour 
{
	private static List<Item> _inventory = new List<Item>();		// The actual inventory
	private static int _curInventorySlots;							// Current number of inventory slots
	private static int _maxInventorySlots;							// Max number of inventory slots
	private static int _curMoney;									// Current amount of money.
	private static int _maxMoney;									// Max amount of money.

	private Item testItem;

	// Default Constructor.
	public PlayerInventory()
	{
		// Default values
		_curInventorySlots = 6;
		_maxInventorySlots = 24;
		_curMoney = 0;
		_maxMoney = 999999;
	}

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public static bool AddItem(Item itemToAdd)
	{
		// Check if player has item already.
		if (_inventory.Count > 0)
		{
			// Loop through the entire inventory
			for (int cnt = 0; cnt < _inventory.Count; cnt++)
			{
				// If the item being added exists in inventory already
				if (itemToAdd.Name == _inventory[cnt].Name)
				{
					// If player can hold more than one of the item
					if (_inventory[cnt].MaxAmount > 1)
					{
						// If the player has less than the max amount, add item to inventory
						if (_inventory[cnt].CurAmount < _inventory[cnt].MaxAmount)
						{
							_inventory[cnt].CurAmount++;
							Debug.Log("Player Has Item And It Is Stackable: Item Added");
							return true;
						}
					}
					// Add item to inventory
					else
					{
						_inventory.Add(itemToAdd);
						Debug.Log("Player Has Item And It Is Not Stackable: Item Added");
						return true;
					}
				}
				// Player does not have item already
				else
				{
					_inventory.Add(itemToAdd);
					Debug.Log("Player Does Not Have Item: Item Added");
					return true;
				}
			}
		}
		// If inventory is empty, add the item.
		else
		{
			_inventory.Add(itemToAdd);
			Debug.Log("Player Has Empty Inventory: Item Added");
			return true;
		}
		// Player could not add item
		Debug.Log("Item NOT Added");
		return false;
	}

	public static bool RemoveItem(Item itemToRemove)
	{
		// Loop through the entire inventory
		for (int cnt = 0; cnt < _inventory.Count; cnt++)
		{
			// If the item exists in inventory
			if (itemToRemove.Name == _inventory[cnt].Name)
			{
				// If player has one or more than one of the item, remove one of the item
				if (_inventory[cnt].CurAmount > 1)
				{
					_inventory[cnt].CurAmount--;
					Debug.Log("Item Removed");
					return true;
				}
				// Otherwise, remove the item completely
				else
				{
					_inventory.Remove(itemToRemove);
					Debug.Log("Item Removed");
					return true;
				}
			}
		}
		// Player could not remove item
		Debug.Log("Item NOT Removed");
		return false;
	}

	public static void ModifyMoney(int moneyMod)
	{
		// Modify player's cur money
		_curMoney += moneyMod;

		// Ensure money can't be negative
		if (_curMoney < 0)
			_curMoney = 0;

		// Ensure money can't be more than the max
		if (_curMoney > _maxMoney)
			_curMoney = _maxMoney;
	}

	public static bool IncreaseCurInventorySlots()
	{
		// Check if adding a new inventory slot would be more than the max slots allowed.
		if (_curInventorySlots + 1 > _maxInventorySlots)
		{
			return false;
		}

		// If all good, add inventory slot.
		_curInventorySlots++;
		return true;
	}

	public static List<Item> Inventory
	{
		get { return _inventory; }
	}

	public static int CurInventorySlots
	{
		get { return _curInventorySlots; }
		set { _curInventorySlots = value; }
	}

	public static int MaxInventorySlots
	{
		get { return _maxInventorySlots; }
		set { _maxInventorySlots = value; }
	}

	public static int CurMoney
	{
		get { return _curMoney; }
		set { _curMoney = value; }
	}

	public static int MaxMoney
	{
		get { return _maxMoney; }
		set { _maxMoney = value; }
	}
}
