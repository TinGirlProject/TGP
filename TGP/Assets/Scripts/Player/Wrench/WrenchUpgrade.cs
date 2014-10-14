using UnityEngine;
using System.Collections;

public class WrenchUpgrade : MonoBehaviour
{
    private string _name;
    private string _description;
    private uint _level;
    private uint _curUP;
    private uint _maxUP;

    public void Init(string name, string description)
    {
        _name = name;
        _description = description;
        _level = 1;
        _curUP = 0;
        // Determine _maxUP via algorithm
        // _maxUP = _level * ...
    }

    private void LevelUp()
    {

    }

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
}
