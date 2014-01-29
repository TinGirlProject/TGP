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
