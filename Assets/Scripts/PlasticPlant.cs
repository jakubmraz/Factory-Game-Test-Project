using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticPlant : Building
{
    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        if (theGameSystem.plasticWaste >= 10)
        {
            theGameSystem.plasticWaste -= 10;
            theGameSystem.fleeceJackets++;
        }
    }
}
