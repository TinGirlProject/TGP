/// <summary>
/// Player GUI.cs
/// 
/// @Galen Manuel
/// @Mar. 1st, 2014
/// 
/// This class will control all the GUI in reagards to the player.
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerGUIOLD : MonoBehaviour 
{
	public GUISkin playerSkin;
	
	private bool _showBox;
	private float _offset = 10;
	private float _slotWidth = 32;
	private float _slotHeight = 32;
	private float _itemCountWidth = 10;
	private float _itemCountHeight = 10;
	private string _toolTip = "";

	/**************************************************/
	/*			Inventory Window Variables			  */
	/**************************************************/
	private bool _displayInventoryWindow = false;										// Should we show the inventory window?
	private const int INVENTORY_WINDOW_ID = 1;											// Unique window ID for inventory window.
	private Rect _inventoryWindowRect = new Rect(10, Screen.height - 178, 202, 173);	// Inventory rect.
	private int _inventoryCols = 6;														// Default inventory window columns.
	private int _inventoryRows = 4;														// Default inventory window rows.

	private float _doubleClickTimer = 0;												// Timer to determine time between clicks.
	private const float DOUBLE_CLICK_TIMER_THRESHHOLD = 0.5f;							// Time between clicks for it to be considered a double click.
	private Item _selectedItem;															// What item is selected by the player.
	private Item _clickedItem;															// What item is clicked by the player.

	private Rect[] _slotRects = new Rect[6 * 4];
	private Rect[] _itemCountRects = new Rect[6 * 4];

	// Use this for initialization
	void Start () 
	{
		// Count for slotRects array initialization loop.
		int cnt = 0;
		// Initialize the array holding all possible inventory rects.
		// This will be used for mouse position detection over the inventory slots.
		for (int y = 0; y < _inventoryRows; y++)
		{
			for (int x = 0; x < _inventoryCols; x++)
			{
				_slotRects[cnt] = new Rect((_offset * 0.5f) + (x * _slotWidth), 20 +  (y * _slotHeight), _slotWidth, _slotHeight);
				cnt++;
			}
		}

		// Count for itemCountRects array initialization loop.
		cnt = 0;
		// Initialize the array holding all possible inventory rects.
		// This will be used for mouse position detection over the inventory slots.
		for (int y = 0; y < _inventoryRows; y++)
		{
			for (int x = 0; x < _inventoryCols; x++)
			{
				_itemCountRects[cnt] = new Rect((_offset * 0.5f) + ((x + _slotWidth - _itemCountWidth) * _slotWidth), 20 +  ((y + _slotHeight - _itemCountHeight) * _slotHeight), _itemCountWidth, _itemCountHeight);
				cnt++;
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		// Create a hatchet for testing.
		if (Input.GetKeyDown(KeyCode.P))
		{
            Item testItem = new Item("Hatchet", "This is a test item", 1, 1, false);

            testItem.icon = Resources.Load("Item Icons/Weapons/" + testItem.name) as Texture2D;

            PlayerInventoryOLD.AddItem(testItem);
		}
		// Increase inventory for testing.
		if (Input.GetKeyDown (KeyCode.O)) 
		{
			if (PlayerInventoryOLD.IncreaseCurInventorySlots())
			{
				Debug.Log ("Sweet");
			}
		}
		// Display or hide the inventory window.
		if (Input.GetKeyUp(KeyCode.I))
		{
			_displayInventoryWindow = !_displayInventoryWindow;
		}

		if (Input.GetKeyUp(KeyCode.J))
		{

		}
	}

	void OnGUI()
	{
		// Register the current event so that we don't have to keep calling it.
		Event curEvent = Event.current;

		GUI.skin = playerSkin;

		if (_displayInventoryWindow)
		{
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");
		}

		if (_selectedItem != null)
		{
			GUI.Box(new Rect(Input.mousePosition.x - 16, Screen.height - Input.mousePosition.y - 16, 32, 32), new GUIContent(_selectedItem.icon), "Inventory Item");

			if (_inventoryWindowRect.Contains(curEvent.mousePosition))
			{
				Debug.Log("Mouse over inventory window.");
				if (curEvent.type == EventType.MouseUp)
				{
					if (PlayerInventoryOLD.AddItem(_selectedItem))
					{
						_selectedItem = null;
					}
				}
			}
			else
			{
				if (curEvent.type == EventType.MouseUp)
				{
                    if (!_selectedItem.keyItem)
                    {
                        Debug.Log("\"" + _selectedItem.name + "\" destroyed");
                        _selectedItem = null;
                    }
                    else
                    {
                        Debug.Log("\"" + _selectedItem.name + "\" CAN'T be destroyed, returning to inventory.");
                        if (PlayerInventoryOLD.AddItem(_selectedItem))
                        {
                            _selectedItem = null;
                        }
                    }
				}
			}
		}

		DisplayTooltip();
	}

	private void InventoryWindow(int id)
	{
		Event curEvent = Event.current;

		int cnt = 0;

		for (int y = 0; y < _inventoryRows; y++)
		{
			for (int x = 0; x < _inventoryCols; x++)
			{
                if (cnt < PlayerInventoryOLD.Inventory.Count)
                {
                    GUI.Box(_slotRects[cnt], new GUIContent(PlayerInventoryOLD.Inventory[cnt].icon, PlayerInventoryOLD.Inventory[cnt].ToolTip()), "Inventory Item");

                    if (PlayerInventoryOLD.Inventory[cnt].curAmount > 1)
                    {
                        GUI.Box(_itemCountRects[cnt], PlayerInventoryOLD.Inventory[cnt].curAmount.ToString());
                    }

                    if (_slotRects[cnt].Contains(curEvent.mousePosition))
                    {
                        if (curEvent.type == EventType.MouseDrag && _selectedItem == null)
                        {
                            _selectedItem = PlayerInventoryOLD.Inventory[cnt];
                            if (!PlayerInventoryOLD.RemoveItem(_selectedItem))
                            {
                                Debug.LogWarning("Could not remove item.");
                            }
                        }
                    }

                    //					if (_selectedItem == PlayerInventoryOLD.Inventory[cnt])
                    //					{
                    //						GUI.Box (new Rect(Input.mousePosition.x - _slotWidth * 0.5f, (Screen.height - Input.mousePosition.y) + _slotHeight * 0.5f, _slotWidth, _slotHeight), 
                    //						         new GUIContent(PlayerInventoryOLD.Inventory[cnt].icon), "Inventory Item");
                    //					}
                    //					else
                    //					{
                    //						//if(GUI.Button(new Rect((_offset * 0.5f) + (x * _slotWidth), 20 +  (y * _slotHeight), _slotWidth, _slotHeight), 
                    //						//              new GUIContent(PlayerInventoryOLD.Inventory[cnt].icon, PlayerInventoryOLD.Inventory[cnt].Description), "Inventory Item"))
                    //						//{
                    //						//	Debug.Log ("Clicked: " + PlayerInventoryOLD.Inventory[cnt].name);
                    //							//if (_selectedItem == null)
                    //							//{
                    //							//	_selectedItem = PlayerInventoryOLD.Inventory[cnt];
                    //							//}
                    //							//						if(_doubleClickTimer != 0 && _selectedItem != null)
                    //							//						{
                    //							//							if(Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHHOLD)
                    //							//							{
                    //							//								Debug.Log ("Double Clicked: " + PlayerCharacter.Inventory[cnt].name);
                    //							//								
                    //							//								if(PlayerCharacter.EquipedWeapon == null)
                    //							//								{
                    //							//									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
                    //							//									PlayerCharacter.Inventory.RemoveAt(cnt);
                    //							//								}
                    //							//								else
                    //							//								{
                    //							//									Item temp = PlayerCharacter.EquipedWeapon;
                    //							//									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
                    //							//									PlayerCharacter.Inventory[cnt] = temp;
                    //							//								}
                    //							//								
                    //							//								_doubleClickTimer = 0;
                    //							//								_selectedItem = null;
                    //							//							}
                    //							//							else
                    //							//							{
                    //							//								Debug.Log ("Reset the double click timer");
                    //							//								_doubleClickTimer = Time.time;
                    //							//							}
                    //							//						}
                    //							//						else
                    //							//						{
                    //							//							_doubleClickTimer = Time.time;
                    //							//							_selectedItem = PlayerCharacter.Inventory[cnt];
                    //							//						}
                    //						//}
                    //					}
                }
                else if (cnt < PlayerInventoryOLD.CurInventorySlots)
                {
                    GUI.Box(_slotRects[cnt], ""/*(x + y * _inventoryCols).ToString()*/, "Empty Slot");
                }

                cnt++;
              }
		}

		SetToolTip();

		GUI.DragWindow();
	}

	private void SetToolTip()
	{
		if (Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
		{
			if(_toolTip != "")					// Mouse out event.
				_toolTip = "";
			
			if(GUI.tooltip != "")				// Mouse in event.
				_toolTip = GUI.tooltip;
		}
	}

	private void DisplayTooltip()
	{
		if(_toolTip != "")
		{
			float x = _offset;
			float y = Screen.height - (223 + _offset);
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
