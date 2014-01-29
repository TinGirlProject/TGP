using UnityEngine;
using System.Collections;

public class CollectObjective : Objective 
{
	private uint _curAmount;
	private uint _neededAmount;
	private Item _itemNeeded;

	public CollectObjective()
	{
		_description = "Missing Description";
		_objectiveComplete = false;
		_curAmount = 0;
		_neededAmount = 0;
		_itemNeeded = null;
		_myQuest = null;
	}

	public CollectObjective(string description, Quest myQuest, uint neededAmount, Item itemToCollect)
	{
		_description = description;
		_objectiveComplete = false;
		_myQuest = myQuest;
		_neededAmount = neededAmount;
		_itemNeeded = itemToCollect;
	}

	public bool ItemCollected(Item itemCollected)
	{
		if (itemCollected.Name == _itemNeeded.Name)
		{
			if (_curAmount + 1 <= _neededAmount)
			{
				_curAmount++;
				return true;
			}

			if (_curAmount == _neededAmount)
			{
				_objectiveComplete = true;
				_myQuest.SendMessage("UpdateObjective", this, SendMessageOptions.RequireReceiver);
				return true;
			}
		}
		return false;
	}
}
