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

    [SerializeField] private Text plasticUI;
    [SerializeField] private Text timeUI;
    [SerializeField] private Text fleeceJacketUI;
    [SerializeField] private Text moneyUI;

    private bool MonthlyTickHappened;

    // Start is called before the first frame update
    void Start()
    {
        buildings = new List<Building>();
        money = 100;
        gameTime = new DateTime(2019, 09, 28, 12, 00, 00);
        StartCoroutine(TimePassingCoroutine());
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
}
