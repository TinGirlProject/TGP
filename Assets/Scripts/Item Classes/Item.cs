/// <summary>
/// Item.cs
/// Written By Galen Manuel
/// Last modified January 21st, 2014.
/// 
///Base class for all items in game.
/// </summary>
using UnityEngine;

public class Item
{
	protected string _name;									// Name of the item.
	protected string _description;							// Description of the item.
	private int _maxAmount;									// Max amount of this item in inventory. Greater than one if stackable.
	private int _curAmount;									// Current amount of item.
	private Texture2D _icon;								// Item icon in inventory.

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
	
	public Texture2D Icon
	{
		get { return _icon; }
		set { _icon = value; }
	}
#endregion
}
