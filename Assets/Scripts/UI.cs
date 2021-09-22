using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private City theCity;
    private Campaign theCampaign;
    private GameSystem theGameSystem;

    private DateTime gameTime;

    [SerializeField] private List<Building> buildingPrefabs;

    [SerializeField] private Text plasticUI;
    [SerializeField] private Text timeUI;
    [SerializeField] private Text fleeceJacketUI;
    [SerializeField] private Text moneyUI;

    [SerializeField] private RectTransform buildMenu;
    [SerializeField] private Button buildingButtonPrefab;
    [SerializeField] private RectTransform buildingsContainer;

    [SerializeField] private BuildingInfoScreen buildingInfoScreen;
    [SerializeField] private CityInfoScreen cityInfoScreen;
    [SerializeField] private CampaignInfoScreen campaignInfoScreen;

    void Awake()
    {
        theCity = GetComponent<City>();
        theCampaign = GetComponent<Campaign>();
        theGameSystem = GetComponent<GameSystem>();
        gameTime = theGameSystem.gameTime;
    }

    public void UpdateUI()
    {
        theCity = GetComponent<City>();
        theCampaign = GetComponent<Campaign>();
        theGameSystem = GetComponent<GameSystem>();
        gameTime = theGameSystem.gameTime;

        cityInfoScreen.GetCityInfo(theCity);

        plasticUI.text = "Plastic: " + theGameSystem.plasticWaste;
        fleeceJacketUI.text = "Fleece Jackets: " + theGameSystem.fleeceJackets;
        timeUI.text = gameTime.ToShortDateString() + " " + gameTime.ToShortTimeString();
        moneyUI.text = "Money: " + theGameSystem.money + "$";
        cityInfoScreen.GetCityInfo(theCity);
        campaignInfoScreen.GetCampaignInfo(theCampaign, gameTime);
    }

    public void ShowBuildngInfoScreen(Building building)
    {
        HideBuildMenu();
        HideCampaignInfo();
        HideCityInfo();

        buildingInfoScreen.gameObject.SetActive(true);
        buildingInfoScreen.GetBuildingInfo(building);
    }

    public void HideBuildingInfoScreen()
    {
        buildingInfoScreen.gameObject.SetActive(false);
    }

    public void ShowBuildMenu(BuildingSlot slotBeingStoodIn)
    {
        HideCityInfo();
        HideCampaignInfo();
        HideBuildingInfoScreen();

        FillBuildingUI(slotBeingStoodIn);
        buildMenu.gameObject.SetActive(true);
    }

    public void HideBuildMenu()
    {
        buildMenu.gameObject.SetActive(false);
        FlushBuildingUI();
    }

    public void ShowCityInfo()
    {
        HideBuildMenu();
        HideBuildingInfoScreen();
        HideCampaignInfo();

        cityInfoScreen.gameObject.SetActive(true);
        cityInfoScreen.GetCityInfo(theCity);
    }

    public void HideCityInfo()
    {
        cityInfoScreen.gameObject.SetActive(false);
    }

    public void ShowCampaignInfo()
    {
        HideBuildMenu();
        HideBuildingInfoScreen();
        HideCityInfo();

        campaignInfoScreen.gameObject.SetActive(true);
        campaignInfoScreen.GetCampaignInfo(theCampaign, gameTime);
    }

    public void HideCampaignInfo()
    {
        campaignInfoScreen.gameObject.SetActive(false);
    }

    private void FillBuildingUI(BuildingSlot slotBeingStoodIn)
    {
        int posY = -40;

        foreach (var building in buildingPrefabs)
        {
            Transform newBuildingButton = Instantiate(buildingButtonPrefab, new Vector3(0, posY), Quaternion.identity).GetComponent<Transform>();
            newBuildingButton.SetParent(buildingsContainer, false);
            newBuildingButton.GetComponent<BuildButton>().AssignBuildingToButton(building, slotBeingStoodIn);

            posY -= 70;
        }
    }

    private void FlushBuildingUI()
    {
        foreach (Transform child in buildingsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
