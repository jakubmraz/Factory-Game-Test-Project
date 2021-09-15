using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticPlant : Building
{
    private GameSystem theGameSystem;

    void Awake()
    {
        theGameSystem = GameObject.FindObjectOfType<GameSystem>();
        StartCoroutine(ProduceFleeceJackets());
    }

    IEnumerator ProduceFleeceJackets()
    {
        while (true)
        {
            if (theGameSystem.plasticWaste >= 10)
            {
                theGameSystem.plasticWaste -= 10;
                theGameSystem.fleeceJackets++;
            }
            yield return new WaitForSeconds(5f);
        }
        
    }
}
