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

    //Creates a Fleet from a Fleet class
    public void MakeFleet(Fleet_Class fleet, Fleet_Manager fManager)
    {
        manager = fManager;
        fleetClass = fleet;
        ID = fManager.FleetsS.Count;
        //fleetName = manager.manager.localisationManager.CreateName("FleetNames", this);
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
            switch (ID)
            {

                case 1:
                    rTransform.localPosition = new Vector3(-420, 0);
                    break;

                case 2:
                    rTransform.localPosition = new Vector3(530, 0);
                    break;

                case 3:
                    rTransform.localPosition = new Vector3(1480, 0);
                    break;

                case 4:
                    rTransform.localPosition = new Vector3(2430, 0);
                    break;

                case 5:
                    rTransform.localPosition = new Vector3(3380, 0);
                    break;

                case 6:
                    rTransform.localPosition = new Vector3(4330, 0);
                    break;

                case 7:
                    rTransform.localPosition = new Vector3(5280, 0);
                    break;

                case 8:
                    rTransform.localPosition = new Vector3(6230, 0);
                    break;

                case 9:
                    rTransform.localPosition = new Vector3(7180, 0);
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

        }
        else
        {
            this.gameObject.SetActive(false);
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
