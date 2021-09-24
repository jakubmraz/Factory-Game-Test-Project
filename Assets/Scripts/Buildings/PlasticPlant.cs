using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticPlant : Building
{
    public int plasticConsumed;

    public override void DeploySvyetlana()
    {
        deployedSvyetlanas += 1;
        Svyetlana svyetlana = Instantiate(SvyetlanaPrefab, transform.position, Quaternion.identity).GetComponent<Svyetlana>();
        svyetlana.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4);
        svyetlana.parentBuilding = this;
        svyetlana.FindTargetBuilding("Plastic Container");
    }

    public override void FinishProduction()
    {
        deployedSvyetlanas -= 1;
        if (theGameSystem.plasticWaste >= plasticConsumed)
        {
            theGameSystem.plasticWaste -= plasticConsumed;
            theGameSystem.fleeceJackets++;
        }
    }
}
