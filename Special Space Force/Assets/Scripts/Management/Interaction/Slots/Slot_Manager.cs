﻿using System.Collections;
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
    public bool menu;

    public void NewSlotTop()
    {
        GameObject temp = Instantiate(prefabSlot, viewedSlot.transform);
        Slot_Script tempS = temp.GetComponent<Slot_Script>();
        tempS.input.text = "New Slot";
        tempS.slotName = "New Slot";
        tempS.slotHeight = viewedSlot.slotHeight + 1;
        tempS.ID = viewedSlot.containedSlots.Count + 1;
        tempS.MakeSlot(tempS, viewedSlot, this);
        tempS.SetPosition(slotN1.GetComponent<Slot_Script>(), viewedSlot);
        viewedSlot.containedSlots.Add(tempS);
        slots = new List<Slot_Class>();
        slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
        moveToDropdown.SetDropdown();
    }

    public void OpenSlot(Slot_Script newViewed)
    {
        viewedSlot = newViewed;
        currentName.text = newViewed.slotName;
        foreach(Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
        {
            ss.SetPosition(slotN1.GetComponent<Slot_Script>(), slotN1.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
        }
        slotFieldScroll.value = 0;
    }

    public void UpSlot()
    {
        viewedSlot = viewedSlot.slotParent;
        currentName.text = viewedSlot.slotName;
        foreach (Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
        {
            ss.SetPosition(slotN1.GetComponent<Slot_Script>(), viewedSlot.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
        }
        slotFieldScroll.value = 0;
    }

    public void TopSlot()
    {
        viewedSlot = slotN1.GetComponent<Slot_Script>();
        currentName.text = viewedSlot.slotName;
        foreach (Slot_Script ss in slotN1.GetComponent<Slot_Script>().containedSlots)
        {
            ss.SetPosition(slotN1.GetComponent<Slot_Script>(), slotN1.GetComponent<Slot_Script>().containedSlots.Count, viewedSlot);
        }
        slotFieldScroll.value = 0;
    }

    public void SetName(Text textBox)
    {
        viewedSlot.SetName(textBox.text);
    }

    public void MoveToSlot(GameObject Dropdown)
    {
        FindSelected(Dropdown.GetComponent<Dropdown>());
        Debug.Log("Moving to Slot " + Dropdown.GetComponent<Dropdown>().options[Dropdown.GetComponent<Dropdown>().value].text);
        slotFieldScroll.value = 0;
    }

    public void MovingSlot(bool setting)
    {
        menu = setting;
    }

    void Awake()
    {
        // Get the components we need to do this
        raycaster = GetComponent<GraphicRaycaster>();
        menu = false;
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0) && !menu)
        {
            //Set up the new Pointer Event
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

            //Create blank script
            int highestSlotHeight;
            highestSlotHeight = -2;

            Highest = results[0].gameObject.GetComponent<Slot_Script>();
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
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

            if(highestSlotHeight > -2 && !menu)
            {
                OpenSlot(Highest);
            }

        }
    }

    public void FindSelected(Dropdown dropdown)
    {
        CheckSlot(slotN1.GetComponent<Slot_Script>(), dropdown.options[dropdown.value].text);
    }

    private void CheckSlot(Slot_Script slot, string name)
    {
        string dash = "-";
        string noDashes;

        noDashes = name.Replace(dash, "");

        if (noDashes == slot.slotName)
        {
            viewedSlot.transform.position = new Vector3(0, 0, 0);
            slot.containedSlots.Add(viewedSlot);
            viewedSlot.slotParent.containedSlots.Remove(viewedSlot);
            viewedSlot.slotParent = slot;
            viewedSlot.transform.SetParent(slot.transform,false);
            OpenSlot(slot);
            slots = new List<Slot_Class>();
            slots.Add(slotN1.GetComponent<Slot_Script>().MasterSaveClass());
        }
        else
        {

            foreach (Slot_Script sc in slot.containedSlots)
            {
                CheckSlot(sc, name);
            }
        }
    }
}
