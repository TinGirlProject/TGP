/// <summary>
/// Quest.cs
/// Written By Galen Manuel
/// Last modified January 29th, 2014.
/// 
/// This is the base class for all quests in the game.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest 
{
	private string _name;
	private string _description;
	private List<Objective> _activeObjectives;
	private List<Objective> _completeObjectives;
	private bool _questComplete;

	public Quest()
	{
		_name = "Missing Name";
		_description = "Missing Description";
		_activeObjectives = new List<Objective>();
		_completeObjectives = new List<Objective>();
		_questComplete = false;
	}

	public Quest(string name, string description, List<Objective> activeObjectives)
	{
		_name = name;
		_description = description;
		_activeObjectives = activeObjectives;
		_completeObjectives = new List<Objective>();
		_questComplete = false;
	}

	public void CompleteQuest()
	{
		_questComplete = true;
		PlayerQuests.CompleteActiveQuest(this);
	}

	public void UpdateObjective(Objective toUpdate)
	{
		if (_activeObjectives.Contains(toUpdate))
		{
			if (toUpdate.ObjectiveComplete)
			{
				_activeObjectives.Remove(toUpdate);
				_completeObjectives.Add(toUpdate);
				if (_activeObjectives.Count == 0)
				{
					CompleteQuest();
				}
			}
		}
	}

	public string Name
	{
		get { return _name; }
		set { _name = value; }
	}

	public string Description
	{
		get { return _description; }
		set { _description = value; }
	}

	public List<Objective> ActiveObjectives
	{
		get { return _activeObjectives; }
	}

	public List<Objective> CompleteObjectives
	{
		get { return _completeObjectives; }
	}

	public bool QuestComplete
	{
		get { return _questComplete; }
		set { _questComplete = value; }
	}
}
