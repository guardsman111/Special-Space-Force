using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Manager : MonoBehaviour
{
    /// <summary>
    /// This Script manages all interactions between the slots presented on screen to the player.
    /// </summary>
    public Galaxy_Generation_Manager manager;
    public Manager_Script modManager;
    public Slot_Generator generator;
    public GraphicRaycaster raycaster;

    public GameObject prefabSlot;
    public GameObject prefabTrooper;
    public Slot_Script viewedSlot;
    public Slot_Class viewedSlotClass;
    public GameObject content;
    public InputField currentName;

    private List<Slot_Class> slots;
    public List<int> slotIDs;

    public GameObject slotN1;

    public Slot_Script Highest;

    public Slot_Button moveToDropdown;
    public Slot_Button transferDropdown;

    public Scrollbar slotFieldScroll;

    public Slider slotSlider;
    public GameObject slotOptions;
    public GameObject squadOptions;
    public Dropdown slotRoleDropdown;

    public Trooper_Show_Script trooperShowManager;
    public Promote_Script promoter;

    public bool menu;
    bool matched = false;
    int selectionExtension = 0;

    public Sprite_Pack trooperSkinPack;
    public Sprite[] trooperSkinSprites;
    public Sprite_Pack maleHairPack;
    public Sprite_Pack maleHairOutlinePack;
    public Sprite[] maleHairSprites;
    public Sprite[] maleHairOutlineSprites;
    public Sprite_Pack femaleHairPack;
    public Sprite_Pack femaleHairOutlinePack;
    public Sprite[] femaleHairSprites;
    public Sprite[] femaleHairOutlineSprites;

    public Color32[] hairColours;


    public List<Trooper_Script> selectedTroopers;

    public int nTroopers;

    public List<Slot_Class> Slots
    {
        get { return slots; }

        set
        {
            if (value != slots)
            {
                slots = value;
            }
        }
    }

    //Gets the graphics raycaster
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        menu = false;
        trooperSkinPack = new Sprite_Pack();
        maleHairPack = new Sprite_Pack();
        femaleHairPack = new Sprite_Pack();
        maleHairOutlinePack = new Sprite_Pack();
        femaleHairOutlinePack = new Sprite_Pack();
        trooperSkinPack.containedSprites = new List<Sprite>();
        maleHairPack.containedSprites = new List<Sprite>();
        femaleHairPack.containedSprites = new List<Sprite>();
        maleHairOutlinePack.containedSprites = new List<Sprite>();
        femaleHairOutlinePack.containedSprites = new List<Sprite>();
        slotIDs = new List<int>();

        foreach (Sprite s in trooperSkinSprites)
        {
            trooperSkinPack.packName = "Trooper Skins";
            trooperSkinPack.packName = "Skins";
            trooperSkinPack.containedSprites.Add(s);
        }

        foreach (Sprite s in maleHairSprites)
        {
            maleHairPack.packName = "Male Hair";
            maleHairPack.packType = "Hair";
            maleHairPack.containedSprites.Add(s);
        }

        foreach (Sprite s in maleHairOutlineSprites)
        {
            maleHairOutlinePack.packName = "Male Hair Outline";
            maleHairOutlinePack.packType = "Hair Outline";
            maleHairOutlinePack.containedSprites.Add(s);
        }

        foreach (Sprite s in femaleHairSprites)
        {
            femaleHairPack.packName = "Female Hair";
            femaleHairPack.packType = "Hair";
            femaleHairPack.containedSprites.Add(s);
        }

        foreach (Sprite s in femaleHairOutlineSprites)
        {
            femaleHairOutlinePack.packName = "Female Hair Outline";
            femaleHairOutlinePack.packType = "Hair Outline";
            femaleHairOutlinePack.containedSprites.Add(s);
        }

        nTroopers = 0;
    }


    //Creates a new Slot below the viewed slot
    public void NewSlotTop()
    {
        if (viewedSlot.containedSlots.Count < 9)
        {
            GameObject temp = Instantiate(prefabSlot, viewedSlot.transform);
            Slot_Script tempS = temp.GetComponent<Slot_Script>();
            tempS.input.text = "New Slot";
            tempS.slotName = "New Slot";
            tempS.slotClass = new Slot_Class();
            tempS.slotClass.slotHeight = viewedSlot.slotClass.slotHeight + 1;
            tempS.slotClass.positionID = viewedSlot.slotClass.containedSlots.Count + 1;
            tempS.ID = tempS.slotClass.positionID;
            tempS.slotClass.containedSlots = new List<Slot_Class>();
            tempS.slotClass.containedTroopers = new List<Trooper_Class>();
            tempS.squad = false;
            tempS.squadRole = modManager.rankManager.squadRoles[0];
            tempS.MakeSlot(tempS, viewedSlot, this);
            viewedSlot.slotClass.containedSlots.Add(tempS.slotClass);
            tempS.SetPosition(viewedSlot, 0, viewedSlot);
        }
        else
        {
            Debug.Log("Maximum slots reached");
        }
    }

    //Creates a new squad below the viewed slot
    public void NewSquadTop()
    {
        if (viewedSlot.containedSlots.Count < 9)
        {
            GameObject temp = Instantiate(prefabSlot, viewedSlot.transform);
            Slot_Script tempS = temp.GetComponent<Slot_Script>();
            tempS.input.text = "New Slot";
            tempS.slotName = "New Slot";
            tempS.slotClass = new Slot_Class();
            tempS.slotClass.slotHeight = viewedSlot.slotClass.slotHeight + 1;
            tempS.slotClass.positionID = viewedSlot.slotClass.containedSlots.Count + 1;
            tempS.ID = tempS.slotClass.positionID;
            tempS.slotClass.containedSlots = new List<Slot_Class>();
            tempS.slotClass.containedTroopers = new List<Trooper_Class>();
            tempS.slotClass.squad = true;
            tempS.squad = true;
            tempS.squadRole = modManager.rankManager.squadRoles[0];
            tempS.MakeSlot(tempS, viewedSlot, this);
            viewedSlot.slotClass.containedSlots.Add(tempS.slotClass);
            tempS.SetPosition(viewedSlot, 0, viewedSlot);
        }
        else
        {
            Debug.Log("Maximum slots reached");
        }
    }

    //Creates a new squad below the given slot, provided that slot doesn't have 9 squads already
    public void NewSlot(Slot_Script newParent)
    {
        if (viewedSlot.containedSlots.Count < 9)
        {
            Debug.Log("new Slot " + newParent.slotName);
            GameObject temp = Instantiate(prefabSlot, newParent.transform);
            Slot_Script tempS = temp.GetComponent<Slot_Script>();
            tempS.input.text = "New Slot";
            tempS.slotName = "New Slot";
            tempS.slotClass = new Slot_Class();
            tempS.slotClass.slotHeight = newParent.slotClass.slotHeight + 1;
            tempS.slotClass.positionID = newParent.slotClass.containedSlots.Count + 1;
            tempS.ID = tempS.slotClass.positionID;
            tempS.slotClass.containedSlots = new List<Slot_Class>();
            tempS.slotClass.containedTroopers = new List<Trooper_Class>();
            tempS.squad = false;
            tempS.squadRole = modManager.rankManager.squadRoles[0];
            tempS.MakeSlot(tempS, newParent, this);
            newParent.slotClass.containedSlots.Add(tempS.slotClass);
            tempS.SetPosition(newParent, 0, viewedSlot);
            OpenSlot(viewedSlot);
        }
        else
        {
            Debug.Log("Maximum slots reached");
        }
    }

    //Deletes the passed slot and removes it from its parent
    public void DeleteSlot(Slot_Script slot)
    {
        Debug.Log("delete " + slot.slotName);
        foreach (Slot_Script ss in slot.slotParent.containedSlots)
        {
            if (ss.slotClass.positionID >= slot.ID)
            {
                ss.slotClass.positionID -= 1;
            }
        }
        slot.slotParent.slotClass.containedSlots.Remove(slot.slotClass);
        Slot_Script slotParent = slot.slotParent;
        Destroy(slot.gameObject);
        OpenSlot(viewedSlot);
    }

    //Opens the clicked slot
    public void OpenSlot(Slot_Script newViewed)
    {
        viewedSlotClass = newViewed.slotClass;
        while(viewedSlot.containedSlots.Count > 0)
        {
            Destroy(viewedSlot.containedSlots[0].gameObject);
            viewedSlot.containedSlots.RemoveAt(0);
        }

        if(viewedSlot.gameObject != slotN1)
        {
            Destroy(viewedSlot.gameObject);
        }

        GameObject tempS = Instantiate(prefabSlot, slotN1.transform);
        viewedSlot = tempS.GetComponent<Slot_Script>();

        if (viewedSlotClass != null)
        {
            viewedSlot.LoadSlot(viewedSlotClass, viewedSlotClass.positionID, this);
        } 
        else
        {
            viewedSlot = newViewed;
        }
        currentName.text = newViewed.slotName;
        viewedSlot.SetPosition(viewedSlot, viewedSlot, 0);
        slotN1.GetComponent<Slot_Script>().SetPosition(viewedSlot, viewedSlot);

        if (viewedSlot.squad == true)
        {
            viewedSlot.containedTroopers = new List<Trooper_Script>();
            foreach (Trooper_Class tc in viewedSlot.slotClass.containedTroopers)
            {
                GameObject tempT = Instantiate(prefabTrooper, viewedSlot.transform);
                Trooper_Script tempTS = tempT.GetComponent<Trooper_Script>();

                tempTS.LoadTrooper(tc, this, viewedSlot);
                viewedSlot.containedTroopers.Add(tempTS);
                tempTS.SetPosition(viewedSlot, viewedSlot);
            }

            if (viewedSlot.containedTroopers.Count != 0)
            {
                slotSlider.interactable = false;
            }
            else
            {
                slotSlider.interactable = true;
            }

            slotSlider.value = 1;
            squadOptions.SetActive(true);
            slotOptions.SetActive(false);
            Toggle[] cColours = squadOptions.GetComponentsInChildren<Toggle>();
            foreach(Toggle t in cColours)
            {
                if(t.name == "Squad Colours Toggle")
                {
                    if (viewedSlot.cColours == true)
                    {
                        t.isOn = true;
                    }
                    else 
                    {
                        t.isOn = false;
                    }
                }
            }
            promoter.SetupRankDropdown();
            slotRoleDropdown.value = modManager.rankManager.squadRoles.IndexOf(viewedSlot.squadRole);
            if (viewedSlot.containedTroopers.Count > 0)
            {
                trooperShowManager.ChangeTrooper(viewedSlot.containedTroopers[0]);
            }
        }
        else
        {
            viewedSlot.containedSlots = new List<Slot_Script>();

            if (viewedSlot.slotClass != null)
            {
                foreach (Slot_Class sc in viewedSlot.slotClass.containedSlots)
                {
                    GameObject temp = Instantiate(prefabSlot, viewedSlot.transform);
                    Slot_Script tempSS = temp.GetComponent<Slot_Script>();

                    tempSS.LoadSlot(sc, sc.positionID, this);
                    viewedSlot.containedSlots.Add(tempSS);
                    tempSS.slotParent = viewedSlot;
                    if (tempSS.squad == false)
                    {
                        foreach (Slot_Class sc2 in tempSS.slotClass.containedSlots)
                        {
                            GameObject temp2 = Instantiate(prefabSlot, tempSS.transform);
                            Slot_Script tempSS2 = temp2.GetComponent<Slot_Script>();

                            tempSS2.LoadSlot(sc2, sc2.positionID, this);
                            tempSS.containedSlots.Add(tempSS2);
                            tempSS2.slotParent = tempSS;
                            if (tempSS2.squad == false)
                            {
                                foreach (Slot_Class sc3 in tempSS2.slotClass.containedSlots)
                                {
                                    GameObject temp3 = Instantiate(prefabSlot, tempSS2.transform);
                                    Slot_Script tempSS3 = temp3.GetComponent<Slot_Script>();

                                    tempSS3.LoadSlot(sc3, sc3.positionID, this);
                                    tempSS2.containedSlots.Add(tempSS3);
                                    tempSS3.slotParent = tempSS2;
                                }
                            }
                            else
                            {
                                tempSS2.containedTroopers = new List<Trooper_Script>();
                                GameObject tempT = Instantiate(prefabTrooper, tempSS2.transform);
                                foreach (Trooper_Class tc in tempSS2.slotClass.containedTroopers)
                                {
                                    Trooper_Script tempTS = tempT.GetComponent<Trooper_Script>();

                                    tempTS.LoadTrooper(tc, this, tempSS2);
                                    tempSS2.containedTroopers.Add(tempTS);
                                    tempTS.SetPosition(tempSS2, viewedSlot);
                                }
                            }
                        }
                    }
                    else
                    {
                        tempSS.containedTroopers = new List<Trooper_Script>();
                        foreach (Trooper_Class tc in tempSS.slotClass.containedTroopers)
                        {
                            GameObject tempT = Instantiate(prefabTrooper, tempSS.transform);
                            Trooper_Script tempTS = tempT.GetComponent<Trooper_Script>();

                            tempTS.LoadTrooper(tc, this, tempSS);
                            tempSS.containedTroopers.Add(tempTS);
                            tempTS.SetPosition(tempSS, viewedSlot);
                        }
                    }
                }
            } 
            else
            {
                foreach (Slot_Class sc in manager.save.topSlots)
                {
                    GameObject temp = Instantiate(prefabSlot, content.transform);
                    Slot_Script tempSS = temp.GetComponent<Slot_Script>();

                    tempSS.LoadSlot(sc, sc.positionID, this);
                    viewedSlot.containedSlots.Add(tempSS);
                }
            }

            if (viewedSlot.containedSlots.Count != 0)
            {
                slotSlider.interactable = false;
            }
            else
            {
                slotSlider.interactable = true;
            }

            foreach (Slot_Script ss in viewedSlot.containedSlots)
            {
                ss.SetPosition(viewedSlot, 0, viewedSlot);
                foreach (Slot_Script ss2 in ss.containedSlots)
                {
                    ss2.SetPosition(ss, 1, viewedSlot);
                    foreach (Slot_Script ss3 in ss2.containedSlots)
                    {
                        ss3.SetPosition(ss2, 2, viewedSlot);
                    }
                }
            }
            slotSlider.value = 0;
            squadOptions.SetActive(false);
            slotOptions.SetActive(true);
        }
        slotFieldScroll.value = 0;
    }

    //Returns to the parent of the viewed slot
    public void UpSlot()
    {
        GetSlotAbove(slots[0], viewedSlot.slotClass);
    }

    //
    private void GetSlotAbove(Slot_Class checkingSlot, Slot_Class openSlot)
    {
        Slot_Class slot = new Slot_Class();

        for (int i = 0; i < checkingSlot.containedSlots.Count; i++)
        {
            if (checkingSlot.containedSlots[i].uID == openSlot.uID)
            {
                GameObject tempS = Instantiate(prefabSlot, slotN1.transform);
                Slot_Script ss = tempS.GetComponent<Slot_Script>();
                ss.LoadSlot(checkingSlot, checkingSlot.positionID, this);
                OpenSlot(ss);
                Destroy(tempS);
            }
            else
            {
                GetSlotAbove(checkingSlot.containedSlots[i], openSlot);
            }
        }

    }

    //Returns to the top slot
    public void TopSlot()
    {
        slotN1.GetComponent<Slot_Script>().SetName(slots[0].slotName);
        OpenSlot(slotN1.GetComponent<Slot_Script>());
        slotFieldScroll.value = 0;
        slotSlider.value = 0;
        slotSlider.interactable = false;
        squadOptions.SetActive(false);
        slotOptions.SetActive(true);
        DeselectTroopers();
    }

    //Sets the name of the slot
    public void SetName(Text textBox)
    {
        viewedSlot.SetName(textBox.text);
    }

    public void SetDropdown()
    {
        moveToDropdown.SetDropdownSlot();
        transferDropdown.SetDropdownSlot();
    }

    //Moves the Slot to be a child of the selected slot from the dropdown
    public void MoveToSlot(GameObject Dropdown)
    {
        if (Dropdown.GetComponent<Dropdown>().value != 0)
        {
            FindSelected(Dropdown.GetComponent<Dropdown>());
            Debug.Log("Moving to Slot " + Dropdown.GetComponent<Dropdown>().options[Dropdown.GetComponent<Dropdown>().value].text);
            slotFieldScroll.value = 0;
            Dropdown.GetComponent<Dropdown>().value = 0;
        }
    }

    //Moves the selected troopers to be a child of the selected slot from the dropdown
    public void MoveToSquad(GameObject Dropdown)
    {
        if (Dropdown.GetComponent<Dropdown>().value != 0)
        {
            FindSelectedSquad(Dropdown.GetComponent<Dropdown>());
            Debug.Log("Moving to Squad " + Dropdown.GetComponent<Dropdown>().options[Dropdown.GetComponent<Dropdown>().value].text);
            slotFieldScroll.value = 0;
            foreach (Trooper_Script ts in selectedTroopers)
            {
                ts.imageManager.TurnOff("selected");
            }
            selectedTroopers.Clear();
            OpenSlot(viewedSlot);
        }
    }

    //Registers opening of the menu to prevent users from clicking through it
    public void MovingSlot(bool setting)
    {
        moveToDropdown.SetDropdownSlot();
        transferDropdown.SetDropdownSquad();
        menu = setting;
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
                Highest = results[0].gameObject.GetComponent<Slot_Script>();
            }
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Slot_Script>() != null)
                {
                    Slot_Script temp = result.gameObject.GetComponent<Slot_Script>();
                    if (temp.slotHeight > highestSlotHeight)
                    {
                        Highest = temp;
                        highestSlotHeight = temp.slotHeight;
                    }
                    foreach (Trooper_Script ts in selectedTroopers)
                    {
                        if (ts.imageManager != null)
                        {
                            ts.imageManager.TurnOff("selected");
                        }
                    }
                    selectedTroopers.Clear();
                    break;
                }
                if (result.gameObject.transform.parent.CompareTag("Trooper"))
                {
                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                    {
                        foreach (Trooper_Script ts in selectedTroopers)
                        {
                            if (ts.imageManager != null)
                            {
                                ts.imageManager.TurnOff("selected");
                            }
                        }
                        selectedTroopers.Clear();
                        Trooper_Script tempT = result.gameObject.transform.parent.transform.parent.GetComponent<Trooper_Script>();
                        trooperShowManager.ChangeTrooper(tempT);
                        modManager.equipmentManager.SetDropdowns(tempT);
                        promoter.ChangeRank(tempT.trooperRank);
                    }
                    result.gameObject.transform.parent.transform.parent.GetComponent<Trooper_Script>().imageManager.TurnOn("selected");
                    Debug.Log(result.gameObject.name);
                    selectedTroopers.Add(result.gameObject.transform.parent.transform.parent.GetComponent<Trooper_Script>());
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
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    ts.imageManager.TurnOff("selected");
                }
                selectedTroopers.Clear();
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

    //Finds the selected string from the dropdown menu
    public void FindSelected(Dropdown dropdown)
    {
        CheckSlot(slotN1.GetComponent<Slot_Script>().slotClass, dropdown.options[dropdown.value].text, dropdown);
    }

    //Finds the selected string from the dropdown menu
    public void FindSelectedSquad(Dropdown dropdown)
    {
        CheckSquad(slotN1.GetComponent<Slot_Script>().slotClass, dropdown.options[dropdown.value].text, dropdown);
    }

    //Converts the dropdown string into a returnable slot and sends the viewed slot to that slot
    //Cannot handle duplicate names, just finds the first name in the list that has that name. need to use some ID's or something
    private void CheckSlot(Slot_Class slotS, string name, Dropdown dropdown)
    {
        //Remove the dashes that help the user determine children
        string dash = "-";
        string noDashes;

        noDashes = name.Replace(dash, "");

        Slot_Class slot = new Slot_Class();

        for (int i = 0; i < slotS.containedSlots.Count; i++)
        {
            if (slotS.containedSlots[i].slotName == noDashes)
            {
                GameObject tempS = Instantiate(prefabSlot, slotN1.transform);
                Slot_Script ss = tempS.GetComponent<Slot_Script>();
                ss.LoadSlot(slotS.containedSlots[i], slotS.containedSlots[i].positionID, this);
                DeleteFromParent(slots[0], viewedSlot.slotClass);
                ss.slotClass.containedSlots.Add(viewedSlot.slotClass);
                viewedSlot.slotClass.positionID = ss.slotClass.containedSlots.Count;
                viewedSlot.ChangeHeight(slot.slotHeight);
                OpenSlot(viewedSlot);
                Destroy(tempS);
            }
            else
            {
                if (slotS.containedSlots[i] == viewedSlot.slotClass)
                {
                    Debug.Log("Attempting to prevent moving to children");
                }
                else
                {
                    CheckSlot(slotS.containedSlots[i], name, dropdown);
                }
            }
        }

    }

    //Converts the dropdown string into a returnable slot and sends the viewed slot to that slot
    //Cannot handle duplicate names, just finds the first name in the list that has that name. need to use some ID's or something
    private void DeleteFromParent(Slot_Class checkingSlot, Slot_Class openSlot)
    {
        Slot_Class slot = new Slot_Class();

        for (int i = 0; i < checkingSlot.containedSlots.Count; i++)
        {
            if (checkingSlot.containedSlots[i].uID == openSlot.uID)
            {
                checkingSlot.containedSlots.RemoveAt(i);
                break;
            }
            else
            {
                DeleteFromParent(checkingSlot.containedSlots[i], openSlot);
            }
        }

    }

    //Same as checkslot, but for transfering troopers. 
    private void CheckSquad(Slot_Class slot, string name, Dropdown dropdown)
    {
        //Remove the dashes that help the user determine children
        string dash = "-";
        string noDashes;

        noDashes = name.Replace(dash, "");

        for (int i = 0; i < slot.containedSlots.Count; i++)
        {
            if (slot.containedSlots[i].slotName == noDashes)
            {
                GameObject tempS = Instantiate(prefabSlot, slotN1.transform);
                Slot_Script ss = tempS.GetComponent<Slot_Script>();
                ss.LoadSlot(slot.containedSlots[i], slot.containedSlots[i].positionID, this);
                DeleteFromParentSquad(viewedSlot.slotClass);
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    ts.trooperClass.trooperPosition = ss.slotClass.containedTroopers.Count + 1;
                    ss.slotClass.containedTroopers.Add(ts.trooperClass);
                }
                Destroy(tempS);
            }
            else
            {
                if (slot.containedSlots[i] == viewedSlot.slotClass)
                {
                    Debug.Log("Attempting to prevent moving to children");
                }
                else
                {
                    CheckSquad(slot.containedSlots[i], name, dropdown);
                }
            }
        }
    }

    //Converts the dropdown string into a returnable slot and sends the viewed slot to that slot
    //Cannot handle duplicate names, just finds the first name in the list that has that name. need to use some ID's or something
    private void DeleteFromParentSquad(Slot_Class openSlot)
    {
        Slot_Class slot = new Slot_Class();

        foreach (Trooper_Script ts in selectedTroopers)
        {
            int index = openSlot.containedTroopers.IndexOf(ts.trooperClass);
            openSlot.containedTroopers.RemoveAt(index);
        }

        for(int i = 0; i < openSlot.containedTroopers.Count; i++)
        {
            openSlot.containedTroopers[i].trooperPosition = i + 1;
        }
    }

    //Turns the slot into a squad 
    public void SetSquad(Slider slider)
    {
        if (slider.value == 1)
        {
            if (viewedSlot.containedSlots.Count == 0)
            {
                viewedSlot.slotClass.squad = true;
                viewedSlot.squad = true;
                slotOptions.SetActive(false);
                squadOptions.SetActive(true);
            }
            else
            {
                Debug.Log("Contains Slots");
                slider.value = 0;
                slider.interactable = false;
            }
        }
        else
        {
            if (viewedSlot.containedTroopers.Count == 0)
            {
                viewedSlot.squad = false;
                viewedSlot.slotClass.squad = false;
                squadOptions.SetActive(false);
                slotOptions.SetActive(true);
            }
            else
            {
                Debug.Log("Contains Troopers");
                slider.value = 1;
                slider.interactable = false;
            }
        }
    }

    //Sets the squad role according to the passed dropdown
    public void SetRole(Dropdown dropdown)
    {
        viewedSlot.squadRole = modManager.rankManager.squadRoles[dropdown.value];
        promoter.SetupRankDropdown();
    }

    //Changes the number of troopers 
    public void ChangeTroopers(int change)
    {
        nTroopers += change;
    }

    //Returns the number of troopers in the slot
    public string GetTroopers()
    {
        string number;

        number = nTroopers.ToString();
        return number;
    }

    //Deselects the troopers (turns off the background selected image)
    public void DeselectTroopers()
    {
        foreach (Trooper_Script ts in selectedTroopers)
        {
            ts.imageManager.TurnOff("selected");
        }
        selectedTroopers.Clear();
    }

    public void ToggleUsedColors(Toggle toggle)
    {
        if(toggle.isOn == true)
        {
            viewedSlot.cColours = true;
            viewedSlot.slotClass.useSquadColours = true;
            foreach (Trooper_Script ts in viewedSlot.containedTroopers)
            {
                ts.trooperImages[17].color = viewedSlot.slotClass.squadColours[0];
                ts.trooperImages[18].color = viewedSlot.slotClass.squadColours[1];
                ts.trooperImages[19].color = viewedSlot.slotClass.squadColours[2];
                ts.trooperImages[20].color = viewedSlot.slotClass.squadColours[3];
                ts.trooperImages[10].color = viewedSlot.slotClass.squadColours[4];
                ts.trooperImages[11].color = viewedSlot.slotClass.squadColours[5];
                ts.trooperImages[12].color = viewedSlot.slotClass.squadColours[6];
                ts.trooperImages[13].color = viewedSlot.slotClass.squadColours[7];
                ts.trooperImages[14].color = viewedSlot.slotClass.squadColours[8];
                ts.trooperImages[4].color = viewedSlot.slotClass.squadColours[9];
                ts.trooperImages[5].color = viewedSlot.slotClass.squadColours[10];
                ts.trooperImages[6].color = viewedSlot.slotClass.squadColours[11];
                ts.trooperImages[7].color = viewedSlot.slotClass.squadColours[12];
                ts.trooperImages[8].color = viewedSlot.slotClass.squadColours[13];
                ts.trooperImages[27].color = viewedSlot.slotClass.squadColours[14];
                ts.trooperImages[28].color = viewedSlot.slotClass.squadColours[15];
                ts.trooperImages[24].color = viewedSlot.slotClass.squadColours[16];
                ts.trooperImages[25].color = viewedSlot.slotClass.squadColours[17];
                ts.trooperImages[30].color = viewedSlot.slotClass.squadColours[14];
                ts.trooperImages[31].color = viewedSlot.slotClass.squadColours[15];
                foreach(Text t in ts.slotLocations)
                {
                    t.color = viewedSlot.slotClass.squadColours[18];
                }
            }
        }
        else
        {
            viewedSlot.cColours = false;
            viewedSlot.slotClass.useSquadColours = false;
            foreach (Trooper_Script ts in viewedSlot.containedTroopers)
            {
                ts.trooperImages[17].color = manager.modManager.GeneratedProduct.playerColours[10];
                ts.trooperImages[18].color = manager.modManager.GeneratedProduct.playerColours[11];
                ts.trooperImages[19].color = manager.modManager.GeneratedProduct.playerColours[12];
                ts.trooperImages[20].color = manager.modManager.GeneratedProduct.playerColours[13];
                ts.trooperImages[10].color = manager.modManager.GeneratedProduct.playerColours[0];
                ts.trooperImages[11].color = manager.modManager.GeneratedProduct.playerColours[1];
                ts.trooperImages[12].color = manager.modManager.GeneratedProduct.playerColours[2];
                ts.trooperImages[14].color = manager.modManager.GeneratedProduct.playerColours[3];
                ts.trooperImages[13].color = manager.modManager.GeneratedProduct.playerColours[4];
                ts.trooperImages[4].color = manager.modManager.GeneratedProduct.playerColours[5];
                ts.trooperImages[5].color = manager.modManager.GeneratedProduct.playerColours[6];
                ts.trooperImages[6].color = manager.modManager.GeneratedProduct.playerColours[7];
                ts.trooperImages[8].color = manager.modManager.GeneratedProduct.playerColours[8];
                ts.trooperImages[7].color = manager.modManager.GeneratedProduct.playerColours[9];
                ts.trooperImages[27].color = manager.modManager.GeneratedProduct.playerColours[14];
                ts.trooperImages[28].color = manager.modManager.GeneratedProduct.playerColours[15];
                ts.trooperImages[24].color = manager.modManager.GeneratedProduct.playerColours[16];
                ts.trooperImages[25].color = manager.modManager.GeneratedProduct.playerColours[17];
                ts.trooperImages[30].color = manager.modManager.GeneratedProduct.playerColours[14];
                ts.trooperImages[31].color = manager.modManager.GeneratedProduct.playerColours[15];
                foreach (Text t in ts.slotLocations)
                {
                    t.color = manager.modManager.GeneratedProduct.playerColours[18];
                }
            }
        }
    }


    public void MoveSlotLocation(Slot_Class slot, string newLocT, int newID, int planetNumber)
    {
        if (newLocT == "Craft")
        {
            slot.craftID = newID;
            slot.systemID = 0;
        }
        else if (newLocT == "Planet")
        {
            slot.craftID = 0;
            slot.systemID = newID;
            slot.planetN = planetNumber;
        }
    }
}
