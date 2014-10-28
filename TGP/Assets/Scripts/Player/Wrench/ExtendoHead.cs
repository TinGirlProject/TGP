using UnityEngine;
using System.Collections;

public class ExtendoHead : WrenchHead
{
    void Awake()
    {
        Log.YELLOW("Awake?");
        Messenger.AddListener(PlayerInput.LEFTMOUSEUP, CheckUseCondition);
    }

    public override void Activate()
    {
        Log.YELLOW(_name + ": " + _description);
    }

    public void CheckUseCondition()
    {
        // Check to make sure wrench should be used

        Messenger.Broadcast("UseHead");
    }
}
