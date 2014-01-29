using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour 
{
	private string _name;
	private string _description;
	private Objective[] _objectives;
	private bool _questComplete;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void CompleteQuest()
	{
		_questComplete = true;
	}

	public void UpdateObjective(Objective toUpdate)
	{

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

	public Objective[] Objectives
	{
		get { return _objectives; }
	}

	public bool QuestComplete
	{
		get { return _questComplete; }
		set { _questComplete = value; }
	}
}
