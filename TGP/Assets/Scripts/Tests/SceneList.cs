using UnityEngine;
using System.Collections;

public class SceneList : MonoBehaviour 
{
    public string[] scenes;

    public int marginX, marginY;

    public int x, y;
    public int width, height;

    void OnGUI()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if (GUI.Button(new Rect(x + marginX * i, y + marginY * i, width, height), scenes[i]))
            {
                Application.LoadLevel(scenes[i]);
            }
        }
    }
}
