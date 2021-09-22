using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    private City theCity;
    private Campaign theCampaign;
    private UI theUI;
    [SerializeField] private Player thePlayer;

    public int money;

    public int plasticWaste;
    public int fleeceJackets;

    public List<Building> buildings;

    public DateTime gameTime;
    public int gameSpeed;
    public bool gamePaused;

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

        theUI = GetComponent<UI>();
        theCity = GetComponent<City>();
        buildings = new List<Building>();

        money = 500;
        gameTime = new DateTime(2019, 09, 28, 12, 00, 00);
        gameSpeed = 1;

        StartCoroutine(TimePassingCoroutine());
    }

    IEnumerator TimePassingCoroutine()
    {
        while(true){
            while (gamePaused)
            {
                yield return new WaitForSeconds(.33f);
            }

            gameTime = gameTime.AddHours(1);
            HourTick();

            if (gameTime.Day == 1)
            {
                StartCoroutine(MonthlyTickCoroutine());
            }
            if (gameTime.Hour == 0)
            {
                StartCoroutine(DailyTickCoroutine());
            }

            float actualSpeed = 1f;
            switch (gameSpeed)
            {
                case 1:
                    actualSpeed = 1f;
                    break;
                case 2:
                    actualSpeed = .66f;
                    break;
                case 3:
                    actualSpeed = .33f;
                    break;
            }
            yield return new WaitForSeconds(actualSpeed);
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
        theUI.UpdateUI();
    }

    void DailyTick()
    {
        theCity.Pollute();
        CheckCampaignGoals();
    }

    void MonthlyTick()
    {
        foreach (var building in buildings)
        {
            money -= building.monthlyUpkeep;
        }
    }

    public void RaiseGameSpeed()
    {
        if (gameSpeed < 3)
            gameSpeed++;

        theUI.UpdateGameSpeedIcon(gameSpeed);
    }

    public void LowerGameSpeed()
    {
        if (gameSpeed > 1)
            gameSpeed--;

        theUI.UpdateGameSpeedIcon(gameSpeed);
    }

    public void PauseGame()
    {
        gamePaused = true;
        thePlayer.PausePlayer();
        theUI.SwitchPausePlayButtons();
    }

    public void UnpauseGame()
    {
        gamePaused = false;
        thePlayer.UnpausePlayer();
        theUI.SwitchPausePlayButtons();
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
}
