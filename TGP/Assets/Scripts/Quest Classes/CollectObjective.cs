/// <summary>
/// CollectObjective.cs
/// 
/// @Galen Manuel
/// @Feb.24th, 2014
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
		// Default all variables.
		_description = "Missing Description";
		_objectiveComplete = false;
		_myQuest = null;
		
		_curAmount = -1;
		_neededAmount = -1;
		_itemNeeded = "";
	}
	
	public CollectObjective(string description, Quest myQuest, int neededAmount, string itemToCollect)
	{
		// Assign all variables
		_description = description;
		_objectiveComplete = false;
		_myQuest = myQuest;

		_neededAmount = neededAmount;
		_itemNeeded = itemToCollect;
	}
	/// <summary>
	/// Check to see if the item is the one needed for this objective and responds appropriately.
	/// </summary>
	/// <returns><c>true</c>, if collected was item, <c>false</c> otherwise.</returns>
	/// <param name="itemCollected">Item collected.</param>
	public override bool ItemCollected(Item itemCollected)
	{
        if (itemCollected.Name == _itemNeeded)
        {
            if (_curAmount + 1 <= _neededAmount)
            {
                _curAmount++;
            }

            if (_curAmount == _neededAmount)
            {
                _objectiveComplete = true;
                _myQuest.UpdateObjective(this);
            }

            return true;
        }
		return false;
	}
	// Setters and Getters
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