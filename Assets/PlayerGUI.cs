/// <summary>
/// Player GUI.cs
/// Written By Galen Manuel
/// Last modified January 21st, 2014.
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

	// Use this for initialization
	void Start () 
	{
		//_inventoryRows = PlayerInventory.MaxInventorySlots / _inventoryCols;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			Item testItem = new Item("Hatchet", "This is a test item", 1, 1);
			
			testItem.Icon = Resources.Load("Item Icons/Weapons/" + testItem.Name) as Texture2D;
			
			if (PlayerInventory.AddItem(testItem))
				Debug.Log("Item Added");
			else
				Debug.Log("Item NOT Added");
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
	}

	private void InventoryWindow(int id)
	{
		int cnt = 0;

		for (int y = 0; y < _inventoryRows; y++)
		{
			for (int x = 0; x < _inventoryCols; x++)
			{
				if(cnt < PlayerInventory.Inventory.Count)
				{
					if(GUI.Button(new Rect((_offset * 0.5f) + (x * _slotWidth), 20 +  (y * _slotHeight), _slotWidth, _slotHeight), 
					              new GUIContent(PlayerInventory.Inventory[cnt].MaxAmount > 1 ? PlayerInventory.Inventory[cnt].CurAmount.ToString() : "", 
					               PlayerInventory.Inventory[cnt].Icon), "Empty Slot"))
					{
//						if(_doubleClickTimer != 0 && _selectedItem != null)
//						{
//							if(Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHHOLD)
//							{
//								Debug.Log ("Double Clicked: " + PlayerCharacter.Inventory[cnt].Name);
//								
//								if(PlayerCharacter.EquipedWeapon == null)
//								{
//									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
//									PlayerCharacter.Inventory.RemoveAt(cnt);
//								}
//								else
//								{
//									Item temp = PlayerCharacter.EquipedWeapon;
//									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
//									PlayerCharacter.Inventory[cnt] = temp;
//								}
//								
//								_doubleClickTimer = 0;
//								_selectedItem = null;
//							}
//							else
//							{
//								Debug.Log ("Reset the double click timer");
//								_doubleClickTimer = Time.time;
//							}
//						}
//						else
//						{
//							_doubleClickTimer = Time.time;
//							_selectedItem = PlayerCharacter.Inventory[cnt];
//						}
					}
				}
				else
				{
					GUI.Box(new Rect((_offset * 0.5f) + (x * _slotWidth), 20 +  (y * _slotHeight), _slotWidth, _slotHeight), (x + y * _inventoryCols).ToString(), "Empty Slot");
				}
				
				cnt++;
			}
		}
	}
}
