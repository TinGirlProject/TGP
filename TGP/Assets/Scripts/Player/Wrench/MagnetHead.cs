using UnityEngine;
using System.Collections;

public class MagnetHead : WrenchHead
{
    public override void Activate()
    {
        Log.ORANGE(_name + ": " + _description);
    }
}
