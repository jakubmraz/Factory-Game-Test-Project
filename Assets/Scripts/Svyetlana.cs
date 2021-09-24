using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Svyetlana : MonoBehaviour
{
    public Building targetBuilding;
    public Building parentBuilding;
    public bool headingBack;

    public void FindTargetBuilding(string targetBuildingType)
    {
        GameSystem theGameSystem = FindObjectOfType<GameSystem>();
        switch (targetBuildingType)
        {
            case "Sweatshop":
                List<PlasticPlant> sweatshops = new List<PlasticPlant>();
                foreach (var building in theGameSystem.buildings)
                {
                    if (building is PlasticPlant sweatshop)
                    {
                        sweatshops.Add(sweatshop);
                    }
                }
                targetBuilding = sweatshops.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
                    .FirstOrDefault();
                break;

            case "Plastic Container":
                List<PlasticContainerBuilding> plasticContainers = new List<PlasticContainerBuilding>();
                foreach (var building in theGameSystem.buildings)
                {
                    if (building is PlasticContainerBuilding container)
                    {
                        plasticContainers.Add(container);
                    }
                }
                targetBuilding = plasticContainers.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
                    .FirstOrDefault();
                break;

            case "Garage":
                List<GarageBuilding> garages = new List<GarageBuilding>();
                foreach (var building in theGameSystem.buildings)
                {
                    if (building is GarageBuilding garage)
                    {
                        garages.Add(garage);
                    }
                }
                targetBuilding = garages.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
                    .FirstOrDefault();
                break;
        }

        if (targetBuilding != null)
        {
            GoToTargetBuilding();
        }
        else
        {
            parentBuilding.deployedSvyetlanas -= 1;
            Destroy(gameObject);
        }
    }

    public void GoToTargetBuilding()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetBuilding.transform.position);
    }

    public void GoBack()
    {
        headingBack = true;
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(parentBuilding.transform.position);
    }

    public void FinishDutyAndDie()
    {
        parentBuilding.FinishProduction();
        Destroy(gameObject);
    }
}
