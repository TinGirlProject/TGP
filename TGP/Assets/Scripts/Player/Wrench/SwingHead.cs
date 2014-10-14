using UnityEngine;
using System.Collections;

public class SwingHead : WrenchHead 
{
    public override void Activate()
    {
        Log.GREEN("Swing");
    }

    public void RenderRange()
    {
        Log.BLUE("Swing Render Range");
    }
}
