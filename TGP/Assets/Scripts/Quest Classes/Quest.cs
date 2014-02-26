/// <summary>
/// Quest.cs
/// 
/// @Galen Manuel
/// @Feb.24th, 2014
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
	private QuestTypes _type;

	public Quest()
	{
		// Default all variables
		_name = "Missing Name";
		_description = "Missing Description";
		_activeObjectives = new List<Objective>();
		_completeObjectives = new List<Objective>();
		_questComplete = false;
		_type = QuestTypes.eNONE_QUEST;
	}

	public Quest(string name, string description, List<Objective> activeObjectives, QuestTypes type)
	{
		// Assign all variables
		_name = name;
		_description = description;
		_activeObjectives = activeObjectives;
		_completeObjectives = new List<Objective>();
		_questComplete = false;
		_type = type;
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

	public QuestTypes QuestType
	{
		get { return _type; }
		set { _type = value; }
	}
}

public enum QuestTypes
{
	eMAIN_QUEST,
	eSUB_QUEST,
	eNONE_QUEST
}
