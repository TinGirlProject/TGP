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
	protected string _description;
	protected bool _objectiveComplete;
	protected Quest _myQuest;

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
}

public enum ObjectiveType
{
	eCOLLECT,
	eKILL,
	eSPEAKTO,
	eGOTO,
	eFIND
}
