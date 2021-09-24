using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string buildingName = "Name Needed";
    public int buildingCost = 50;
    public int monthlyUpkeep = 10;
    public Sprite buildingIcon;

    public string materialConsumed;
    public int materialConsumedAmount;

    public string productProduced;

    public Svyetlana SvyetlanaPrefab;
    public int maxSvyetlanas = 5;
    public int deployedSvyetlanas = 0;

    protected GameSystem theGameSystem;
    protected City theCity;

    public virtual void Produce(GameSystem gameSystem, City city)
    {
        theGameSystem = gameSystem;
        theCity = city;
        if (deployedSvyetlanas < maxSvyetlanas)
            DeploySvyetlana();
    }

    public virtual void OnBuilt()
    {

    }

    public virtual void OnDestroyed()
    {

    }

    public virtual void DeploySvyetlana()
    {

    }

    public virtual void OnSvyetlanaReturned()
    {

    }

    public virtual void OnSvyetlanaArrived()
    {

    }

    public virtual void FinishProduction()
    {

    }
}
