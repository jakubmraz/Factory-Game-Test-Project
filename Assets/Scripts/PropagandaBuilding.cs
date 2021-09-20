using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropagandaBuilding : Building
{
    private City theCity;

    void Awake()
    {
        theCity = FindObjectOfType<City>();
        StartCoroutine(SpreadPropaganda());
    }

    IEnumerator SpreadPropaganda()
    {
        while (true)
        {
            if (theCity.EcoAwareness < 100)
            {
                theCity.EcoAwareness += 0.5f;
            }
            yield return new WaitForSeconds(50f);
        }
    }
}
