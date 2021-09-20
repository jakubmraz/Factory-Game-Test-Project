using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBuilding : Building
{
    private GameSystem theGameSystem;

    void Awake()
    {
        theGameSystem = GameObject.FindObjectOfType<GameSystem>();
        StartCoroutine(SellFleeceJackets());
    }

    IEnumerator SellFleeceJackets()
    {
        while (true)
        {
            if (theGameSystem.fleeceJackets >= 1)
            {
                theGameSystem.fleeceJackets--;
                theGameSystem.money += 5;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
