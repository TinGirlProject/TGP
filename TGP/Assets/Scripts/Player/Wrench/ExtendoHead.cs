using UnityEngine;
using System.Collections;

public class ExtendoHead : WrenchHead
{
    protected override void Start()
    {
        base.Start();

        Messenger.AddListener(PlayerInput.s_InputStrings[(int)msg], this.CheckUseCondition);
    }

    public override void Activate()
    {
        Log.YELLOW(_name + ": " + _description);
    }

    public void CheckUseCondition()
    {
        // Check to make sure wrench should be used
        if (_wrench)
        {
            if (_wrench.head)
            {
                if (_wrench.head is ExtendoHead)
                    Messenger.Broadcast(Wrench.USEHEAD);
            }
        }
    }
}
