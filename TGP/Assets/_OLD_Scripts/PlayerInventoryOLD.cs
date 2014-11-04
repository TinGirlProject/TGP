/// <summary>
/// PlayerInventory.cs
/// 
/// @Galen Manuel
/// @Feb.24th, 2014
/// 
/// This class will hold the player's inventory, money and quests and
/// all functions relating to them.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryOLD: MonoBehaviour 
{
	private static List<Item> _inventory = new List<Item>();					// The actual inventory
	private static int _curInventorySlots;										// Current number of inventory slots
	private static int _maxInventorySlots;										// Max number of inventory slots
	private static int _curMoney;												// Current amount of money.
	private static int _maxMoney;												// Max amount of money.

	// Default Constructor.
	public PlayerInventoryOLD()
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

	#region Inventory and Money functions
	public static bool AddItem(Item itemToAdd)
	{
		// Check if player has items already.
		if (_inventory.Count > 0)
		{
			// Loop through the entire inventory
			for (int cnt = 0; cnt < _inventory.Count; cnt++)
			{
				// If the item being added exists in inventory already
                if (itemToAdd.name == _inventory[cnt].name)
                {
                    // Item is stackable
                    if (_inventory[cnt].maxAmount > 1)
                    {
                        // If the player has less than the max amount, add item to inventory
                        if (_inventory[cnt].curAmount < _inventory[cnt].maxAmount)
                        {
                            _inventory[cnt].curAmount++;
                            Debug.Log("Player has \"" + itemToAdd.name + "\" and it is stackable: \"" + itemToAdd.name + "\" added");

                            if (itemToAdd is QuestItem)
                            {
                                if (PlayerQuests.ActiveQuests.Count > 0)
                                {
                                    for (int i = 0; i < PlayerQuests.ActiveQuests.Count; i++)
                                    {
                                        Debug.Log("Active Quest Count: " + PlayerQuests.ActiveQuests.Count);
                                        if (PlayerQuests.ActiveQuests[i].ActiveObjectives.Count > 0)
                                        {
                                            Debug.Log("Active Quest Objective Count: " + PlayerQuests.ActiveQuests[i].ActiveObjectives.Count + " i: " + i);
                                            for (int j = 0; j < PlayerQuests.ActiveQuests[i].ActiveObjectives.Count; j++)
                                            {
                                                Debug.Log("Active Quest Objective Count: " + PlayerQuests.ActiveQuests[i].ActiveObjectives.Count + " j: " + j);
                                                if (itemToAdd.name == PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemNeeded)
                                                {
                                                    PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemCollected(itemToAdd);
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            return true;
                        }
                    }
                    // Non stackable
                    else
                    {
                        // If player has room
                        if (_inventory.Count < _curInventorySlots)
                        {
                            _inventory.Add(itemToAdd);
                            Debug.Log("Player has \"" + itemToAdd.name + "\" and it is not stackable: \"" + itemToAdd.name + "\" added");

                            if (itemToAdd is QuestItem)
                            {
                                for (int i = 0; i < PlayerQuests.ActiveQuests.Count; i++)
                                {
                                    Debug.Log("Active Quest Count: " + PlayerQuests.ActiveQuests.Count);
                                    for (int j = 0; j < PlayerQuests.ActiveQuests[i].ActiveObjectives.Count; j++)
                                    {
                                        Debug.Log("Active Quest Objective Count: " + PlayerQuests.ActiveQuests[i].ActiveObjectives.Count);
                                        if (itemToAdd.name == PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemNeeded)
                                        {
                                            PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemCollected(itemToAdd);
                                            return true;
                                        }
                                    }
                                }
                            }

                            return true;
                        }
                    }
                }
			}

			// New Item, add if player has room.
			if (_inventory.Count < _curInventorySlots)
			{
				_inventory.Add(itemToAdd);
                Debug.Log("Player does not have \"" + itemToAdd.name + "\": \"" + itemToAdd.name + "\" added");

				if (itemToAdd is QuestItem)
				{
					for ( int i = 0; i < PlayerQuests.ActiveQuests.Count; i++)
					{
						Debug.Log("Active Quest Count: " + PlayerQuests.ActiveQuests.Count);
						for (int j = 0; j < PlayerQuests.ActiveQuests[i].ActiveObjectives.Count; j++)
						{
							Debug.Log("Active Quest Objective Count: " + PlayerQuests.ActiveQuests[i].ActiveObjectives.Count);
                            if (itemToAdd.name == PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemNeeded)
                            {
                                PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemCollected(itemToAdd);
                                return true;
                            }
						}
					}
				}

				return true;
			}
		}
		// If inventory is empty, add the item.
		else
		{
			_inventory.Add(itemToAdd);
			Debug.Log("Player has empty inventory: \"" + itemToAdd.name + "\" added");

			if (itemToAdd is QuestItem)
			{
				for ( int i = 0; i < PlayerQuests.ActiveQuests.Count; i++)
				{
					Debug.Log("Active Quest Count: " + PlayerQuests.ActiveQuests.Count);
					for (int j = 0; j < PlayerQuests.ActiveQuests[i].ActiveObjectives.Count; j++)
					{
						Debug.Log("Active Quest Objective Count: " + PlayerQuests.ActiveQuests[i].ActiveObjectives.Count);
                        if (itemToAdd.name == PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemNeeded)
                        {
                            PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemCollected(itemToAdd);
                            return true;
                        }
					}
				}
			}

			return true;
		}
		// Player could not add item
		Debug.Log("\"" + itemToAdd.name + "\" NOT added");
		return false;
	}

	public static bool RemoveItem(Item itemToRemove)
	{
		// Loop through the entire inventory
		for (int cnt = 0; cnt < _inventory.Count; cnt++)
		{
			// If the item exists in inventory
            if (itemToRemove.name == _inventory[cnt].name)
            {
                // If player has one or more than one of the item, remove one of the item
                if (_inventory[cnt].curAmount > 1)
                {
                    _inventory[cnt].curAmount--;
                    Debug.Log("\"" + itemToRemove.name + "\" removed");
                    return true;
                }
                // Otherwise, remove the item completely
                else
                {
                    _inventory.RemoveAt(cnt);
                    Debug.Log("\"" + itemToRemove.name + "\" removed");
                    return true;
                }
            }
		}
		// Player could not remove item
		Debug.Log("\"" + itemToRemove.name + "\" NOT removed");
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
	#endregion

	#region Setters and Getters
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

//	public List<QuestItem> QuestInventory
//	{
//		get { return _questInventory; }
//	}
//
//	public int MaxQuestInventorySlots
//	{
//		get { return _maxQuestInventorySlots; }
//		set { _maxQuestInventorySlots = value; }
//	}
	#endregion
}
