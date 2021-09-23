using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBuilding : Building
{
    private int moneyProducedThisMonth;
    public int moneyProducedLastMonth;

    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        if (theGameSystem.fleeceJackets >= 1)
        {
            theGameSystem.fleeceJackets--;
            theGameSystem.money += 5;
            moneyProducedThisMonth += 5;
        }
    }

    //Call this at the beginning of a new month
    public void FlushMonthlyEarnings()
    {
        moneyProducedLastMonth = moneyProducedThisMonth;
        moneyProducedThisMonth = 0;
    }
}
