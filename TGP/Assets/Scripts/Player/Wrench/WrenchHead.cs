using UnityEngine;
using System.Collections;

public abstract class WrenchHead : MonoBehaviour
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected string _neededComponent;
    [SerializeField]
    protected uint _range;

    public virtual void Init(string name, string description, string neededComponent, uint range)
    {
        _name = name;
        _description = description;
        _neededComponent = neededComponent;
        _range = range;
    }

    public abstract void Activate();

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string NeededComponent
    {
        get { return _neededComponent; }
        set { _neededComponent = value; }
    }

    public uint Range
    {
        get { return _range; }
        set { _range = value; }
    }
}
