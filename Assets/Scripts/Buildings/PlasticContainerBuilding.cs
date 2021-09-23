using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticContainerBuilding : Building
{
    private GameSystem theGameSystem;
    public int extraStorage;

    void Awake()
    {
        theGameSystem = FindObjectOfType<GameSystem>();
    }

    public override void OnBuilt()
    {
        Debug.Log("Hi");
        theGameSystem = FindObjectOfType<GameSystem>();
        theGameSystem.RaisePlasticStorage(extraStorage);
    }

    public override void OnDestroyed()
    {
        theGameSystem.ReducePlasticStorage(extraStorage);
    }
}
