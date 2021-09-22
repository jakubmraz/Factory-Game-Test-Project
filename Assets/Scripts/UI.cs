using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GoalCompletedPopup goalCompletedPopup;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Image gameSpeed;
    [SerializeField] private Sprite speed1Sprite;
    [SerializeField] private Sprite speed2Sprite;
    [SerializeField] private Sprite speed3Sprite;

    [SerializeField] private Transform victoryScreen;
    [SerializeField] private TextMeshProUGUI victoryDescription;

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

    public void SwitchPausePlayButtons()
    {
        if (pauseButton.gameObject.activeInHierarchy)
        {
            pauseButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }
        else
        {
            playButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        }
    }

    public void UpdateGameSpeedIcon(int gameSpeedValue)
    {
        switch (gameSpeedValue)
        {
            case 1:
                gameSpeed.sprite = speed1Sprite;
                break;
            case 2:
                gameSpeed.sprite = speed2Sprite;
                break;
            case 3:
                gameSpeed.sprite = speed3Sprite;
                break;
        }
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

    public void ShowGoalCompletedWindow(Goal goal)
    {
        goalCompletedPopup.gameObject.SetActive(true);
        goalCompletedPopup.FillGoalInfo(goal);
    }

    public void HideGoalCompletedWindow()
    {
        goalCompletedPopup.gameObject.SetActive(false);
    }

    public void ShowVictoryScreen(City city)
    {
        victoryScreen.gameObject.SetActive(true);
        victoryDescription.text = "Thanks to you, the city of " + city.Name +
                                  " is now cleansed of pollution and can ascend into a new era of eco friendliness and prosperity.";
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
