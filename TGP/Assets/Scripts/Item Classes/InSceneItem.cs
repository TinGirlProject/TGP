using UnityEngine;
using System.Collections;

[AddComponentMenu("TGP/Items/InSceneItem")]
public class InSceneItem : MonoBehaviour
{
    public Item item;
    private Item itemToGive;
    private bool _inRange;

	// Use this for initialization
	void Start()
	{
        _inRange = false;

        itemToGive = ScriptableObject.CreateInstance<Item>();
        itemToGive.CopyItem(item);

        itemToGive.curAmount = 0;
        if (itemToGive.valueAmount == 0)
            itemToGive.valueAmount = Random.Range(itemToGive.minValueAmount, itemToGive.maxValueAmount);
        else
            itemToGive.valueAmount = 1;
        
        //Physics.IgnoreLayerCollision(
	}

	// Update is called once per frame
	void Update()
	{
        if (_inRange && Input.GetKeyUp(KeyCode.E))
        {
            if (PlayerInventory.AddItem(itemToGive))
            {
                Messenger<bool>.Broadcast("ShowEPress", false);
                Destroy(this.gameObject);
            }
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