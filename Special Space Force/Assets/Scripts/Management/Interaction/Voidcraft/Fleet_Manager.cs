using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public Craft_Show_Script craftShowManager;

    private List<Fleet_Class> fleets;
    private List<Fleet_Script> fleetsS;
    private List<int> fleetIDs;

    public Scrollbar slotFieldScroll;

    public bool menu;
    bool matched = false;
    int selectionExtension = 0;
    Fleet_Script Highest;

    public List<Voidcraft_Script> selectedCraft;

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
        FleetsS.Add(tempF);
        tempF.ID = Fleets.Count + 1;
        Fleets.Add(tempF.fleetClass);
        fleetIDs.Add(tempF.ID);
        tempF.containedCraft = new List<Voidcraft_Script>();
        tempF.MakeFleet(tempF, this);
        tempF.SetPosition(viewedFleet);
        tempF.MakeFleet(tempF, this);
        //slots.Add(slotN1.GetComponent<Fleet_Script>().MasterSaveClass());
    }

    public void TopSlot()
    {
        viewedFleet = null;
        currentName.text = "Fleet Overview";
        foreach(Fleet_Script fs in fleetsS)
        {
            fs.SetPosition(viewedFleet);
            foreach (Voidcraft_Script vs in fs.containedCraft)
            {
                vs.SetPosition(viewedFleet);
            }
        }
        slotFieldScroll.value = 0;
    }

    //Opens the clicked slot
    public void OpenSlot(Fleet_Script newViewed)
    {
        viewedFleet = newViewed;
        currentName.text = newViewed.fleetName;
        foreach (Fleet_Script fs in fleetsS)
        {
            fs.SetPosition(viewedFleet);
            foreach (Voidcraft_Script vs in fs.containedCraft)
            {
                vs.SetPosition(viewedFleet);
            }
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

    //Same as above but without the dropdowns
    public void UIPressed(bool setting)
    {
        menu = setting;
    }

    //If the mouse is pressed and there isnt a menu open, fire the raycast and work out which slot is the highest (the one visible to the user)
    //If there was a trooper image seleted then it selects that trooper and cancels everything else. If a shift is being held, it adds the trooper to the list.
    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0) && !menu)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();
            bool keepSelection = false;

            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

            int highestSlotHeight;
            highestSlotHeight = -2;

            if (results.Count != 0)
            {
                //Checks through the raycast results and find the component with the highest slot height, which is the one visible to the user
                Highest = results[0].gameObject.GetComponent<Fleet_Script>();
            }
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Fleet_Script>() != null)
                {
                    Fleet_Script temp = result.gameObject.GetComponent<Fleet_Script>();
                    highestSlotHeight = -1;
                    Highest = temp;
                    foreach (Voidcraft_Script vs in selectedCraft)
                    {
                        vs.imageManager.TurnOff("selected");
                    }
                    selectedCraft.Clear();
                }
                if (result.gameObject.transform.parent.CompareTag("Craft"))
                {
                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                    {
                        foreach (Voidcraft_Script ts in selectedCraft)
                        {
                            ts.imageManager.TurnOff("selected");
                        }
                        selectedCraft.Clear();
                        Voidcraft_Script tempV = result.gameObject.transform.parent.transform.parent.GetComponent<Voidcraft_Script>();
                        //craftShowManager.ChangeTrooper(tempV); Waiting for UI to complete
                        //modManager.voidcraftManager.SetDropdowns(tempV);
                    }
                    result.gameObject.transform.parent.transform.parent.GetComponent<Voidcraft_Script>().imageManager.TurnOn("selected");
                    Debug.Log(result.gameObject.name);
                    selectedCraft.Add(result.gameObject.transform.parent.transform.parent.GetComponent<Voidcraft_Script>());
                    keepSelection = true;
                    break;
                }
                else if (result.gameObject.transform.parent.CompareTag("UIForce"))
                {
                    Debug.Log("UI Force Detected");
                    keepSelection = true;
                    selectionExtension = 1;
                }
            }

            if (!keepSelection && selectionExtension == 0)
            {
                foreach (Voidcraft_Script ts in selectedCraft)
                {
                    ts.imageManager.TurnOff("selected");
                }
                selectedCraft.Clear();
            }

            if (selectionExtension > 0 && !keepSelection)
            {
                selectionExtension -= 1;
            }

            //If there was a slot in the selection and menu isn't open (double checking incase of slowness on a script's part) open the clicked slot
            if (highestSlotHeight > -2 && !menu)
            {
                OpenSlot(Highest);
            }

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.LogError("Hallo, Debugger Here!");
        }
    }

}
