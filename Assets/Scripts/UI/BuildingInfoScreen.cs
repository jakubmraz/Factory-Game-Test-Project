using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingInfoScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingConsumption;
    [SerializeField] private TextMeshProUGUI buildingProduction;
    [SerializeField] private TextMeshProUGUI buildingMaintenance;

    public void GetBuildingInfo(Building buildingToGet)
    {
        buildingName.text = buildingToGet.buildingName;
        buildingConsumption.text = buildingToGet.materialConsumedAmount + "x " + buildingToGet.materialConsumed;
        buildingProduction.text = "1x " + buildingToGet.productProduced;
    }
}
