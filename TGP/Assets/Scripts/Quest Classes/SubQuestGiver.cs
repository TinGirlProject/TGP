using UnityEngine;
using System.Collections;

public class SubQuestGiver : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			string name = "Power Outage";
			string description = "Meep has lost some power from chasing that butterfly.\n" +
				"See if you come across any old batteries that could charge him up.";

			Quest quest = new Quest();
			
			CollectObjective objective = new CollectObjective("Collect 3 'Rusty Battery'.", quest, 3, "Rusty Battery");

			quest.Name = name;
			quest.Description = description;
			quest.QuestType = QuestTypes.eSUB_QUEST;
			quest.ActiveObjectives.Add(objective);
			
			if (!PlayerQuests.ActiveQuests.Contains(quest))
			{
				if (PlayerQuests.AddActiveQuest(quest))
					Destroy(this.gameObject);
			}
		}
	}
}
