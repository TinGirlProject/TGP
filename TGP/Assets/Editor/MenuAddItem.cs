using UnityEngine;
using UnityEditor;

public class MenuAddItem
{
    static int count = 0;

    [MenuItem("Tin Girl Project/Item/Create Item")]
    public static void CreateItem()
    {
        Item item = ScriptableObject.CreateInstance<Item>();
        AssetDatabase.CreateAsset(item, "Assets/Game Items/Please Name Me" + (count > 0 ? "" : count.ToString()) + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }
}