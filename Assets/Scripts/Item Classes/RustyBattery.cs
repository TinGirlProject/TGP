﻿using UnityEngine;
using System.Collections;

public class RustyBattery : MonoBehaviour 
{
	private bool _playerOnQuest = false;

	private Objective _myObjective;
	private Quest _myQuest;

	void Start()
	{
		_myObjective = null;
		_myQuest = null;
	}

	// Update is called once per frame
	void Update () 
	{
		if (PlayerQuests.ActiveQuests.Count > 0 && !_playerOnQuest)
		{
			for (int i = 0; i < PlayerQuests.ActiveQuests.Count; i++)
			{
				if (PlayerQuests.ActiveQuests[i].Name == "Power Outage")
				{
					_myQuest = PlayerQuests.ActiveQuests[i];
					_playerOnQuest = true;
					renderer.enabled = true;
					collider.enabled = true;

					for (int j = 0; j < PlayerQuests.ActiveQuests[i].ActiveObjectives.Count; j++)
					{
						if (PlayerQuests.ActiveQuests[i].ActiveObjectives[j].ItemNeeded == transform.name)
						{
							_myObjective = PlayerQuests.ActiveQuests[i].ActiveObjectives[j];
						}
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.transform.tag.Equals("Player") && _playerOnQuest)
		{
			QuestItem bat = new QuestItem(transform.name, "An old old battery.", 3, 1, false, _myQuest);
			bat.Icon = Resources.Load("Item Icons/Quest Items/" + bat.Name) as Texture2D;

			if (PlayerInventory.AddItem(bat))
				Destroy(this.gameObject);
		}
	}
}
