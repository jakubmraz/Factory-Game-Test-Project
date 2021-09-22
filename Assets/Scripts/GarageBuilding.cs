using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageBuilding : Building
{
    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        theCity.TotalPlastic -= 50;
        theGameSystem.plasticWaste += 50;

        if (theCity.TotalPlastic < 0)
        {
            theCity.TotalPlastic = 0;
        }
    }
}
