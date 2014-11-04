using UnityEngine;
using System.Collections.Generic;

public class Wrench : MonoBehaviour 
{
    public const string USEHEAD = "Use Head";

    private List<WrenchUpgrade> _upgrades;
    public WrenchHead head;

    void Awake()
    {
        Messenger.AddListener(USEHEAD, Use);
    }

    void Start()
    {
        _upgrades = new List<WrenchUpgrade>();
    }

    void Update()
    {
        
    }

    void Use()
    {
        head.Activate();
    }
}
