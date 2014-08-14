using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public static string s_Item_Resources_Path = "Item Icons/";

    private bool _displayInventoryWindow = false;										                // Should we show the inventory window?
    private const int INVENTORY_WINDOW_ID = 1;											                // Unique window ID for inventory window.
    private Rect _inventoryWindowRect = new Rect(5, Screen.height - 101, Screen.width - 10, 96);	       // Inventory rect.

    private string _toolTip = "";

    private Item _firstSelectedItem;
    private Item _secondSelectedItem;

    void Update()
    {
        // Display or hide the inventory window.
        if (Input.GetKeyUp(KeyCode.I))
        {
            _displayInventoryWindow = !_displayInventoryWindow;
        }

        //Log.YELLOW(_firstSelectedItem.name);
        //Log.ORANGE(_secondSelectedItem.name);
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

        switch (cur.type)
        {
            case EventType.MouseUp:
                for (int cnt = 0; cnt < PlayerInventory.Inventory.Count; cnt++)
                {
                    Rect rect = new Rect(10 + 69 * cnt, 24, 64, 64);
                    Item item = PlayerInventory.Inventory[cnt];

                    if (rect.Contains(cur.mousePosition))
                    {
                        if (!_firstSelectedItem)
                        {
                            _firstSelectedItem = item;
                            item.guiSelected = true;
                        }
                        else
                        {
                            if (_firstSelectedItem != item)
                            {
                                if (_secondSelectedItem)
                                    _secondSelectedItem.guiSelected = false;
                                _secondSelectedItem = item;
                                item.guiSelected = true;
                            }
                        }

                        if (_firstSelectedItem == item)
                        {
                            if (_secondSelectedItem)
                            {
                                _firstSelectedItem = _secondSelectedItem;
                                _secondSelectedItem.guiSelected = false;
                                _secondSelectedItem = null;
                            }
                        }
                        if (_secondSelectedItem == item)
                        {
                            _secondSelectedItem.guiSelected = false;
                            _secondSelectedItem = null;
                        }
                    }
                }
                break;
            default:
                break;
        }

        for (int cnt = 0; cnt < PlayerInventory.Inventory.Count; cnt++)
        {
            Item item = PlayerInventory.Inventory[cnt];
            Rect rect = new Rect(10 + 69 * cnt, 24, 64, 64);
            GUI.Box(rect, new GUIContent(item.icon, item.description));

            if (rect.Contains(cur.mousePosition))
            {
                if (_firstSelectedItem != item && _secondSelectedItem != item)
                    DrawOutline(rect, Color.yellow);
            }

            if (item.guiSelected)
                DrawOutline(rect, Color.red);
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

    private void DrawOutline(Rect rect, Color color)
    {
        Handles.color = color;
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMin, 0), new Vector3(rect.xMax, rect.yMin, 0));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMin, 0), new Vector3(rect.xMax, rect.yMax, 0));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMax, 0), new Vector3(rect.xMin, rect.yMax, 0));
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMax, 0), new Vector3(rect.xMin, rect.yMin, 0));
    }
}