using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public static string s_Item_Resources_Path = "Item Icons/";

    private bool _displayInventoryWindow = false;										                // Should we show the inventory window?
    private const int INVENTORY_WINDOW_ID = 1;											                // Unique window ID for inventory window.
    private Rect _inventoryWindowRect = new Rect(5, Screen.height - 101, Screen.width - 10, 96);	       // Inventory rect.

    private string _toolTip = "";

    void Update()
    {
        // Display or hide the inventory window.
        if (Input.GetKeyUp(KeyCode.I))
        {
            _displayInventoryWindow = !_displayInventoryWindow;
        }
    }

    void OnGUI()
    {
        Event cur = Event.current;

        // Draw the inventory window.
        if (_displayInventoryWindow)
        {
            _inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");
        }

        DisplayTooltip();
    }

    private void InventoryWindow(int id)
    {
        Event cur = Event.current;

        for (int cnt = 0; cnt < PlayerInventory.Inventory.Count; cnt++)
        {
            Item item = PlayerInventory.Inventory[cnt];
            GUI.Box(new Rect(10 + 69 * cnt, 24, 64, 64), new GUIContent(item.icon, item.description));
        }
        SetToolTip();
    }

    private void SetToolTip()
    {
        if (Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
        {
            if (_toolTip != "")					// Mouse out event.
                _toolTip = "";

            if (GUI.tooltip != "")				// Mouse in event.
                _toolTip = GUI.tooltip;
        }
    }

    private void DisplayTooltip()
    {
        if (_toolTip != "")
        {
            float x = 5;
            float y = Screen.height - (223 + 5);
            int width = 202;
            int height = 50;

            //			if (Input.mousePosition.x + width < Screen.width)
            //				x = Input.mousePosition.x;
            //			else if (Input.mousePosition.x + width > Screen.width)
            //				x = Screen.width - width;
            //
            //			if (Screen.height - Input.mousePosition.y + height < Screen.height && Input.mousePosition.y > 0)
            //				y = Screen.height - Input.mousePosition.y;
            //			//else if (Screen.height - Input.mousePosition.y + height < Screen.height)
            //				//y = 

            GUI.Box(new Rect(x, y, width, height), _toolTip);
        }
    }
}