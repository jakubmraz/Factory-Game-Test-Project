using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
    private GameSystem theGameSystem;

    private BoxCollider triggerColider;
    [SerializeField] private Canvas buildButton;

    private Building building;
    private bool hasBuilding;
    private bool isPlayerInCollision;

    [SerializeField] private Building plasticPlant;
    [SerializeField] private Building sellingBuilding;

    // Start is called before the first frame update
    void Awake()
    {
        theGameSystem = GameObject.FindObjectOfType<GameSystem>();
        triggerColider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBuildButtonPressed()
    {
        if (isPlayerInCollision)
        {
            theGameSystem.ShowBuildMenu(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBuilding)
        {
            buildButton.gameObject.SetActive(true);
            isPlayerInCollision = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            buildButton.gameObject.SetActive(false);
            isPlayerInCollision = false;

            theGameSystem.HideBuildMenu();
        }
    }

    public void BuildBuilding(Building building)
    {
        if (theGameSystem.money >= building.buildingCost)
        {
            theGameSystem.money -= building.buildingCost;
            this.building = Instantiate(building, transform);
            theGameSystem.buildings.Add(building);

            hasBuilding = true;
            buildButton.gameObject.SetActive(false);
            theGameSystem.HideBuildMenu();
        }
    }
}
