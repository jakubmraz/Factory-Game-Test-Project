using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingInfoScreen : MonoBehaviour
{
    public GameSystem theGameSystem;

    private Building building;

    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingConsumption;
    [SerializeField] private TextMeshProUGUI buildingProduction;
    [SerializeField] private TextMeshProUGUI buildingMaintenance;

    public void GetBuildingInfo(Building buildingToGet)
    {
        building = buildingToGet;
        buildingName.text = building.buildingName;
        buildingConsumption.text = building.materialConsumedAmount + "x " + building.materialConsumed;
        buildingProduction.text = "1x " + building.productProduced;
    }

    public void CloseScreen()
    {
        theGameSystem.HideBuildingInfoScreen();
    }
}
