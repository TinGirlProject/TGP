/// <summary>
/// Player GUI.cs
/// Written By Galen Manuel
/// Last modified January 22nd, 2014.
/// 
/// This class will control all the GUI in reagards to the player.
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour 
{
	public GUISkin playerSkin;
	
	private bool _showBox;
	private float _offset = 10;
	private float _slotWidth = 32;
	private float _slotHeight = 32;
	private string _toolTip = "";

	/**************************************************/
	/*			Inventory Window Variables			  */
	/**************************************************/
	private bool _displayInventoryWindow = false;										// Should we show the inventory window?
	private const int INVENTORY_WINDOW_ID = 1;											// Unique window ID for inventory window.
	private Rect _inventoryWindowRect = new Rect(10, Screen.height - 163, 202, 153);	// Inventory rect.
	private int _inventoryCols = 6;														// Default inventory window columns.
	private int _inventoryRows = 4;														// Default inventory window rows.

	private float _doubleClickTimer = 0;												// Timer to determine time between clicks.
	private const float DOUBLE_CLICK_TIMER_THRESHHOLD = 0.5f;							// Time between clicks for it to be considered a double click.
	private Item _selectedItem;															// What item is selected by the player.

	private Rect[] _slotRects = new Rect[6 * 4];

	// Use this for initialization
	void Start () 
	{
		int cnt = 0;
		//_inventoryRows = PlayerInventory.MaxInventorySlots / _inventoryCols;
		for (int y = 0; y < _inventoryRows; y++)
		{
			for (int x = 0; x < _inventoryCols; x++)
			{
				_slotRects[cnt] = new Rect((_offset * 0.5f) + (x * _slotWidth), 20 +  (y * _slotHeight), _slotWidth, _slotHeight);

				cnt++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			Item testItem = new Item("Hatchet", "This is a test item", 1, 1);
			
			testItem.Icon = Resources.Load("Item Icons/Weapons/" + testItem.Name) as Texture2D;
			
			PlayerInventory.AddItem(testItem);
		}

		if (Input.GetKeyDown (KeyCode.O)) 
		{
			if (PlayerInventory.IncreaseCurInventorySlots())
			{
				Debug.Log ("Sweet");
			}
		}

		if (Input.GetKeyUp(KeyCode.I))
		{
			_displayInventoryWindow = !_displayInventoryWindow;
		}
	}

	void OnGUI()
	{
		GUI.skin = playerSkin;

		if (_displayInventoryWindow)
		{
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");
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
				if(cnt < PlayerInventory.Inventory.Count)
				{
					GUI.Box(_slotRects[cnt], new GUIContent(PlayerInventory.Inventory[cnt].Icon, PlayerInventory.Inventory[cnt].Description), "Inventory Item");

					if (_slotRects[cnt].Contains(curEvent.mousePosition))
					{
						if (curEvent.type == EventType.MouseDown)
						{
							Debug.Log ("Box " + cnt + " clicked.");
							_selectedItem = PlayerInventory.Inventory[cnt];

							GUI.Box(new Rect(Input.mousePosition.x - 16, Screen.height - Input.mousePosition.y + 16, 32, 32), new GUIContent(PlayerInventory.Inventory[cnt].Icon, "Inventory Item"));
						}
					}

//					if (_selectedItem == PlayerInventory.Inventory[cnt])
//					{
//						GUI.Box (new Rect(Input.mousePosition.x - _slotWidth * 0.5f, (Screen.height - Input.mousePosition.y) + _slotHeight * 0.5f, _slotWidth, _slotHeight), 
//						         new GUIContent(PlayerInventory.Inventory[cnt].Icon), "Inventory Item");
//					}
//					else
//					{
//						//if(GUI.Button(new Rect((_offset * 0.5f) + (x * _slotWidth), 20 +  (y * _slotHeight), _slotWidth, _slotHeight), 
//						//              new GUIContent(PlayerInventory.Inventory[cnt].Icon, PlayerInventory.Inventory[cnt].Description), "Inventory Item"))
//						//{
//						//	Debug.Log ("Clicked: " + PlayerInventory.Inventory[cnt].Name);
//							//if (_selectedItem == null)
//							//{
//							//	_selectedItem = PlayerInventory.Inventory[cnt];
//							//}
//							//						if(_doubleClickTimer != 0 && _selectedItem != null)
//							//						{
//							//							if(Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHHOLD)
//							//							{
//							//								Debug.Log ("Double Clicked: " + PlayerCharacter.Inventory[cnt].Name);
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
				else if (cnt < PlayerInventory.CurInventorySlots)
				{
					GUI.Box(_slotRects[cnt], ""/*(x + y * _inventoryCols).ToString()*/, "Empty Slot");
				}
				
				cnt++;
			}
		}

		SetToolTip();
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
			float y = Screen.height - (213 + _offset);
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
