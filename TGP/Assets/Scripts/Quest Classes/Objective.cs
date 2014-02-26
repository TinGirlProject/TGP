/// <summary>
/// Objective.cs
/// 
/// @Galen Manuel
/// @Feb.24th, 2014
/// 
/// This is the base class for all quest objectives in the game.
/// </summary>
using UnityEngine;
using System.Collections;

public abstract class Objective 
{
	#region All Objective Variables
	protected string _description;
	protected bool _objectiveComplete;
	protected Quest _myQuest;
	#endregion

	#region All Objective Methods
	public void CompleteObjective()
	{
		_objectiveComplete = true;
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
	#endregion

	#region CollectObjective Methods
	public virtual bool ItemCollected(Item itemCollected)
	{
		if (_objectiveComplete)
			return true;

		return false;
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
		get { return ""; }
	}
	#endregion

	#region GoToObjective Methods
	public virtual bool CheckArea(string area)
	{
		if (_objectiveComplete)
			return true;
		
		return false;
	}
	
	public virtual string AreaToReach
	{
		get { return ""; }
	}
	#endregion
}
