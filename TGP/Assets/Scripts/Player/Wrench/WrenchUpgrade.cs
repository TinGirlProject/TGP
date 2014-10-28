using UnityEngine;
using System.Collections;

/// <summary>
/// This class is the base class for all the wrench upgrades available in the game.
/// </summary>
public class WrenchUpgrade : MonoBehaviour
{
    /// <summary>
    /// The name of this wrench upgrade.
    /// </summary>
    private string _name;
    /// <summary>
    /// The description of this wrench upgrade.
    /// </summary>
    private string _description;
    /// <summary>
    /// The current level of this wrench upgrade.
    /// </summary>
    private uint _level;
    /// <summary>
    /// The current amount of UpgradePoints(UP) that this wrench upgrade has.
    /// </summary>
    private uint _curUP;
    /// <summary>
    /// The max UP for the current level of this wrench upgrade.
    /// </summary>
    private uint _maxUP;

    /// <summary>
    /// Initialise this class.
    /// <para>Defaults: level = 1, curUP = 0</para>
    /// </summary>
    /// <param name="name">What this upgrades name is.</param>
    /// <param name="description">What this upgrades description is.</param>
    public void Init(string name, string description)
    {
        _name = name;
        _description = description;
        _level = 1;
        _curUP = 0;
        // Determine _maxUP via algorithm
        // _maxUP = _level * ...
    }

    protected virtual void LevelUp()
    {
        Log.GREEN(name + " LEVELED UP!");
    }

    /// <summary>
    /// This method will add the modification and level up recursively.
    /// </summary>
    /// <param name="mod">How much UP was gained.</param>
    public void ModifyCurUP(uint mod)
    {
        _curUP += mod;

        if (_curUP >= _maxUP)
        {
            uint dif = _curUP - _maxUP;
            LevelUp();
            _curUP = 0;

            if (dif > 0)
                ModifyCurUP(dif);
        }
    }
    #region Setters and Getters
    public string Name
    {
        get { return _name; }
    }

    public string Description
    {
        get { return _description; }
    }

    public uint Level
    {
        get { return _level; }
    }

    public uint CurUP
    {
        get { return _curUP; }
    }

    public uint MaxUP
    {
        get { return _maxUP; }
    }
    #endregion
}
