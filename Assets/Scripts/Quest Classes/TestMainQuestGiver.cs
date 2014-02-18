using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMainQuestGiver : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			Quest quest = new Quest(); 

			CollectObjective objective = new CollectObjective("Collect 3 'Rusty Battery'.", quest, 3, "Rusty Battery");

			quest.Name = "Power Outage";
			quest.Description = "Meep has lost some power from chasing that butterfly.\n" +
								"See if you come across any old batteries that could charge him up.";
			quest.ActiveObjectives.Add(objective);

			if (!PlayerQuests.ActiveQuests.Contains(quest))
			{
				if (PlayerQuests.AddActiveQuest(quest))
					Destroy(this.gameObject);
			}
		}
	}
}
