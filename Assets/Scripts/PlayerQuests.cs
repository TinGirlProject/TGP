using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerQuests : MonoBehaviour 
{	
	private static List<Quest> _activeQuests = new List<Quest>();		// Player's active quests
	private static List<Quest> _completedQuests = new List<Quest>();	// Player's completed quests
	private static int _maxNumberOfQuests;								// Max number of quests

	public PlayerQuests()
	{
		_maxNumberOfQuests = 12;
	}

	#region Quest Functions
	public static bool AddActiveQuest(Quest questToAdd)
	{
		// Check if player has quests already.
		if (_activeQuests.Count > 0)
		{
			// Loop through active quests
			for (int cnt = 0; cnt < _activeQuests.Count; cnt++)
			{
				// If the quest being added exists in active quests already
				if (questToAdd.Name == _activeQuests[cnt].Name)
				{
					/* 
					 * Take quest and destroy it
					 * return false with reason?
					 */
					return false;
				}
			}
			
			// New Quest, add if player has room.
			if (_activeQuests.Count < _maxNumberOfQuests)
			{
				_activeQuests.Add(questToAdd);
				Debug.Log("Player does not have \"" + questToAdd.Name + "\": \"" + questToAdd.Name + "\" added");
				return true;
			}
		}
		// If no active quests, add the quest.
		else
		{
			_activeQuests.Add(questToAdd);
			Debug.Log("Player has empty Quest Log: \"" + questToAdd.Name + "\" added");
			return true;
		}
		// Player could not add item
		Debug.Log("\"" + questToAdd.Name + "\" NOT added");
		return false;
	}

	public static void CompleteActiveQuest(Quest quest)
	{
		if (quest.QuestComplete)
		{
			if (_activeQuests.Contains(quest))
			{
				_completedQuests.Add(quest);
				_activeQuests.Remove(quest);
				Debug.Log(quest.Name + " complete.");
			}
		}
	}
	#endregion

	public static List<Quest> ActiveQuests
	{
		get { return _activeQuests; }
	}
	
	public static List<Quest> CompletedQuests
	{
		get { return _completedQuests; }
	}
	
	public static int MaxNumberOfQuests
	{
		get { return _maxNumberOfQuests; }
		set { _maxNumberOfQuests = value; }
	}
}
