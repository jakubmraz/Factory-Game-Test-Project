using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    private Building building;
    private BuildingSlot slot;

    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingPrice;
    [SerializeField] private Image buildingIcon;

    public void AssignBuildingToButton(Building buildingToAssign, BuildingSlot slotBeingStoodIn)
    {
        building = buildingToAssign;
        slot = slotBeingStoodIn;

        buildingName.text = building.buildingName;
        buildingPrice.text = building.buildingCost + "$";
        buildingIcon.sprite = building.buildingIcon;
    }

    public void OnButtonClicked()
    {
        slot.BuildBuilding(building);
    }
}
