using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagnetHead : WrenchHead
{
    private bool _activated;
    private List<MagneticItem> _inRangeObjects;

    protected override void Start()
    {
        base.Start();

        _activated = false;
        _inRangeObjects = new List<MagneticItem>();
        Messenger.AddListener(PlayerInput.s_InputStrings[(int)msg], this.CheckUseCondition);
    }

    void Update()
    {
        if (_activated)
        {
            MagneticItem[] magObjects = GameObject.FindObjectsOfType<MagneticItem>();

            for (int i = 0; i < magObjects.Length; i++)
            {
                float dis = Vector3.Distance(magObjects[i].transform.position, _wrench.transform.position);

                if (dis < _range)
                {
                    if (!_inRangeObjects.Contains(magObjects[i]))
                        _inRangeObjects.Add(magObjects[i]);
                }
                else
                {
                    if (_inRangeObjects.Contains(magObjects[i]))
                    {
                        _inRangeObjects[_inRangeObjects.IndexOf(magObjects[i])].SendMessage("RemoveAttractionTarget", SendMessageOptions.RequireReceiver);
                        _inRangeObjects.Remove(magObjects[i]); 
                    }
                }
            }

            for (int i = 0; i < _inRangeObjects.Count; i++)
            {
                _inRangeObjects[i].SendMessage("SetAttractionTarget", this, SendMessageOptions.RequireReceiver);
            }

            Log.GREEN(_inRangeObjects.Count);
        }
    }

    public override void Activate()
    {
        _activated = !_activated;

        Log.YELLOW(_name + (_activated ? " activated." : " deactivated."));

        if (!_activated)
        {
            for (int i = 0; i < _inRangeObjects.Count; i++)
            {
                _inRangeObjects[i].SendMessage("RemoveAttractionTarget", SendMessageOptions.RequireReceiver);
            }
            _inRangeObjects.Clear();
        }
    }

    public void CheckUseCondition()
    {
        // Check to make sure wrench should be used
        if (_wrench)
        {
            if (_wrench.head)
            {
                if (_wrench.head is MagnetHead)
                    Messenger.Broadcast(Wrench.USEHEAD);
            }
        }
    }

    public void RemoveFromInRangeList(MagneticItem item)
    {
        if (_inRangeObjects.Contains(item))
            _inRangeObjects.Remove(item);
    }

    public bool Activated
    {
        get { return _activated; }
    }
}
