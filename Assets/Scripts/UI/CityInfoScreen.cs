using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityInfoScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cityName;
    [SerializeField] private TextMeshProUGUI cityPopulation;
    [SerializeField] private TextMeshProUGUI ecoAwareness;
    [SerializeField] private TextMeshProUGUI dailyPollution;
    [SerializeField] private TextMeshProUGUI dailyPlastic;
    [SerializeField] private TextMeshProUGUI totalPollution;
    [SerializeField] private TextMeshProUGUI totalPlastic;

    public void GetCityInfo(City city)
    {
        cityName.text = city.Name;
        cityPopulation.text = "" + city.Population;
        ecoAwareness.text = "" + city.EcoAwareness + "%";
        dailyPollution.text = "" + city.DailyPollution();
        dailyPlastic.text = "" + city.DailyPlastic;
        totalPollution.text = "" + city.TotalPollution() + " (" + city.TotalPollutionPercentage() + "%)";
        totalPlastic.text = "" + city.TotalPlastic + " (" + city.PlasticPercentage() + "%)";
    }
}
