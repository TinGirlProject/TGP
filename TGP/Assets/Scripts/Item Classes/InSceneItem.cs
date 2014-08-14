﻿using UnityEngine;
using System.Collections;

public class InSceneItem : MonoBehaviour
{
    public Item item;

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            if (item)
            {
                Item newItem = ScriptableObject.CreateInstance<Item>();
                newItem.CopyItem(item);
                PlayerInventory.Inventory.Add(newItem);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("There is no Item attached.");
            }
        }
    }
}