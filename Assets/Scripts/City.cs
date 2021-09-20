using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class City : MonoBehaviour
{
    public string Name;
    public int Population;
    public float EcoAwareness;
    public int DailyPlastic;
    public int TotalPlastic;

    public int StartingPlastic;

    void Awake()
    {
        TotalPlastic += StartingPlastic;
        Pollute();
    }

    public int DailyPollution()
    {
        //All types of trash together
        return DailyPlastic;
    }

    public int TotalPollution()
    {
        //All types of trash together
        return TotalPlastic;
    }

    public int TotalPollutionPercentage()
    {
        //The city can handle hundred times its population in pollution
        //float percentage = TotalPollution() / (Population * 100) * 100;
        double pollution = TotalPollution();
        double percentage = pollution / (Population * 100) * 100;

        return Convert.ToInt32(percentage);
    }

    public int PlasticPercentage()
    {
        float percentage = TotalPlastic / TotalPollution() * 100;
        return Convert.ToInt32(percentage);
    }

    public void Pollute()
    {
        //Each person produces 1-5 pieces of trash a day, eco awareness reduces this by up to 50%
        //int dailyPlastic = Population * Random.Range(1, 5) * (Convert.ToInt32(EcoAwareness / 2) / 100);
        int dailyPlastic = 0;

        //Calculates the pollution for each tenth of the population
        //so the entire city doesn't produce the max amount on an unlucky day
        for (int i = 0; i < 10; i++)
        {
            float dailyPlasticTemp = Population / 10 * Random.Range(1, 5) * EcoAwareness / 2 / 100;
            dailyPlastic += Convert.ToInt32(dailyPlasticTemp);
        }

        DailyPlastic = dailyPlastic;
        TotalPlastic += dailyPlastic;
    }
}
