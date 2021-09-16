﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
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
        theGameSystem = GameObject.FindObjectOfType<GameSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBuildButtonPressed()
    {
        if (isPlayerInCollision)
        {
            theGameSystem.ShowBuildMenu(this);
        }
    }

    public void OnInfoButtonPressed()
    {
        if (isPlayerInCollision)
        {
            theGameSystem.ShowBuildngInfoScreen(building);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBuilding)
        {
            buildButton.gameObject.SetActive(true);
            isPlayerInCollision = true;
        }

        if (other.tag == "Player" && hasBuilding)
        {
            infoButton.gameObject.SetActive(true);
            isPlayerInCollision = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            buildButton.gameObject.SetActive(false);
            infoButton.gameObject.SetActive(false);
            isPlayerInCollision = false;

            theGameSystem.HideBuildMenu();
            theGameSystem.HideBuildingInfoScreen();
        }
    }

    public void BuildBuilding(Building building)
    {
        if (theGameSystem.money >= building.buildingCost)
        {
            theGameSystem.money -= building.buildingCost;
            this.building = Instantiate(building, transform);
            theGameSystem.buildings.Add(building);

            hasBuilding = true;
            buildButton.gameObject.SetActive(false);
            theGameSystem.HideBuildMenu();
        }
    }
}
