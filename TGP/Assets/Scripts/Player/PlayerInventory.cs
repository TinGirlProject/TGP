using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private static List<Item> s_inventory = new List<Item>();
    private static int s_curInventorySlots;
    private static int s_maxInventorySlots;

    private static bool s_hasSlingshot;

    void Start()
    {
        s_curInventorySlots = 10;
        s_maxInventorySlots = 30;
        s_inventory = new List<Item>(s_curInventorySlots);

        s_hasSlingshot = false;
    }

    public static bool AddItem(Item itemToAdd)
    {
        // Check if the player has any items already
        if (s_inventory.Count > 0)
        {
            // Loop through the entire inventory
            for (int cnt = 0; cnt < s_inventory.Count; cnt++)
            {
                // If the item being added exists in inventory already
                if (itemToAdd.name == s_inventory[cnt].name)
                {
                    // Item is stackable
                    if (s_inventory[cnt].maxAmount > 1)
                    {
                        // If the player has less than the max amount, add item to inventory
                        if ((s_inventory[cnt].curAmount + itemToAdd.valueAmount) < s_inventory[cnt].maxAmount)
                        {
                            s_inventory[cnt].curAmount += itemToAdd.valueAmount;

                            /*
                             * Quest item stuff - if including 
                             */
                            return true;
                        }
                        else
                        {
                            int difference = (s_inventory[cnt].curAmount + itemToAdd.valueAmount) - itemToAdd.maxAmount;
                            s_inventory[cnt].curAmount = itemToAdd.maxAmount;
                            itemToAdd.valueAmount = difference;
                            //s_inventory.Add(itemToAdd);
                            return false;
                        }
                    }
                    // Non stackable
                    else
                    {
                        // If player has room
                        if (s_inventory.Count < s_curInventorySlots)
                        {
                            s_inventory.Add(itemToAdd);
                            itemToAdd.curAmount += itemToAdd.valueAmount;
                            Log.BLUE(s_inventory[cnt].name + " added. Now have " + s_inventory[cnt].curAmount);

                            /*
                             * Quest item stuff - if including 
                             */

                            return true;
                        }
                    }
                }
            }

            // New Item, add if player has room.
            if (s_inventory.Count < s_curInventorySlots)
            {
                if (itemToAdd.name == "Slingshot")
                {
                    if (!s_hasSlingshot)
                    {
                        s_hasSlingshot = true;
                        s_inventory.Add(itemToAdd);
                        itemToAdd.curAmount += itemToAdd.valueAmount;
                    }
                }
                else
                {
                    s_inventory.Add(itemToAdd);
                    itemToAdd.curAmount += itemToAdd.valueAmount;
                }

                /*
                 * Quest item stuff - if including 
                 */

                return true;
            }
        }
        // If not, add the item
        else
        {
            if (itemToAdd.name == "Slingshot")
            {
                if (!s_hasSlingshot)
                {
                    s_hasSlingshot = true;
                    s_inventory.Add(itemToAdd);
                    itemToAdd.curAmount += itemToAdd.valueAmount;
                }
            }
            else
            {
                s_inventory.Add(itemToAdd);
                itemToAdd.curAmount += itemToAdd.valueAmount;
            }
            /*
             * Quest item stuff - if including 
             */
            return true;
        }
        return false;
    }

    public static bool RemoveItem(Item itemToRemove)
    {
        // Loop through the entire inventory
        for (int cnt = 0; cnt < s_inventory.Count; cnt++)
        {
            // If the item exists in inventory
            if (itemToRemove.name == s_inventory[cnt].name)
            {
                // If player has one or more than one of the item, remove one of the item
                if (s_inventory[cnt].curAmount > 1)
                {
                    s_inventory[cnt].curAmount--;
                    Debug.Log("\"" + itemToRemove.name + "\" removed");
                    return true;
                }
                // Otherwise, remove the item completely
                else
                {
                    if (s_inventory[cnt].name == "Slingshot")
                    {
                        s_hasSlingshot = false;
                    }
                    
                    s_inventory.RemoveAt(cnt);
                    return true;
                }
            }
        }
        // Player could not remove item
        Debug.Log("\"" + itemToRemove.name + "\" NOT removed");
        return false;
    }

    public static bool IncreaseCurInventorySlots()
    {
        // Check if adding a new inventory slot would be more than the max slots allowed.
        if (s_curInventorySlots + 1 > s_maxInventorySlots)
        {
            return false;
        }

        // If all good, add inventory slot.
        s_curInventorySlots++;
        return true;
    }

    private static Item GetItemByName(string name)
    {
        foreach (Item item in s_inventory)
        {
            if (item.name == name)
                return item;
        }

        return null;
    }

    #region Setters and Getters
    public static List<Item> Inventory
    {
        get { return s_inventory; }
    }

    public static int InventorySlots
    {
        get { return s_curInventorySlots; }
    }

    public static bool HasSlingshot
    {
        get { return s_hasSlingshot; }
    }

    //public static bool HasAmmo
    //{
    //    get 
    //    {
    //        Item ammo = GetItemByName("Pebbles");
    //        if (ammo)
    //            return ammo.curAmount > 0;
    //        else
    //            return false;
    //    }
    //}

    public static bool CanFireSlingshot
    {
        get
        {
            Item ammo = GetItemByName("Pebbles");
            if (ammo)
                return ammo.curAmount > 0 && s_hasSlingshot;
            else
                return false;
        }
    }
    #endregion
}