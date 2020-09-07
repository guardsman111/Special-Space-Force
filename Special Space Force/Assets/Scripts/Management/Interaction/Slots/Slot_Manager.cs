using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Manager : MonoBehaviour
{
    public Slot_Generator generator;
    public GameObject prefabSlot;
    public Slot_Script viewedSlot;
    public List<Slot_Class> slots;
    public GameObject slotN1;
    public GraphicRaycaster raycaster;
    public Slot_Script Highest;
    public Slot_Button moveToDropdown;
    public InputField currentName;
    public Scrollbar slotFieldScroll;
    public Slider slotSlider;
    public List<GameObject> buttonsSwappable;
    public bool menu;
    bool matched = false;

    //Gets the graphics raycaster
    void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        menu = false;
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

        ///
        //If squad, use different code??
        ///

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
        } 
        else
        {
            if (viewedSlot.containedSlots.Count != 0)
            {
                slotSlider.interactable = false;
            } else 
            {
                slotSlider.interactable = true;
            }
            foreach (Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
            {
                ss.SetPosition(slotN1.GetComponent<Slot_Script>(), slotN1.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
            }
            slotSlider.value = 0;
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
        slotFieldScroll.value = 0;
        slotSlider.value = 0;
        slotSlider.interactable = false;
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
    }

    //Sets the name of the slot
    public void SetName(Text textBox)
    {
        viewedSlot.SetName(textBox.text);
    }

    public void SetDropdown()
    {
        moveToDropdown.SetDropdown();
    }

    //Moves the Slot to be a child of the selected slot from the dropdown
    public void MoveToSlot(GameObject Dropdown)
    {
        FindSelected(Dropdown.GetComponent<Dropdown>());
        Debug.Log("Moving to Slot " + Dropdown.GetComponent<Dropdown>().options[Dropdown.GetComponent<Dropdown>().value].text);
        slotFieldScroll.value = 0;
        moveToDropdown.SetDropdown();
        //Dropdown.GetComponent<Dropdown>().value = 0;
    }

    //Registers opening of the menu to prevent users from clicking through it
    public void MovingSlot(bool setting)
    {
        moveToDropdown.SetDropdown();
        menu = setting;
    }

    //If the mouse is pressed and there isnt a menu open, fire the raycast and work out which slot is the highest (the one visible to the user)
    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0) && !menu)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

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
                }
            }

            //If there was a slot in the selection and menu isn't open (double checking incase of slowness on a script's part) open the clicked slot
            if(highestSlotHeight > -2 && !menu)
            {
                OpenSlot(Highest);
            }

        }
    }

    //Finds the selected string from the dropdown menu
    public void FindSelected(Dropdown dropdown)
    {
        CheckSlot(slotN1.GetComponent<Slot_Script>(), dropdown.options[dropdown.value].text);
    }

    //Converts the dropdown string into a returnable slot and sends the viewed slot to that slot
    //Cannot handle duplicate names, just finds the first name in the list that has that name. need to use some ID's or something
    private void CheckSlot(Slot_Script slot, string name)
    {
        //Remove the dashes that help the user determine children
        string dash = "-";
        string noDashes;

        noDashes = name.Replace(dash, "");

        //Sends and sets details of the slot and its new parent, then saves all slots
        if (noDashes == slot.slotName)
        {
            viewedSlot.transform.position = new Vector3(0, 0, 0);
            viewedSlot.slotParent.containedSlots.Remove(viewedSlot);
            foreach (Slot_Script ss in viewedSlot.slotParent.containedSlots)
            {
                if(ss.ID >= viewedSlot.ID)
                {
                    ss.ID -= 1;
                }
            }
            slot.containedSlots.Add(viewedSlot);
            viewedSlot.ID = slot.containedSlots.Count;
            viewedSlot.slotParent = slot;
            viewedSlot.ChangeHeight(slot.slotHeight);
            viewedSlot.transform.SetParent(slot.transform,false);
            slots = new List<Slot_Class>();
            slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
            OpenSlot(slot);
        }
        else
        {
            //Repeats the process with every slot until a name match is found
            for (int i = 0; i< slot.containedSlots.Count; i++)
            {
                CheckSlot(slot.containedSlots[i], name);
            }
        }
    }

    public void SetSquad(Slider slider)
    {
        if (slider.value == 1)
        {
            if (viewedSlot.containedSlots.Count == 0)
            {
                viewedSlot.squad = true;
                buttonsSwappable[0].SetActive(false);
                buttonsSwappable[1].SetActive(false);
                buttonsSwappable[2].SetActive(true);
                buttonsSwappable[3].SetActive(true);
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
                buttonsSwappable[2].SetActive(false);
                buttonsSwappable[3].SetActive(false);
                buttonsSwappable[0].SetActive(true);
                buttonsSwappable[1].SetActive(true);
            }
            else
            {
                Debug.Log("Contains Troopers");
                slider.value = 1;
                slider.interactable = false;
            }
        }
    }
}
