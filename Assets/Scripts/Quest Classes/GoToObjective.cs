/// <summary>
/// GoToObjective.cs
/// 
/// @Galen Manuel
/// @Feb.24th, 2014
/// 
/// This class is what all collect objectives will be.
/// </summary>
using UnityEngine;
using System.Collections;

public class GoToObjective : Objective 
{
	private string _areaToReach;

	public GoToObjective()
	{
		// Default all variables.
		_description = "Missing Description";
		_objectiveComplete = false;
		_myQuest = null;

		_areaToReach = "";
	}

	public GoToObjective(string description, Quest myQuest, string areaToReach)
	{
		// Assign all variables
		_description = description;
		_objectiveComplete = false;
		_myQuest = myQuest;

		_areaToReach = areaToReach;
	}
	/// <summary>
	/// Checks if the area passed in is the one needed for this objective.
	/// </summary>
	/// <returns><c>true</c>, if area is the one needed, <c>false</c> otherwise.</returns>
	/// <param name="areaName">Area name.</param>
	public override bool CheckArea(string areaName)
	{
		if (areaName == _areaToReach)
		{
			_objectiveComplete = true;
			_myQuest.UpdateObjective(this);
			return true;
		}

		return false;
	}
	// Getters and Setters
	public override string AreaToReach
	{
		get { return _areaToReach; }
	}
}
