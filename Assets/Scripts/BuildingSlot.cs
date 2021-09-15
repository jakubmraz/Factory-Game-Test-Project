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
        if (isPlayerInCollision)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //Show building selection menu

                //On selection, check selected building's price and paste here instead of the static 50
                if(theGameSystem.money >= 50){
                    theGameSystem.money -= 50;
                    building = Instantiate(plasticPlant, transform);
                    theGameSystem.buildings.Add(building);
                }
            }

            if (Input.GetButtonDown("Fire2"))
            {
                //Show building selection menu

                //On selection, check selected building's price and paste here instead of the static 50
                if (theGameSystem.money >= 50)
                {
                    theGameSystem.money -= 50;
                    building = Instantiate(sellingBuilding, transform);
                }
            }
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
        }
    }
}
