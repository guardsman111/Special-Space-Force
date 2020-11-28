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
    public GameObject prefabSlot;
    public Slot_Script viewedSlot;
    public List<Slot_Class> slots;
    public GameObject slotN1;
    public GraphicRaycaster raycaster;
    public Slot_Script Highest;
    public Slot_Button moveToDropdown;
    public Slot_Button transferDropdown;
    public InputField currentName;
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
    public Sprite[] maleHairSprites;
    public Sprite_Pack femaleHairPack;
    public Sprite[] femaleHairSprites;

    public Color32[] hairColours;


    public List<Trooper_Script> selectedTroopers;

    public int nTroopers;

    //Gets the graphics raycaster
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        menu = false;
        trooperSkinPack = new Sprite_Pack();
        maleHairPack = new Sprite_Pack();
        femaleHairPack = new Sprite_Pack();
        trooperSkinPack.containedSprites = new List<Sprite>();
        maleHairPack.containedSprites = new List<Sprite>();
        femaleHairPack.containedSprites = new List<Sprite>();

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

        foreach (Sprite s in femaleHairSprites)
        {
            femaleHairPack.packName = "Female Hair";
            femaleHairPack.packType = "Hair";
            femaleHairPack.containedSprites.Add(s);
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
            tempS.slotHeight = viewedSlot.slotHeight + 1;
            tempS.ID = viewedSlot.containedSlots.Count + 1;
            tempS.containedSlots = new List<Slot_Script>();
            tempS.containedTroopers = new List<Trooper_Script>();
            tempS.squad = false;
            tempS.squadRole = 0;
            tempS.MakeSlot(tempS, viewedSlot, this);
            tempS.SetPosition(slotN1.GetComponent<Slot_Script>(), viewedSlot);
            viewedSlot.containedSlots.Add(tempS);
            slots = new List<Slot_Class>();
            slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
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
            tempS.input.text = "New Squad";
            tempS.slotName = "New Squad";
            tempS.slotHeight = viewedSlot.slotHeight + 1;
            tempS.ID = viewedSlot.containedSlots.Count + 1;
            tempS.squad = true;
            tempS.squadRole = 0;
            tempS.containedSlots = new List<Slot_Script>();
            tempS.containedTroopers = new List<Trooper_Script>();
            tempS.MakeSlot(tempS, viewedSlot, this);
            tempS.SetPosition(slotN1.GetComponent<Slot_Script>(), viewedSlot);
            viewedSlot.containedSlots.Add(tempS);
            slots = new List<Slot_Class>();
            slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
        }
        else
        {
            Debug.Log("Maximum slots reached");
        }
    }

    //Creates a new squad below the given slot, provided that slot doesn't have 9 squads already
    public void NewSlot(Slot_Script newParent)
    {
        if (newParent.containedSlots.Count < 9)
        {
            Debug.Log("new Slot " + newParent.slotName);
            GameObject temp = Instantiate(prefabSlot, newParent.transform);
            Slot_Script tempS = temp.GetComponent<Slot_Script>();
            tempS.input.text = "New Slot";
            tempS.slotName = "New Slot";
            tempS.slotHeight = newParent.slotHeight + 1;
            tempS.ID = newParent.containedSlots.Count + 1;
            tempS.containedSlots = new List<Slot_Script>();
            tempS.containedTroopers = new List<Trooper_Script>();
            tempS.squad = false;
            tempS.squadRole = 0;
            tempS.MakeSlot(tempS, newParent, this);
            newParent.containedSlots.Add(tempS);
            OpenSlot(viewedSlot);
            slots = new List<Slot_Class>();
            slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
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
            if (ss.ID >= slot.ID)
            {
                ss.ID -= 1;
            }
        }
        slot.slotParent.containedSlots.Remove(slot);
        Slot_Script slotParent = slot.slotParent;
        Destroy(slot.gameObject);
        OpenSlot(viewedSlot);
        slots = new List<Slot_Class>();
        slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
    }

    //Opens the clicked slot
    public void OpenSlot(Slot_Script newViewed)
    {
        viewedSlot = newViewed;
        currentName.text = newViewed.slotName;

        if (viewedSlot.squad == true)
        {
            if (viewedSlot.containedTroopers.Count != 0)
            {
                slotSlider.interactable = false;
            }
            else
            {
                slotSlider.interactable = true;
            }
            foreach (Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
            {
                ss.SetPosition(slotN1.GetComponent<Slot_Script>(), slotN1.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
            }
            slotSlider.value = 1;
            squadOptions.SetActive(true);
            slotOptions.SetActive(false);
            slotRoleDropdown.value = viewedSlot.squadRole;
        }
        else
        {
            if (viewedSlot.containedSlots.Count != 0)
            {
                slotSlider.interactable = false;
            }
            else
            {
                slotSlider.interactable = true;
            }
            foreach (Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
            {
                ss.SetPosition(slotN1.GetComponent<Slot_Script>(), slotN1.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
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
        viewedSlot = viewedSlot.slotParent;
        currentName.text = viewedSlot.slotName;
        foreach (Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
        {
            ss.SetPosition(slotN1.GetComponent<Slot_Script>(), viewedSlot.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
        }
        slotSlider.value = 0;
        slotSlider.interactable = false;
        squadOptions.SetActive(false);
        slotOptions.SetActive(true);
        DeselectTroopers();
    }

    //Returns to the top slot
    public void TopSlot()
    {
        viewedSlot = slotN1.GetComponent<Slot_Script>();
        currentName.text = viewedSlot.slotName;
        foreach (Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
        {
            ss.SetPosition(slotN1.GetComponent<Slot_Script>(), slotN1.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
        }
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

    //Moves the Slot to be a child of the selected slot from the dropdown
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

            //Checks through the raycast results and find the component with the highest slot height, which is the one visible to the user
            Highest = results[0].gameObject.GetComponent<Slot_Script>();
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
                        ts.imageManager.TurnOff("selected");
                    }
                    selectedTroopers.Clear();
                }
                if (result.gameObject.transform.parent.CompareTag("Trooper"))
                {
                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                    {
                        foreach (Trooper_Script ts in selectedTroopers)
                        {
                            ts.imageManager.TurnOff("selected");
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
        int newValue = dropdown.value - 1;
        CheckSlot(slotN1.GetComponent<Slot_Script>(), dropdown.options[dropdown.value].text, dropdown);
    }

    //Finds the selected string from the dropdown menu
    public void FindSelectedSquad(Dropdown dropdown)
    {
        CheckSquad(slotN1.GetComponent<Slot_Script>(), dropdown.options[dropdown.value].text, dropdown);
    }

    //Converts the dropdown string into a returnable slot and sends the viewed slot to that slot
    //Cannot handle duplicate names, just finds the first name in the list that has that name. need to use some ID's or something
    private void CheckSlot(Slot_Script slot, string name, Dropdown dropdown)
    {
        //Remove the dashes that help the user determine children
        string dash = "-";
        string noDashes;

        noDashes = name.Replace(dash, "");

        //Sends and sets details of the slot and its new parent, then saves all slots
        if (noDashes == slot.slotName)
        {
            if (dropdown.GetComponent<Slot_Button>().ids[dropdown.value] == slot.uID)
            {
                viewedSlot.transform.position = new Vector3(0, 0, 0);
                viewedSlot.slotParent.containedSlots.Remove(viewedSlot);
                foreach (Slot_Script ss in viewedSlot.slotParent.containedSlots)
                {
                    if (ss.ID >= viewedSlot.ID)
                    {
                        ss.ID -= 1;
                    }
                }
                slot.containedSlots.Add(viewedSlot);
                viewedSlot.ID = slot.containedSlots.Count;
                viewedSlot.slotParent = slot;
                viewedSlot.ChangeHeight(slot.slotHeight);
                viewedSlot.transform.SetParent(slot.transform, false);
                slots = new List<Slot_Class>();
                slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
                OpenSlot(slot);
            }
        }
        else
        {
            if (slot == viewedSlot)
            {
                Debug.Log("Attempting to prevent moving to children");
            }
            else
            {
                //Repeats the process with every slot until a name match is found
                for (int i = 0; i < slot.containedSlots.Count; i++)
                {
                    CheckSlot(slot.containedSlots[i], name, dropdown);
                }
            }
        }
    }

    //Same as checkslot, but for transfering troopers. 
    private void CheckSquad(Slot_Script slot, string name, Dropdown dropdown)
    {
        //Remove the dashes that help the user determine children
        string dash = "-";
        string noDashes;

        noDashes = name.Replace(dash, "");

        //Sends and sets details of the slot and its new parent, then saves all slots
        if (noDashes == slot.slotName)
        {
            if (dropdown.GetComponent<Slot_Button>().ids[dropdown.value] == slot.uID)
            {
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    if (slot.containedTroopers.Count <= 19)
                    {
                        slot.gameObject.SetActive(true);
                        ts.gameObject.transform.parent = slot.gameObject.transform;
                        ts.trooperPosition = slot.containedTroopers.Count + 1;
                        ts.trooperSquad = slot;
                        slot.containedTroopers.Add(ts);
                        viewedSlot.containedTroopers.Remove(ts);
                        slot.gameObject.SetActive(false);
                    } 
                    else
                    {
                        Debug.Log("Squad Full - " + slot.slotName);
                    }
                }
                int counter = 0;
                foreach(Trooper_Script ts in viewedSlot.containedTroopers)
                {
                    counter += 1;
                    ts.trooperPosition = counter;
                }
                slots = new List<Slot_Class>();
                slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
            }
        }
        else
        {
            //Repeats the process with every slot until a name match is found
            for (int i = 0; i < slot.containedSlots.Count; i++)
            {
                CheckSquad(slot.containedSlots[i], name, dropdown);
            }
        }
    }

    //Turns the slot into a squad 
    public void SetSquad(Slider slider)
    {
        if (slider.value == 1)
        {
            if (viewedSlot.containedSlots.Count == 0)
            {
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
        viewedSlot.squadRole = dropdown.value;
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
}
