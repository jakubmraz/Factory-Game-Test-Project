using System;
using TMPro;
using UnityEngine;

public class CampaignInfoScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI campaignGoal;
    [SerializeField] private TextMeshProUGUI campaignTimeLimit;
    [SerializeField] private TextMeshProUGUI campaignTimeRemaining;
    [SerializeField] private GameObject sideGoal1;
    [SerializeField] private TextMeshProUGUI sideGoal1Completed;
    [SerializeField] private TextMeshProUGUI sideGoal1Value;
    [SerializeField] private TextMeshProUGUI sideGoal1Reward;
    [SerializeField] private GameObject sideGoal2;
    [SerializeField] private TextMeshProUGUI sideGoal2Completed;
    [SerializeField] private TextMeshProUGUI sideGoal2Value;
    [SerializeField] private TextMeshProUGUI sideGoal2Reward;

    public void GetCampaignInfo(Campaign campaign, DateTime gameTime)
    {
        campaignGoal.text = campaign.MainGoal.GetGoalString();
        campaignTimeLimit.text = campaign.TimeLimit.ToShortDateString();
        TimeSpan span = campaign.TimeLimit.Subtract(gameTime);
        campaignTimeRemaining.text = span.Days + " days";

        if (campaign.SideGoals.Count >= 1)
        {
            sideGoal1Value.text = campaign.SideGoals[0].GetGoalString();
            sideGoal1Reward.text = campaign.SideGoals[0].rewardValue + " " + campaign.SideGoals[0].rewardType;
            if(campaign.SideGoals[0].completed)
                sideGoal1Completed.gameObject.SetActive(true);
        }
        else
        {
            sideGoal1.gameObject.SetActive(false);
        }

        if (campaign.SideGoals.Count >= 2)
        {
            Debug.Log(campaign.SideGoals.Count);
            sideGoal2Value.text = campaign.SideGoals[1].GetGoalString();
            sideGoal2Reward.text = campaign.SideGoals[1].rewardValue + " " + campaign.SideGoals[1].rewardType;
            if (campaign.SideGoals[1].completed)
                sideGoal2Completed.gameObject.SetActive(true);
        }
        else
        {
            sideGoal2.gameObject.SetActive(false);
        }
    }
}
