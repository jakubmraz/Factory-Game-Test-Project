using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBuilding : Building
{
    private int moneyProducedThisMonth;
    public int moneyProducedLastMonth;

    public override void DeploySvyetlana()
    {
        deployedSvyetlanas += 1;
        Svyetlana svyetlana = Instantiate(SvyetlanaPrefab, transform.position, Quaternion.identity).GetComponent<Svyetlana>();
        svyetlana.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4);
        svyetlana.InitializeSvyetlana("Fleece Jacket", 1, this);
    }

    public override void FinishProduction(bool wasSvyetlanaSuccessful)
    {
        deployedSvyetlanas -= 1;
        if (wasSvyetlanaSuccessful)
        {
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
