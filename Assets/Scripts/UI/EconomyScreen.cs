using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI incomeValue;
    [SerializeField] private TextMeshProUGUI spendingValue;
    [SerializeField] private TextMeshProUGUI buildingMaintenanceValue;
    [SerializeField] private TextMeshProUGUI balanceValue;

    public void GetEconomyInfo(GameSystem theGameSystem)
    {
        int income = 0;
        int buildingMonthlyUpkeep = 0;
        foreach (var building in theGameSystem.buildings)
        {
            buildingMonthlyUpkeep += building.monthlyUpkeep;
            if (building is SellingBuilding sellingBuilding)
            {
                income += sellingBuilding.moneyProducedLastMonth;
            }
        }

        int totalSpending = buildingMonthlyUpkeep;

        incomeValue.text = income + "$";
        buildingMaintenanceValue.text = buildingMonthlyUpkeep + "$ (100%)"; //:^)
        spendingValue.text = buildingMonthlyUpkeep + "$";
        balanceValue.text = Convert.ToInt32(income - totalSpending) + "$";
    }
}
