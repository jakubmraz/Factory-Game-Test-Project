using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticPlant : Building
{
    public int plasticConsumed;

    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        if (theGameSystem.plasticWaste >= plasticConsumed)
        {
            theGameSystem.plasticWaste -= plasticConsumed;
            theGameSystem.fleeceJackets++;
        }
    }
}
