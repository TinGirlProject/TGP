using UnityEngine;
using System.Collections.Generic;

public class Wrench : MonoBehaviour 
{
    private List<WrenchUpgrade> _upgrades;
    public WrenchHead _head;

    void Start()
    {
        _upgrades = new List<WrenchUpgrade>();
    }

    void Update()
    {
        
    }

    void Use()
    {
        _head.Activate();
    }
}
