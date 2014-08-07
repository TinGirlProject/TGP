using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private static List<Item> s_inventory = new List<Item>();
    private static int s_inventorySlots;

    void Start()
    {
        s_inventory = new List<Item>();
    }

    public static List<Item> Inventory
    {
        get { return s_inventory; }
    }

    public static int InventorySlots
    {
        get { return s_inventorySlots; }
    }
}