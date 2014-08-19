using UnityEngine;
using System.Collections;

public class InSceneItem : MonoBehaviour
{
    public Item item;
    private bool _inRange;

	// Use this for initialization
	void Start()
	{
        _inRange = false;
	}

	// Update is called once per frame
	void Update()
	{
        if (_inRange && Input.GetKeyUp(KeyCode.E))
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();
            newItem.CopyItem(item);
            PlayerInventory.AddItem(newItem);
            Messenger<bool>.Broadcast("ShowEPress", false);
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            if (item)
            {
                Messenger<bool>.Broadcast("ShowEPress", true);
                _inRange = true;
            }
            else
            {
                Debug.Log("There is no Item attached.");
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            Messenger<bool>.Broadcast("ShowEPress", false);
            _inRange = false;
        }
    }
}