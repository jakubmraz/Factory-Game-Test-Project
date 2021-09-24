using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    private UI theUI;
    private GameSystem theGameSystem;

    [SerializeField] private Button buildButton;
    [SerializeField] private Button infoButton;

    private Building building;
    private bool hasBuilding;
    private bool isPlayerInCollision;

    [SerializeField] private Building plasticPlant;
    [SerializeField] private Building sellingBuilding;
    [SerializeField] private Building garageBuilding;

    // Start is called before the first frame update
    void Awake()
    {
        theUI = GameObject.FindObjectOfType<UI>();
        theGameSystem = FindObjectOfType<GameSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBuildButtonPressed()
    {
        if (isPlayerInCollision)
        {
            theUI.ShowBuildMenu(this);
        }
    }

    public void OnInfoButtonPressed()
    {
        if (isPlayerInCollision)
        {
            theUI.ShowBuildngInfoScreen(building);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBuilding)
        {
            buildButton.gameObject.SetActive(true);
            isPlayerInCollision = true;
        }

        if (other.CompareTag("Player") && hasBuilding)
        {
            infoButton.gameObject.SetActive(true);
            isPlayerInCollision = true;
        }

        if (other.CompareTag("Svyetlana"))
        {
            Svyetlana svyetlana = other.GetComponent<Svyetlana>();
            if (!svyetlana.headingBack && this.building == svyetlana.targetBuilding)
            {
                building.OnSvyetlanaArrived();
                svyetlana.GoBack();
            }
            if(svyetlana.headingBack && this.building == svyetlana.parentBuilding)
                svyetlana.FinishDutyAndDie();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            buildButton.gameObject.SetActive(false);
            infoButton.gameObject.SetActive(false);
            isPlayerInCollision = false;

            theUI.HideBuildMenu();
            theUI.HideBuildingInfoScreen();
        }
    }

    public void BuildBuilding(Building building)
    {
        if (theGameSystem.money >= building.buildingCost)
        {
            theGameSystem.money -= building.buildingCost;
            this.building = Instantiate(building, transform);
            theGameSystem.buildings.Add(this.building);
            building.OnBuilt();

            hasBuilding = true;
            buildButton.gameObject.SetActive(false);
            theUI.HideBuildMenu();
        }
    }
}
