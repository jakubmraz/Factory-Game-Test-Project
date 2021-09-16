using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageBuilding : Building
{
    private City theCity;
    private GameSystem theGameSystem;

    void Awake()
    {
        theCity = FindObjectOfType<City>();
        theGameSystem = FindObjectOfType<GameSystem>();
        StartCoroutine(DeliverTrash());
    }

    IEnumerator DeliverTrash()
    {
        while (true)
        {
            if (theCity.TotalPlastic > 0)
            {
                theCity.TotalPlastic -= 50;
                theGameSystem.plasticWaste += 50;

                if (theCity.TotalPlastic < 0)
                {
                    theCity.TotalPlastic = 0;
                }
            }
            yield return new WaitForSeconds(5f);
        }

    }
}
