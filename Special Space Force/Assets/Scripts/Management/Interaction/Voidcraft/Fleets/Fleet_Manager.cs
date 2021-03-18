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
    public Fleet_Script mainFleetView;
    private List<Voidcraft_Class> craft;
    private List<int> fleetIDs;
    private List<int> craftIDs;

    public Scrollbar slotFieldScroll;
    public GameObject FleetOptions;
    public GameObject CraftOptions;

    public Show_Hide_Organisation closer;

    public bool menu;
    bool matched = false;
    int selectionExtension = 0;
    Fleet_Script Highest;

    public List<Voidcraft_Script> selectedCraft;

    public Sound_Script speakerManager;

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

    public List<Voidcraft_Class> Craft
    {
        get { return craft; }

        set
        {
            if (value != craft)
            {
                craft = value;
            }
        }
    }

    public List<int> FleetIDs
    {
        get { return fleetIDs; }

        set
        {
            if (value != fleetIDs)
            {
                fleetIDs = value;
            }
        }
    }

    public List<int> CraftIDs
    {
        get { return craftIDs; }

        set
        {
            if (value != craftIDs)
            {
                craftIDs = value;
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
        craftIDs = new List<int>();
        craft = new List<Voidcraft_Class>();
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
        //slots.Add(slotN1.GetComponent<Fleet_Script>().MasterSaveClass());
    }

    public void TopSlot()
    {
        speakerManager.PlaySound();
        viewedFleet = mainFleetView;
        foreach(Fleet_Script fs in fleetsS)
        {
            fs.SetPosition(viewedFleet);
            foreach (Voidcraft_Script vs in fs.containedCraft)
            {
                vs.SetPosition(viewedFleet);
            }
        }
        slotFieldScroll.value = 0;
        FleetOptions.SetActive(true);
        CraftOptions.SetActive(false);
        currentName.text = viewedFleet.fleetName;
    }

    //Opens the clicked slot
    public void OpenFleet(Fleet_Script newViewed)
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

        Toggle[] cColours = CraftOptions.GetComponentsInChildren<Toggle>();
        foreach (Toggle t in cColours)
        {
            if (t.name == "Squad Colours Toggle")
            {
                if (viewedFleet.fColours == true)
                {
                    t.isOn = true;
                }
                else
                {
                    t.isOn = false;
                }
            }
        }

        FleetOptions.SetActive(false);
        CraftOptions.SetActive(true);
        slotFieldScroll.value = 0;
    }

    //Moves the selected Craft to be a child of the selected Fleet from the dropdown
    public void MoveToFleet(GameObject Dropdown)
    {
        if (Dropdown.GetComponent<Dropdown>().value != 0)
        {
            FindSelectedFleet(Dropdown.GetComponent<Dropdown>());
            Debug.Log("Moving to Squad " + Dropdown.GetComponent<Dropdown>().options[Dropdown.GetComponent<Dropdown>().value].text);
            slotFieldScroll.value = 0;
            foreach (Voidcraft_Script ts in selectedCraft)
            {
                ts.imageManager.TurnOff("selected");
            }
            selectedCraft.Clear();
            OpenFleet(viewedFleet);
        }
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
                if (result.gameObject.CompareTag("Craft"))
                {
                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                    {
                        foreach (Voidcraft_Script ts in selectedCraft)
                        {
                            ts.imageManager.TurnOff("selected");
                        }
                        selectedCraft.Clear();
                        Voidcraft_Script tempV = result.gameObject.transform.parent.transform.parent.GetComponent<Voidcraft_Script>();
                        craftShowManager.ChangeCraft(tempV);
                        //modManager.voidcraftManager.SetDropdowns(tempV); Waiting for UI to complete
                    }
                    if (result.gameObject.name == "Outline")
                    {
                        result.gameObject.transform.parent.transform.parent.GetComponent<Voidcraft_Script>().imageManager.TurnOn("selected");
                    }
                    else
                    {
                        result.gameObject.transform.parent.GetComponent<Voidcraft_Script>().imageManager.TurnOn("selected");
                    }
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
                    break;
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
                speakerManager.PlaySound();
                OpenFleet(Highest);
            }

        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.LogError("Hallo, Debugger Here!");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (viewedFleet == null || viewedFleet == mainFleetView)
            {
                closer.ShowHide();
            }
            else
            {
                speakerManager.PlaySound();
                TopSlot();
            }
        }
    }


    //Finds the selected string from the dropdown menu
    public void FindSelectedFleet(Dropdown dropdown)
    {
        foreach (Fleet_Script fleet in fleetsS)
        {
            CheckFleet(fleet, dropdown.options[dropdown.value].text, dropdown);
        }
    }

    //Used to transfer craft between fleets. 
    private void CheckFleet(Fleet_Script fleet, string name, Dropdown dropdown)
    {
        //Remove the dashes that help the user determine children
        string dash = "-";
        string noDashes;

        noDashes = name.Replace(dash, "");

        //Sends and sets details of the slot and its new parent, then saves all slots
        if (noDashes == fleet.fleetName)
        {
            if (dropdown.GetComponent<Slot_Button>().ids[dropdown.value] == fleet.fleetClass.uID)
            {
                foreach (Voidcraft_Script vs in selectedCraft)
                {
                    if (fleet.containedCraft.Count <= 19)
                    {
                        fleet.gameObject.SetActive(true);
                        vs.gameObject.transform.parent = fleet.gameObject.transform;
                        vs.craftClass.positionID = fleet.containedCraft.Count + 1;
                        vs.craftFleet = fleet;

                        fleet.containedCraft.Add(vs);
                        viewedFleet.containedCraft.Remove(vs);
                        fleet.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Squad Full - " + fleet.fleetName);
                    }
                }
                int counter = 0;
                foreach (Voidcraft_Script vs in viewedFleet.containedCraft)
                {
                    counter += 1;
                    vs.craftClass.positionID = counter;
                }
                //fleet = new List<Slot_Class>();
                //fleet.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
            }
        }
    }

    //Sets 
    public void ToggleUsedColors(Toggle toggle)
    {
        if (toggle.isOn == true)
        {
            viewedFleet.fColours = true;
            viewedFleet.fleetClass.useFleetColours = true;
            foreach (Voidcraft_Script vs in viewedFleet.containedCraft)
            {
                vs.craftImages[1].color = viewedFleet.fleetClass.fleetColours[0];
                vs.craftImages[2].color = viewedFleet.fleetClass.fleetColours[1];
                vs.craftImages[3].color = viewedFleet.fleetClass.fleetColours[2];
                vs.craftImages[4].color = viewedFleet.fleetClass.fleetColours[3];
                vs.craftImages[5].color = viewedFleet.fleetClass.fleetColours[4];
            }
        }
        else
        {
            viewedFleet.fColours = false;
            viewedFleet.fleetClass.useFleetColours = false;
            foreach (Voidcraft_Script vs in viewedFleet.containedCraft)
            {
                vs.craftImages[1].color = manager.modManager.GeneratedProduct.playerFleetColours[0];
                vs.craftImages[2].color = manager.modManager.GeneratedProduct.playerFleetColours[1];
                vs.craftImages[3].color = manager.modManager.GeneratedProduct.playerFleetColours[2];
                vs.craftImages[4].color = manager.modManager.GeneratedProduct.playerFleetColours[3];
                vs.craftImages[5].color = manager.modManager.GeneratedProduct.playerFleetColours[4];
            }
        }
    }

    public void SetName(Text textBox)
    {
        viewedFleet.SetName(textBox.text);
    }
}
