/// <summary>
	/// CollectObjective.cs
	/// Written By Galen Manuel
	/// Last modified January 29th, 2014.
	/// 
	/// This class is what all collect objectives will be.
	/// </summary>
	using UnityEngine;
using System.Collections;

public class CollectObjective : Objective 
{
	private int _curAmount;
	private int _neededAmount;
	private string _itemNeeded;
	
	public CollectObjective()
	{
		_description = "Missing Description";
		_objectiveComplete = false;
		_curAmount = 0;
		_neededAmount = 0;
		_itemNeeded = "";
		_myQuest = null;
	}
	
	public CollectObjective(string description, Quest myQuest, int neededAmount, string itemToCollect)
	{
		_description = description;
		_objectiveComplete = false;
		_myQuest = myQuest;
		_neededAmount = neededAmount;
		_itemNeeded = itemToCollect;
	}
	
	public override bool ItemCollected(Item itemCollected)
	{
		if (itemCollected.Name == _itemNeeded)
		{
			if (_curAmount + 1 <= _neededAmount)
			{
				_curAmount++;
				Debug.Log(_curAmount);

				if (_curAmount == _neededAmount)
				{
					_objectiveComplete = true;
					_myQuest.UpdateObjective(this);
				}

				return true;
			}

		}
		return false;
	}

	public override int CurAmount
	{
		get { return _curAmount; }
	}

	public override int NeededAmount
	{
		get { return _neededAmount; }
	}

	public override string ItemNeeded
	{
		get { return _itemNeeded; }
	}
}