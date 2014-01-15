/// <summary>
/// Player inventory.cs
/// Written By Galen Manuel
/// Last modified January 14th, 2014.
/// 
/// This class will hold the player's inventory including money and
/// all functions relating to the inventory.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour 
{
	private List<Item> _inventory = new List<Item>();
	private int _curInventorySlots;
	private int _maxInventorySlots;
	private int _curMoney;
	private int _maxMoney;

	public PlayerInventory()
	{
		DontDestroyOnLoad(this.gameObject);
		// Default values
		_curInventorySlots = 9;
		_maxInventorySlots = 18;
		_curMoney = 0;
		_maxMoney = 999999;
	}

	public bool AddItem(Item itemToAdd)
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
						_inventory.Add(itemToAdd);
						return true;
					}
				}
			}
			// Player does not have item already
			else
			{
				_inventory.Add(itemToAdd);
				return true;
			}
		}
		// Player could not add item
		return false;
	}

	public bool RemoveItem(Item itemToRemove)
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
					return true;
				}
				// Otherwise, remove the item completely
				else
				{
					_inventory.Remove(itemToRemove);
					return true;
				}
			}
		}
		// Player could not remove item
		return false;
	}

	public void ModifyMoney(int moneyMod)
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

	public bool IncreaseCurInventorySlots()
	{
		if (_curInventorySlots + 1 > _maxInventorySlots)
			return false;

		_curInventorySlots++;
		return true;
	}

	public List<Item> Inventory
	{
		get { return _inventory; }
	}

	public int CurInventorySlots
	{
		get { return _curInventorySlots; }
		set { _curInventorySlots = value; }
	}

	public int MaxInventorySlots
	{
		get { return _maxInventorySlots; }
		set { _maxInventorySlots = value; }
	}

	public int CurMoney
	{
		get { return _curMoney; }
		set { _curMoney = value; }
	}

	public int MaxMoney
	{
		get { return _maxMoney; }
		set { _maxMoney = value; }
	}
}
