/// <summary>
/// AreaInfo.cs
/// 
/// @Galen Manuel
/// @Feb. 24th, 2014
/// 
/// This class holds information on the area in the gameworld.  Should be placed on a trigger GameObject.
/// </summary>
using UnityEngine;
using System.Collections;

public class AreaInfo : MonoBehaviour 
{
	[SerializeField]
	private string _areaName;
	[SerializeField]
	private string _areaDescription;
	//[SerializeField]
	//private AudioClip _clipToPlay;

	/// <summary>
	/// Update the player's area info if it is not already updated.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			if (PlayerAttributes.CurAreaInfo != this)
			{
				PlayerAttributes.UpdateArea(this);
			}
		}
	}

	// Setters and Getters
	public string AreaName
	{
		get { return _areaName; }
	}

	public string AreaDescription
	{
		get { return _areaDescription; }
	}
}
