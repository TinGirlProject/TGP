/// <summary>
/// Item.cs
/// 
/// @Galen Manuel
/// @Feb.24th, 2014
/// 
/// Base class for all items in game.
/// </summary>
using UnityEngine;

public class Item
{
	protected string _name;
	protected string _description;
	protected bool _canBeDestroyed;
	protected int _maxAmount;								// Greater than one if stackable.
	protected int _curAmount;
	protected Texture2D _icon;								// Item icon in inventory.
	
	public Item()
	{
		// Default all variables
		_name = "Missing Name";
		_description = "Missing Description";
		_curAmount = -1;
		_maxAmount = -1;
		_canBeDestroyed = false;
	}
	
	public Item(string name, string description, int maxAmt, int curAmt, bool canBeDestroyed)
	{
		// Assigns all variables
		_name = name;
		_description = description;
		_maxAmount = maxAmt;
		_curAmount = curAmt;
		_canBeDestroyed = canBeDestroyed;
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

	public bool CanBeDestroyed
	{
		get { return _canBeDestroyed; }
		set { _canBeDestroyed = value; }
	}
#endregion

	public virtual string ToolTip()
	{
		return Name + "\n" + Description + "\n" + CurAmount;
	}
}