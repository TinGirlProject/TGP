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
    public bool canBeDestroyed;
    public int maxAmount;								// Greater than one if stackable.
    public int curAmount;
    public Texture2D icon;								// Item icon in inventory.

    public Item()
    {
        // Default all variables
        name = "Missing Name";
        description = "Missing Description";
        curAmount = -1;
        maxAmount = -1;
        canBeDestroyed = false;
    }

    public Item(string nameIn, string descriptionIn, int maxAmtIn, int curAmtIn, bool canBeDestroyedIn)
    {
        // Assigns all variables
        name = nameIn;
        description = descriptionIn;
        maxAmount = maxAmtIn;
        curAmount = curAmtIn;
        canBeDestroyed = canBeDestroyedIn;

        icon = Resources.Load(PlayerGUI.s_Item_Resources_Path + name) as Texture2D;
    }

    public virtual string ToolTip()
    {
        return name + "\n" + description + "\n" + curAmount;
    }
}
