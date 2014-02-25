using UnityEngine;
using System.Collections;

public class MainQuestGiver : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			string name = "The Great Tree";
			string description = "You've found your way into the great tree.  Make your\n" +
								"way down through the tree to discover it's secrets.";
			
			Quest quest = new Quest();
			
			GoToObjective objective = new GoToObjective("Go to the base of the Great Tree", quest, "Tree Base");
			
			quest.Name = name;
			quest.Description = description;
			quest.QuestType = QuestTypes.eMAIN_QUEST;
			quest.ActiveObjectives.Add(objective);
			
			if (!PlayerQuests.ActiveQuests.Contains(quest))
			{
				if (PlayerQuests.AddActiveQuest(quest))
					Destroy(this.gameObject);
			}
		}
	}
}
