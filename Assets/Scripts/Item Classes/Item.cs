/// <summary>
/// Item.cs
/// Written By Galen Manuel
/// Last modified January 14th, 2014.
/// 
///Base class for all items in game.
/// </summary>
using UnityEngine;

public class Item 
{
	protected string _name;
	protected string _description;
	private int _maxAmount;
	private int _curAmount;
	//private Texture2D _icon;

	/// <summary>
	/// Initializes a new instance of the <see cref="Item"/> class
	/// using defaults.
	/// </summary>
	public Item()
	{
		_name = "Missing Name";
		_description = "Missing Description";
		_curAmount = -1;
		_maxAmount = -1;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Item"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="description">Description.</param>
	/// <param name="maxAmt">Max amt.</param>
	/// <param name="curAmt">Current amt.</param>
	public Item(string name, string description, int maxAmt, int curAmt)
	{
		_name = name;
		_description = description;
		_maxAmount = maxAmt;
		_curAmount = curAmt;	
	}

#region Setters and Getters	
	public string Name
	{
		get{ return _name; }
		set{ _name = value; }
	}

	public string Description
	{
		get{ return _description; }
		set{ _description = value; }
	}
	
	public int CurAmount
	{
		get{ return _curAmount; }
		set{ _curAmount = value; }
	}
	
	public int MaxAmount
	{
		get{ return _maxAmount; }
		set{ _maxAmount = value; }
	}
	
//	public Texture2D Icon
//	{
//		get { return _icon; }
//		set { _icon = value; }
//	}
#endregion
}
