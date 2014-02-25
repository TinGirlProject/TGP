/// <summary>
/// PlayerAttributes.cs
/// 
/// @Galen Manuel
/// @Feb.24th, 2014
/// 
/// This class will hold static variables belonging to the player that will need to be accessed by many scripts.
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour 
{
	private static AreaInfo _curAreaInfo;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public static void UpdateArea(AreaInfo areaInfo)
	{
		_curAreaInfo = areaInfo;

		Debug.Log("New Area Entered: " + _curAreaInfo.AreaName);

		if (PlayerQuests.ActiveQuests.Count > 0)
		{
			for (int i = 0; i < PlayerQuests.ActiveQuests.Count; i++)
			{
				for (int j = 0; j < PlayerQuests.ActiveQuests[i].ActiveObjectives.Count; j++)
				{
					if (PlayerQuests.ActiveQuests[i].ActiveObjectives[j].CheckArea(_curAreaInfo.AreaName))
						return;
				}
			}
		}
	}

	public static AreaInfo CurAreaInfo
	{
		get { return _curAreaInfo; }
		set { _curAreaInfo = value; }
	}
}
