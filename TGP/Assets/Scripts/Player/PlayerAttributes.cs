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
	private static AreaInfo s_curAreaInfo;
    private static float s_slignshotForce = 10;

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
		s_curAreaInfo = areaInfo;

		Debug.Log("New Area Entered: " + s_curAreaInfo.AreaName);

		if (PlayerQuests.ActiveQuests.Count > 0)
		{
			for (int i = 0; i < PlayerQuests.ActiveQuests.Count; i++)
			{
				for (int j = 0; j < PlayerQuests.ActiveQuests[i].ActiveObjectives.Count; j++)
				{
					if (PlayerQuests.ActiveQuests[i].ActiveObjectives[j].CheckArea(s_curAreaInfo.AreaName))
						return;
				}
			}
		}
	}

    #region Setters and Getters
    public static AreaInfo CurAreaInfo
	{
		get { return s_curAreaInfo; }
		set { s_curAreaInfo = value; }
    }

    public static float SlingshotForce
    {
        get { return s_slignshotForce; }
        set { s_slignshotForce = value; }
    }
    #endregion
}
