using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBuilding : Building
{
    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        if (theGameSystem.fleeceJackets >= 1)
        {
            theGameSystem.fleeceJackets--;
            theGameSystem.money += 5;
        }
    }
}
