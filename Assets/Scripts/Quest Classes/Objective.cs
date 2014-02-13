/// <summary>
/// Objective.cs
/// Written By Galen Manuel
/// Last modified January 29th, 2014.
/// 
/// This is the base class for all quest objectives in the game.
/// </summary>
using UnityEngine;
using System.Collections;

public class Objective 
{
	#region All Objective Variables
	private string _description;
	private bool _objectiveComplete;
	private Quest _myQuest;
	private ObjectiveTypes _type;
	#endregion
	#region Collect Objective Variables
	private int _collectCurAmount;
	private int _collectNeededAmount;
	private string _collectItemNeeded;
	#endregion

	public Objective()
	{
		_description = "Missing Description";
		_objectiveComplete = false;
		_myQuest = null;
		_type = ObjectiveTypes.eNONE;
		_collectCurAmount = -1;
		_collectNeededAmount = -1;
		_collectItemNeeded = "Set Item";
	}

	public void CompleteObjective()
	{
		_objectiveComplete = true;
	}

	public bool CollectItemCollected(QuestItem itemCollected)
	{
		//if (itemCollected.Name == _collectItemNeeded)
		//{
			if (_collectCurAmount + 1 <= _collectNeededAmount)
			{
				_collectCurAmount++;
				Debug.Log("Current Amount: " + _collectCurAmount);
				if (_collectCurAmount == _collectNeededAmount)
				{
					CompleteObjective();
					_myQuest.UpdateObjective(this);
					return true;
				}
				return true;
			}
		//}
		return false;
	}

	public string Description
	{
		get { return _description; }
		set { _description = value; }
	}

	public bool ObjectiveComplete
	{
		get { return _objectiveComplete; }
	}

	public Quest MyQuest
	{
		get { return _myQuest; }
		set { _myQuest = value; }
	}

	public ObjectiveTypes ObjectiveType
	{
		get { return _type; }
		set { _type = value; }
	}

	public int CollectCurAmount
	{
		get { return _collectCurAmount; }
		set { _collectCurAmount = value; }
	}

	public int CollectNeededAmount
	{
		get { return _collectNeededAmount; }
		set { _collectNeededAmount = value; }
	}

	public string CollectItemNeeded
	{
		get { return _collectItemNeeded; }
		set { _collectItemNeeded = value; }
	}
}

public enum ObjectiveTypes
{
	eNONE,
	eCOLLECT,
	eKILL,
	eSPEAKTO,
	eGOTO,
	eFIND
}
