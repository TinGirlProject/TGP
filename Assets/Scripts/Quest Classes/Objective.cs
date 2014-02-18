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
	protected string _description;
	protected bool _objectiveComplete;
	protected Quest _myQuest;
	#endregion

	public Objective()
	{
		_description = "Missing Description";
		_objectiveComplete = false;
		_myQuest = null;
	}

	public void CompleteObjective()
	{
		_objectiveComplete = true;
	}

	public virtual bool ItemCollected(Item itemCollected)
	{
		if (_objectiveComplete)
			return true;

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

	public virtual int CurAmount
	{
		get { return -1; }
	}
	
	public virtual int NeededAmount
	{
		get { return -1; }
	}

	public virtual string ItemNeeded
	{
		get { return "Not a Collect Objective"; }
	}
}
