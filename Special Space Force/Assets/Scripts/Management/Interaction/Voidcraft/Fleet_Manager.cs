using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fleet_Manager : MonoBehaviour
{
    public Galaxy_Generation_Manager manager;
    public Manager_Script modManager;
    public GraphicRaycaster raycaster;
    public Fleet_Generator generator;

    public GameObject prefabFleet;
    public Fleet_Script viewedFleet;
    public InputField currentName;
    public Transform content;

    private List<Fleet_Class> fleets;
    private List<Fleet_Script> fleetsS;
    private List<int> fleetIDs;

    public Scrollbar slotFieldScroll;

    public bool menu;
    bool matched = false;
    int selectionExtension = 0;

    public List<Voidcraft_Script> selectedTroopers;

    public List<Fleet_Class> Fleets
    {
        get { return fleets; }

        set
        {
            if (value != fleets)
            {
                fleets = value;
            }
        }
    }
    public List<Fleet_Script> FleetsS
    {
        get { return fleetsS; }

        set
        {
            if (value != fleetsS)
            {
                fleetsS = value;
            }
        }
    }

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        menu = false;
        Fleets = new List<Fleet_Class>();
        FleetsS = new List<Fleet_Script>();
        fleetIDs = new List<int>();
    }

    //Creates a new Slot below the viewed slot
    public void NewFleet()
    {
        GameObject temp = Instantiate(prefabFleet, content.transform);
        Fleet_Script tempF = temp.GetComponent<Fleet_Script>();
        tempF.input.text = "New Fleet";
        tempF.fleetName = "New Fleet";
        tempF.ID = Fleets.Count + 1;
        fleetIDs.Add(tempF.ID);
        tempF.containedCraft = new List<Voidcraft_Script>();
        tempF.MakeFleet(tempF, this);
        tempF.SetPosition(viewedFleet);
        //slots.Add(slotN1.GetComponent<Fleet_Script>().MasterSaveClass());
    }


    //Opens the clicked slot
    public void OpenSlot(Fleet_Script newViewed)
    {
        viewedFleet = newViewed;
        currentName.text = newViewed.fleetName;
            foreach (Voidcraft_Script vs in newViewed.containedCraft)
            {
                vs.SetPosition(viewedFleet);
            }
            //Toggle[] cColours = squadOptions.GetComponentsInChildren<Toggle>();
            //foreach (Toggle t in cColours)
            //{
            //    if (t.name == "Squad Colours Toggle")
            //    {
            //        if (viewedSlot.cColours == true)
            //        {
            //            t.isOn = true;
            //        }
            //        else
            //        {
            //            t.isOn = false;
            //        }
            //    }
            //}
        
        slotFieldScroll.value = 0;
    }

}
