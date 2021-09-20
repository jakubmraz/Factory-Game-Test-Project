using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    private City theCity;
    public Campaign theCampaign;

    public int money;

    public int plasticWaste;
    public int fleeceJackets;

    public List<Building> buildings;

    public DateTime gameTime;

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

    private bool monthlyTickHappened;
    private bool dailyTickHappened;

    // Start is called before the first frame update
    void Start()
    {
        theCampaign = gameObject.AddComponent<Campaign>();
        theCampaign.MainGoal = new Goal()
        {
            pollutionGoal = 10
        };
        theCampaign.TimeLimit = new DateTime(2020, 1, 1);
        theCampaign.SideGoals = new List<Goal>()
        {
            new Goal()
            {
                ecoAwarenessGoal = 60,
                rewardType = "Money",
                rewardValue = 1000
            }
        };

        theCity = GetComponent<City>();
        buildings = new List<Building>();
        money = 500;
        gameTime = new DateTime(2019, 09, 28, 12, 00, 00);
        StartCoroutine(TimePassingCoroutine());
    }

    IEnumerator TimePassingCoroutine()
    {
        while(true){
            gameTime = gameTime.AddHours(1);
            //gameTime = gameTime.AddDays(1);
            HourTick();

            if (gameTime.Day == 1)
            {
                StartCoroutine(MonthlyTickCoroutine());
            }
            if (gameTime.Hour == 0)
            {
                StartCoroutine(DailyTickCoroutine());
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator MonthlyTickCoroutine()
    {
        if (!monthlyTickHappened)
        {
            MonthlyTick();
            monthlyTickHappened = true;
        }
        else
        {
            yield break;
        }
        
        while (gameTime.Day == 1 && monthlyTickHappened)
        {
            yield return new WaitForSeconds(5f);
        }

        monthlyTickHappened = false;
    }

    IEnumerator DailyTickCoroutine()
    {
        if (!dailyTickHappened)
        {
            DailyTick();
            dailyTickHappened = true;
        }
        else
        {
            yield break;
        }

        while (gameTime.Hour == 0 && dailyTickHappened)
        {
            yield return new WaitForSeconds(5f);
        }

        dailyTickHappened = false;
    }

    void HourTick()
    {
        //plasticWaste++;
        cityInfoScreen.GetCityInfo(theCity);

        plasticUI.text = "Plastic: " + plasticWaste;
        fleeceJacketUI.text = "Fleece Jackets: " + fleeceJackets;
        timeUI.text = gameTime.ToShortDateString() + " " + gameTime.ToShortTimeString();
        moneyUI.text = "Money: " + money + "$";
    }

    void DailyTick()
    {
        theCity.Pollute();
        CheckCampaignGoals();
        cityInfoScreen.GetCityInfo(theCity);
        campaignInfoScreen.GetCampaignInfo(theCampaign, gameTime);
    }

    void MonthlyTick()
    {
        foreach (var building in buildings)
        {
            money -= building.monthlyUpkeep;
        }
    }

    void CheckCampaignGoals()
    {
        foreach (var sideGoal in theCampaign.SideGoals)
        {
            if (sideGoal.CheckGoalCompletion(theCity))
            {
                switch (sideGoal.rewardType)
                {
                    case "Money":
                        money += sideGoal.rewardValue;
                        break;
                }
            }
        }

        if (theCampaign.MainGoal.CheckGoalCompletion(theCity))
        {
            Win();
        }
    }

    private void Win()
    {
        throw new NotImplementedException();
    }

    public void ShowBuildngInfoScreen(Building building)
    {
        buildingInfoScreen.gameObject.SetActive(true);
        buildingInfoScreen.GetBuildingInfo(building);
    }

    public void HideBuildingInfoScreen()
    {
        buildingInfoScreen.gameObject.SetActive(false);
    }

    public void ShowBuildMenu(BuildingSlot slotBeingStoodIn)
    {
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
        cityInfoScreen.gameObject.SetActive(true);
        cityInfoScreen.GetCityInfo(theCity);
    }

    public void HideCityInfo()
    {
        cityInfoScreen.gameObject.SetActive(false);
    }

    public void ShowCampaignInfo()
    {
        campaignInfoScreen.gameObject.SetActive(true);
        campaignInfoScreen.GetCampaignInfo(theCampaign, gameTime);
    }

    public void HideCampaignInfo()
    {
        campaignInfoScreen.gameObject.SetActive(false);
    }

    private void FillBuildingUI(BuildingSlot slotBeingStoodIn)
    {
        //Don't ask me about the x and y coordinates, I have no idea, it just works
        int posY = 250;

        foreach (var building in buildingPrefabs)
        {
            BuildButton newBuildingButton = Instantiate(buildingButtonPrefab, new Vector3(345, posY), Quaternion.identity, buildingsContainer).GetComponent<BuildButton>();
            newBuildingButton.AssignBuildingToButton(building, slotBeingStoodIn);

            posY -= 60;
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
