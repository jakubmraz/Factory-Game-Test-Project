using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropagandaBuilding : Building
{
    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        if (theCity.EcoAwareness < 100)
        { 
            theCity.EcoAwareness += 0.5f;
        }
    }
}
