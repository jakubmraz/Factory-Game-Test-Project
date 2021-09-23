using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalCompletedPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goalDescription;
    [SerializeField] private TextMeshProUGUI rewardDescription;

    public void FillGoalInfo(Goal goal)
    {
        goalDescription.text = "Achieve " + goal.GetGoalString();
        rewardDescription.text = "Reward: " + goal.rewardValue + " " + goal.rewardType;
    }
}
