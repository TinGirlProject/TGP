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
    protected int _range;

    protected Wrench _wrench;

    public InputMessage msg;

    protected virtual void Start()
    {
        _wrench = GetComponentInParent<Wrench>();
        if (!_wrench)
            Log.RED("Could not get parent 'Wrench' component!");
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

    public int Range
    {
        get { return _range; }
        set { _range = value; }
    }
}
