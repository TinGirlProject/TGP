using UnityEngine;
using System.Collections;

public class Log
{
    /// <summary>
    /// Basic Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void LOG(object text)
    {
        Debug.Log(text);
    }
    /// <summary>
    /// Bold text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void BOLD(object text)
    {
        Debug.Log("<b>" + text + "</b>");
    }
    /// <summary>
    /// Bold colour text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    public static void BOLD(object text, string color)
    {
        Debug.Log("<b>" + "<color=" + color +  ">" + text + "</color>" + "</b>");
    }
    /// <summary>
    /// Italic text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void ITALIC(object text)
    {
        Debug.Log("<i>" + text + "</i>");
    }
    /// <summary>
    /// Italic colour text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    public static void ITALIC(object text, string color)
    {
        Debug.Log("<i>" + "<color=" + color + ">" + text + "</color>" + "</i>");
    }
    /// <summary>
    /// Bold and italic text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void BOLDITALIC(object text)
    {
        Debug.Log("<b>" + "<i>" + text + "</i>" + "</b>");
    }
    /// <summary>
    /// Bold and italic colour text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    public static void BOLDITALIC(object text, string color)
    {
        Debug.Log("<b>" + "<i>" + "<color=" + color + ">" + text + "</color>" + "</i>" + "</b>");
    }
    /// <summary>
    /// Red text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void RED(object text)
    {
        Debug.Log("<color=red>" + text + "</color>");
    }
    /// <summary>
    /// Blue text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void BLUE(object text)
    {
        Debug.Log("<color=blue>" + text + "</color>");
    }
    /// <summary>
    /// Green text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void GREEN(object text)
    {
        Debug.Log("<color=green>" + text + "</color>");
    }
    /// <summary>
    /// Yellow text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void YELLOW(object text)
    {
        Debug.Log("<color=yellow>" + text + "</color>");
    }
    /// <summary>
    /// Orange text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    public static void ORANGE(object text)
    {
        Debug.Log("<color=orange>" + text + "</color>");
    }
    /// <summary>
    /// Custom colour text Debug.Log.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    public static void CUSTOM(object text, string color)
    {
        Debug.Log("<color=" + color + ">" + text + "</color>");
    }
    /// <summary>
    /// Returns a coloured rich text string.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    /// <returns></returns>
    public static string STRING(object text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }
    /// <summary>
    /// Returns a coloured and bolded rich text string.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    /// <returns></returns>
    public static string BOLDSTRING(object text, string color)
    {
        return "<b>" + "<color=" + color + ">" + text + "</color>" + "</b>";
    }
    /// <summary>
    /// Returns a coloured and italicised rich text string.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    /// <returns></returns>
    public static string ITALICSTRING(object text, string color)
    {
        return "<i>" + "<color=" + color + ">" + text + "</color>" + "</i>";
    }
    /// <summary>
    /// Returns a coloured, bolded and italicised rich text string.
    /// </summary>
    /// <param name="text">Not restricted to a string</param>
    /// <param name="color">Found in HtmlColours.cs</param>
    /// <returns></returns>
    public static string BOLDITALICSTRING(object text, string color)
    {
        return "<b>" + "<i>" + "<color=" + color + ">" + text + "</color>" + "</i>" + "</b>";
    }
}