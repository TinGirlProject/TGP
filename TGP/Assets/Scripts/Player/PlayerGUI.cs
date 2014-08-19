/// <summary>
/// 
/// </summary>
using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public static string s_Item_Resources_Path = "Item Icons/";

    private bool _displayInventoryWindow = false;										                // Should we show the inventory window?
    private const int INVENTORY_WINDOW_ID = 1;											                // Unique window ID for inventory window.
    private Rect _inventoryWindowRect = new Rect(5, Screen.height - 101, Screen.width - 10, 96);	    // Inventory rect.

    private string _toolTip = "";

    // Combining Items
    private Item _firstSelectedItem;
    private Item _secondSelectedItem;
    // Dragging and Dropping Items
    //private Item _hoverItem;
    //private GameObject _hoverGameObject;
    //private bool _itemInstantiated;
    // Notifications
    private bool _showCombineNotification = false;
    private string _showCombineGUIString = "Press 'E' to try and combine items.";
    private GUIStyle _showCombineStyle = new GUIStyle();
    private Timer _showCombineTimer;

    // Display 'E' Press
    private bool _showEPress;

    void OnEnable()
    {
        Messenger<bool>.AddListener("ShowEPress", ShowEPress);
    }

    void OnDisable()
    {
        Messenger<bool>.RemoveListener("ShowEPress", ShowEPress);
    }

    void Start()
    {
        _showCombineTimer = new Timer(3);
        GameManager.listOfTimers.Add(_showCombineTimer);

        _showCombineStyle.richText = true;
        _showCombineStyle.alignment = TextAnchor.MiddleLeft;
    }

    void Update()
    {
        #region Timer Updating
        if (_showCombineTimer.IsTimeComplete)
        {
            _showCombineTimer.ResetTimer();
            _showCombineNotification = false;
            _showCombineGUIString = "Press 'E' to try and combine items.";
        }
        #endregion

        // Display or hide the inventory window.
        if (Input.GetKeyUp(KeyCode.I))
        {
            _displayInventoryWindow = !_displayInventoryWindow;
        }

        

        if (_firstSelectedItem && _secondSelectedItem)
        {
            _showCombineNotification = true;
            if (Input.GetKeyUp(KeyCode.E))
            {
                Item newItem = ItemDictionary.GetItem(_firstSelectedItem.name, _secondSelectedItem.name);

                if (newItem)
                {
                    if (!PlayerInventory.AddItem(newItem))
                        Log.RED("Could not add " + newItem.name);
                    if (!PlayerInventory.RemoveItem(_firstSelectedItem))
                        Log.RED("Could not remove " + _firstSelectedItem.name);
                    else
                        _firstSelectedItem = null;
                    if (!PlayerInventory.RemoveItem(_secondSelectedItem))
                        Log.RED("Could not remove " + _secondSelectedItem.name);
                    else
                        _secondSelectedItem = null;
                    newItem.guiSelected = false;

                    _showCombineGUIString = Log.BOLDSTRING("SUCCESS! ", HtmlColors.GREEN);
                    _showCombineTimer.ResetTimer();
                    _showCombineTimer.StartTimer();
                }
                else
                {
                    _showCombineGUIString = Log.BOLDSTRING("FAILURE! " + _firstSelectedItem.name + " and " + _secondSelectedItem.name + " can't be combined.", HtmlColors.RED);
                    _showCombineTimer.ResetTimer();
                    _showCombineTimer.StartTimer();
                    _firstSelectedItem.guiSelected = false;
                    _firstSelectedItem = null;
                    _secondSelectedItem.guiSelected = false;
                    _secondSelectedItem = null;
                }
            }
        }
    }

    void OnGUI()
    {
        Event cur = Event.current;

        // Show the 'Press E' prompt
        if (_showEPress)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.20f, 80, 30), "Press E");
        }

        // Draw the inventory window.
        if (_displayInventoryWindow)
        {
            _inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");

            if (_showCombineNotification)
            {
                GUI.Box(new Rect(5, Screen.height - (131 + 5),
                    _showCombineStyle.CalcSize(new GUIContent(_showCombineGUIString)).x + 1, 30), _showCombineGUIString, _showCombineStyle);
                Utility.DrawOutline(new Rect(5, Screen.height - (131 + 5), 
                    _showCombineStyle.CalcSize(new GUIContent(_showCombineGUIString)).x + 1, 30), Color.black);
            }
        }
        else
        {
            if (_firstSelectedItem)
            {
                _firstSelectedItem.guiSelected = false;
                _firstSelectedItem = null;
            }

            if (_secondSelectedItem)
            {
                _secondSelectedItem.guiSelected = false;
                _secondSelectedItem = null;
            }

            _showCombineNotification = false;
        }

        DisplayTooltip();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
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
                        if (cur.button == 0)
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
                                    if (_secondSelectedItem == item)
                                    {
                                        _secondSelectedItem.guiSelected = false;
                                        _secondSelectedItem = null;
                                        _showCombineNotification = false;
                                    }
                                    else
                                    {
                                        if (_secondSelectedItem)
                                            _secondSelectedItem.guiSelected = false;
                                        _secondSelectedItem = item;
                                        _secondSelectedItem.guiSelected = true;
                                    }
                                }
                                else
                                {
                                    if (_secondSelectedItem)
                                    {
                                        _firstSelectedItem.guiSelected = false;
                                        _firstSelectedItem = null;
                                        _firstSelectedItem = _secondSelectedItem;
                                        _secondSelectedItem = null;
                                        _showCombineNotification = false;
                                    }
                                    else
                                    {
                                        _firstSelectedItem.guiSelected = false;
                                        _firstSelectedItem = null;
                                        _showCombineNotification = false;
                                    }
                                }
                            }
                        }
                        else if (cur.button == 1)
                        {
                            if (item == _firstSelectedItem)
                            {
                                if (_secondSelectedItem)
                                {
                                    _firstSelectedItem.guiSelected = false;
                                    _firstSelectedItem = null;
                                    _firstSelectedItem = _secondSelectedItem;
                                    _secondSelectedItem = null;
                                    _showCombineNotification = false;
                                }
                                else
                                {
                                    _firstSelectedItem.guiSelected = false;
                                    _firstSelectedItem = null;
                                    _showCombineNotification = false;
                                }
                            }
                            else if (item == _secondSelectedItem)
                            {
                                _secondSelectedItem.guiSelected = false;
                                _secondSelectedItem = null;
                                _showCombineNotification = false;
                            }

                            Instantiate(item.inSceneGameObject,
                                        transform.position + new Vector3(0, item.inSceneGameObject.transform.localScale.y * 1.5f, 0),
                                        item.inSceneGameObject.transform.rotation);
                            PlayerInventory.RemoveItem(item);
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
            if (item.curAmount > 1)
                GUI.Box(new Rect(rect.x + 64 - (_showCombineStyle.CalcSize(new GUIContent(item.curAmount.ToString())).x + 2), 
                                 rect.y + 64 - (_showCombineStyle.CalcSize(new GUIContent(item.curAmount.ToString())).y),
                                 _showCombineStyle.CalcSize(new GUIContent(item.curAmount.ToString())).x + 2,
                                 _showCombineStyle.CalcSize(new GUIContent(item.curAmount.ToString())).y), item.curAmount.ToString(), _showCombineStyle);

            if (rect.Contains(cur.mousePosition))
            {
                if (_firstSelectedItem != item && _secondSelectedItem != item)
                    Utility.DrawOutline(rect, Color.yellow);
            }

            if (item.guiSelected)
                Utility.DrawOutline(rect, Color.red);
        }
        SetToolTip();
    }

    private void ShowEPress(bool show)
    {
        _showEPress = show;
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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