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

    public virtual void Produce(GameSystem theGameSystem, City theCity)
    {

    }

    public virtual void OnBuilt()
    {

    }

    public virtual void OnDestroyed()
    {

    }
}
