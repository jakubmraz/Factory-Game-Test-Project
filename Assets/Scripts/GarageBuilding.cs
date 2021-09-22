using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageBuilding : Building
{
    public int plasticProduced;

    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        theCity.TotalPlastic -= plasticProduced;
        theGameSystem.plasticWaste += plasticProduced;

        if (theCity.TotalPlastic < 0)
        {
            theCity.TotalPlastic = 0;
        }
    }
}
