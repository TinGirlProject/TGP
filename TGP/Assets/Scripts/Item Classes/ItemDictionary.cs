using UnityEngine;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour
{
    public List<string> itemKeys;
    public List<Item> itemValues;
    private static Dictionary<string, Item> itemRecipes;

	// Use this for initialization
	void Start()
    {
        itemRecipes = new Dictionary<string, Item>();

        if (itemKeys.Count != itemValues.Count)
        {
            Log.RED("The amount of keys and values do not match.");
        }
        else
        {
            for (int i = 0; i < itemKeys.Count; i++)
            {
                itemRecipes.Add(itemKeys[i], itemValues[i]);
            }
        }
	}

    public static Item GetItem(string item1, string item2, string item3, string item4)
    {
        Item newItem = null;
        string combined = item1 + "|" + item2;

        if (item3 != "")
            combined += ("|" + item3);
        if (item4 != "")
            combined += ("|" + item4);

        if (itemRecipes.ContainsKey(combined))
        {
            Item type;
            itemRecipes.TryGetValue(combined, out type);

            //if (type != null)
            //{
            //    newItem = GetItemFromType(type);
            //}
        }

        return newItem;
    }

    private static Item GetItemFromType(CombinedItems type)
    {
        Item newItem = null;
        switch (type)
        {
            case CombinedItems.SlingShot:
                newItem = new Item("SlingShot", "Shoot things", 1, 1, false);
                break;
            case CombinedItems.Bomb:
                newItem = new Item("Bomb", "Blow something up", 1, 1, false);
                break;
        }

        return newItem;
    }
}

public enum CombinedItems
{
    SlingShot,
    Bomb
}