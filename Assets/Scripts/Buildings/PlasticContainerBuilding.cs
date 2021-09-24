using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlasticContainerBuilding : Building
{
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
        Debug.Log("Hi");
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
        svyetlana.parentBuilding = this;
        svyetlana.FindTargetBuilding("Garage");
    }

    public override void FinishProduction()
    {
        deployedSvyetlanas -= 1;
        if (currentStorage < maxStorage)
            currentStorage += 500;

        if (currentStorage > maxStorage)
            currentStorage = maxStorage;

        UpdateTooltip();
    }

    public override void OnSvyetlanaArrived()
    {
        if(currentStorage >= 300)
            currentStorage = -300;

        UpdateTooltip();
    }

    public void UpdateTooltip()
    {
        storageTooltip.text = currentStorage + "/" + maxStorage;
    }
}
