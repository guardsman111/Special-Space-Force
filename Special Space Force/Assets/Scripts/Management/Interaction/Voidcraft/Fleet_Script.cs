using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fleet_Script : MonoBehaviour
{
    public List<Voidcraft_Script> containedCraft;
    public Fleet_Class fleetClass;
    public string fleetName;
    public RectTransform background;
    public int ID;
    public int uID; // Unique ID given on creation
    public TMP_InputField input;
    public Fleet_Manager manager;
    public bool fColours = false;

    public GameObject[] craftPositions;
    public GameObject[] craftPositions4;
    public GameObject[] craftPositions9;

    //Creates a Fleet from a Fleet class
    public void MakeFleet(Fleet_Class fleet, Fleet_Manager fManager)
    {
        manager = fManager;
        fleetClass = fleet;
        ID = fManager.FleetsS.Count + 1;
        fleetName = manager.manager.localisationManager.CreateName("FleetNames", this);
        fleetName = fleet.fleetName;

        input.text = fleetName;

        if (uID == 0)
        {
            uID = Random.Range(1, 10000000);
        }
    }

    //Creates a fleet from a fleet script and inserts a new parent
    public void MakeFleet(Fleet_Script fleet,  Fleet_Manager fManager)
    {
        manager = fManager;
        fleetClass = new Fleet_Class();
        ID = fleet.ID;
        background = fleet.background;
        containedCraft = fleet.containedCraft;

        fleetName = manager.manager.localisationManager.CreateName("FleetNames", this);
        fleet.fleetName = fleetName;

        input.text = fleetName;

        if (uID == 0)
        {
            uID = Random.Range(1, 10000000);
        }
    }


    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Fleet_Script viewedFleet)
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        if (viewedFleet == null)
        {
            input.gameObject.SetActive(true);
            gameObject.GetComponent<Image>().enabled = true;
            background.gameObject.SetActive(true);
            gameObject.SetActive(true);
            switch (ID)
            {

                case 1:
                    rTransform.localPosition = new Vector3(-4000, 24);
                    break;

                case 2:
                    rTransform.localPosition = new Vector3(-3050, 24);
                    break;

                case 3:
                    rTransform.localPosition = new Vector3(-2100, 24);
                    break;

                case 4:
                    rTransform.localPosition = new Vector3(-1150, 24);
                    break;

                case 5:
                    rTransform.localPosition = new Vector3(-200, 24);
                    break;

                case 6:
                    rTransform.localPosition = new Vector3(750, 24);
                    break;

                case 7:
                    rTransform.localPosition = new Vector3(1700, 24);
                    break;

                case 8:
                    rTransform.localPosition = new Vector3(2650, 24);
                    break;

                case 9:
                    rTransform.localPosition = new Vector3(3600, 24);
                    break;

            }
            gameObject.transform.localScale = new Vector3(1, 1);
            input.transform.localScale = new Vector3(1, 1);
            input.textComponent.transform.localScale = new Vector3(1, 1);
            input.textComponent.fontSize = 36;
            input.textComponent.enableWordWrapping = true;
            input.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 400);
            input.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 60);

            gameObject.transform.localScale = new Vector3(1, 1);
            gameObject.GetComponent<Image>().color = new Color32(191, 191, 191, 100);
        }
        else if (viewedFleet == this)
        {
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
            rTransform.localPosition = new Vector3(-3400, 24);
        }
        else
        {
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }


    //Sets the manager UI pressed value, so that the user can interact easier with the name
    public void NameSelected()
    {
        manager.menu = true;
    }

    //De-sets the manager UI pressed value, so that the user can interact easier with the name
    public void NameDeselected()
    {
        manager.menu = false;
    }
}
