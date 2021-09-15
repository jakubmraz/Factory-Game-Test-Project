using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
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

    private bool MonthlyTickHappened;

    // Start is called before the first frame update
    void Start()
    {
        buildings = new List<Building>();
        money = 100;
        gameTime = new DateTime(2019, 09, 28, 12, 00, 00);
        StartCoroutine(TimePassingCoroutine());
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

    // Update is called once per frame
    void FixedUpdate()
    {
        
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
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator MonthlyTickCoroutine()
    {
        if (!MonthlyTickHappened)
        {
            MonthlyTick();
            MonthlyTickHappened = true;
        }
        else
        {
            yield break;
        }
        
        while (gameTime.Day == 1 && MonthlyTickHappened)
        {
            yield return new WaitForSeconds(5f);
        }

        MonthlyTickHappened = false;
    }

    void HourTick()
    {
        plasticWaste++;

        plasticUI.text = "Plastic: " + plasticWaste;
        fleeceJacketUI.text = "Fleece Jackets: " + fleeceJackets;
        timeUI.text = gameTime.ToShortDateString() + " " + gameTime.ToShortTimeString();
        moneyUI.text = "Money: " + money + "$";
    }

    void MonthlyTick()
    {
        foreach (var building in buildings)
        {
            money -= building.monthlyUpkeep;
        }
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

    private void FillBuildingUI(BuildingSlot slotBeingStoodIn)
    {
        //Don't ask me about the x and y coordinates, I have no idea, it just works
        int posY = 250;

        foreach (var building in buildingPrefabs)
        {
            BuildButton newBuildingButton = Instantiate(buildingButtonPrefab, new Vector3(420, posY), Quaternion.identity, buildingsContainer).GetComponent<BuildButton>();
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
