using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int? pollutionGoal;
    public int? plasticGoal;

    public int? ecoAwarenessGoal;

    //public int? jacketsSoldGoal;

    public bool completed;

    public string rewardType;
    public int rewardValue;

    public bool CheckGoalCompletion(City city)
    {
        if(!completed)
        {
            if (pollutionGoal == null || city.TotalPollutionPercentage() <= pollutionGoal)
                if (plasticGoal == null || city.TotalPlastic <= plasticGoal)
                    if (ecoAwarenessGoal == null || city.EcoAwareness >= ecoAwarenessGoal)
                        //if (jacketsSoldGoal == null || jacketsSold >= jacketsSoldGoal)
                    {
                        completed = true;
                        return true;
                    }
        }

        return false;
    }

    public string GetGoalString()
    {
        if (pollutionGoal != null)
            return pollutionGoal + "% Pollution";
        if (plasticGoal != null)
            return plasticGoal + " Plastic";
        if (ecoAwarenessGoal != null)
            return ecoAwarenessGoal + "% Eco Awareness";

        return string.Empty;
    }
}
