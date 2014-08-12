/// <summary>
/// Item.cs
/// 
/// @Galen Manuel
/// @July 23rd 2014
/// 
/// Base class for all items in game.
/// </summary>
using UnityEngine;

[System.Serializable]
public class Item : ScriptableObject
{
	public string name;
    public string description;
    public bool keyItem;
    public int maxAmount;								// Greater than one if stackable.
    [HideInInspector]
    public int curAmount;
    public Texture2D icon;								// Item icon in inventory.

    /// <summary>
    /// Default Constructor
    /// </summary>
    public Item()
    {
        // Default all variables
        name = "Missing Name";
        description = "Missing Description";
        curAmount = 0;
        maxAmount = 1;
        keyItem = false;
    }

    /// <summary>
    /// Parameter Constructor
    /// </summary>
    /// <param name="nameIn">The name of the item.</param>
    /// <param name="descriptionIn">The description of the item.</param>
    /// <param name="maxAmtIn">The maximum amount of this item that the player can have.</param>
    /// <param name="curAmtIn"></param>
    /// <param name="canBeDestroyedIn"></param>
    public Item(string nameIn, string descriptionIn, int maxAmtIn, int curAmtIn, bool canBeDestroyedIn)
    {
        // Assigns all variables
        name = nameIn;
        description = descriptionIn;
        maxAmount = maxAmtIn;
        curAmount = curAmtIn;
        keyItem = canBeDestroyedIn;

        icon = Resources.Load(PlayerGUI.s_Item_Resources_Path + name) as Texture2D;
    }

    public virtual string ToolTip()
    {
        return name + "\n" + description + "\n" + curAmount;
    }
}
