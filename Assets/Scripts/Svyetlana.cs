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

    private GameSystem theGameSystem;
    public int howMuchToCarry;
    private bool wasSuccessful;

    public void InitializeSvyetlana(string targetResource, int amount, Building parentBuilding)
    {
        theGameSystem = FindObjectOfType<GameSystem>();
        this.parentBuilding = parentBuilding;
        this.howMuchToCarry = amount;

        if (targetBuilding == null)
        {
            FindTargetBuilding(targetResource);
        }
    }

    private void FindTargetBuilding(string targetResource)
    {
        switch (targetResource)
        {
            case "Fleece Jacket":
                targetBuilding = FindNearestSweatshop();
                break;

            case "Plastic":
                if (parentBuilding.GetType() == typeof(PlasticPlant))
                {
                    targetBuilding = FindNearestPlasticContainer();
                    if (targetBuilding == null)
                    {
                        targetBuilding = FindNearestGarage();
                    }
                }

                if (parentBuilding.GetType() == typeof(PlasticContainerBuilding))
                {
                    targetBuilding = FindNearestGarage();
                }
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

    private void GoToTargetBuilding()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetBuilding.transform.position);
    }

    public void GoBack(bool successful)
    {
        wasSuccessful = successful;
        headingBack = true;
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(parentBuilding.transform.position);
    }

    public void FinishDutyAndDie()
    {
        parentBuilding.FinishProduction(wasSuccessful);
        Destroy(gameObject);
    }

    private PlasticPlant FindNearestSweatshop()
    {
        List<PlasticPlant> sweatshops = new List<PlasticPlant>();
        foreach (var building in theGameSystem.buildings)
        {
            if (building is PlasticPlant sweatshop)
            {
                if(sweatshop.currentStorage >= howMuchToCarry)
                    sweatshops.Add(sweatshop);
            }
        }

        return sweatshops.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
            .FirstOrDefault();
    }

    private PlasticContainerBuilding FindNearestPlasticContainer()
    {
        List<PlasticContainerBuilding> plasticContainers = new List<PlasticContainerBuilding>();
        foreach (var building in theGameSystem.buildings)
        {
            if (building is PlasticContainerBuilding container)
            {
                if(container.currentStorage >= howMuchToCarry)
                    plasticContainers.Add(container);
            }
        }

        return plasticContainers.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
            .FirstOrDefault();
    }

    private GarageBuilding FindNearestGarage()
    {
        List<GarageBuilding> garages = new List<GarageBuilding>();
        foreach (var building in theGameSystem.buildings)
        {
            if (building is GarageBuilding garage)
            {
                if(garage.currentStorage >= howMuchToCarry)
                    garages.Add(garage);
            }
        }

        return garages.OrderBy(t => (t.transform.position - this.transform.position).sqrMagnitude)
            .FirstOrDefault();
    }
}
