using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarageBuilding : Building
{
    public int plasticProduced;
    public int maxStorage = 1000;
    public int currentStorage;

    public TextMeshProUGUI storageTooltip;

    void Awake()
    {
        UpdateTooltip();
    }

    public override void Produce(GameSystem theGameSystem, City theCity)
    {
        if (currentStorage < maxStorage)
        {
            theCity.TotalPlastic -= plasticProduced;
            currentStorage += plasticProduced;

            if (currentStorage > maxStorage)
                currentStorage = maxStorage;
        }

        if (theCity.TotalPlastic < 0)
        {
            theCity.TotalPlastic = 0;
        }

        UpdateTooltip();
    }

    public void UpdateTooltip()
    {
        storageTooltip.text = currentStorage + "/" + maxStorage;
    }

    public override bool OnSvyetlanaArrived(Svyetlana svyetlana)
    {
        bool success = false;
        if (currentStorage >= svyetlana.howMuchToCarry)
        {
            currentStorage -= svyetlana.howMuchToCarry;
            success = true;
        }
        
        UpdateTooltip();
        return success;
    }
}
