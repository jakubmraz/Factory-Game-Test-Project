using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlasticContainerBuilding : Building
{
    public int plasticDeliverySize = 500;

    public int maxStorage = 5000;
    public int currentStorage = 0;

    public TextMeshProUGUI storageTooltip;

    void Awake()
    {
        theGameSystem = FindObjectOfType<GameSystem>();
        UpdateTooltip();
    }

    public override void Produce(GameSystem gameSystem, City city)
    {
        if (currentStorage < maxStorage)
        {
            base.Produce(gameSystem, city);
        }
    }

    public override void DeploySvyetlana()
    {
        deployedSvyetlanas += 1;
        Svyetlana svyetlana = Instantiate(SvyetlanaPrefab, transform.position, Quaternion.identity).GetComponent<Svyetlana>();
        svyetlana.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4);
        svyetlana.InitializeSvyetlana("Plastic", plasticDeliverySize, this);
    }

    public override void FinishProduction(bool wasSvyetlanaSuccessful)
    {
        deployedSvyetlanas -= 1;
        if (wasSvyetlanaSuccessful)
        {
            if (currentStorage < maxStorage)
                currentStorage += plasticDeliverySize;

            if (currentStorage > maxStorage)
                currentStorage = maxStorage;
        }
        
        UpdateTooltip();
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

    public void UpdateTooltip()
    {
        storageTooltip.text = currentStorage + "/" + maxStorage;
    }
}
