using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class Slot_Button : MonoBehaviour
{
    /// <summary>
    /// Aids in the moving of troopers between squads and slots
    /// </summary>
    public Slot_Manager manager;
    public List<int> ids;

    //Sets the slot dropdown with names of all slots the slot could be moved too
    public void SetDropdownSlot()
    {
        this.GetComponent<Dropdown>().options.Clear();
        OptionData basic = new OptionData("Move Slot");
        GetComponent<Dropdown>().options.Add(basic);
        ids = new List<int>();
        ids.Add(0);
        AddNames(manager.Slots);
    }

    //Sets the squad dropdown with names of all squads the trooper(s) could be moved too
    public void SetDropdownSquad()
    {
        this.GetComponent<Dropdown>().options.Clear();
        OptionData basic = new OptionData("Transfer Troopers");
        GetComponent<Dropdown>().options.Add(basic);
        ids = new List<int>();
        ids.Add(0);
        AddNames2(manager.Slots);
    }

    //Gathers names for slot dropdown
    private void AddNames(List<Slot_Class> slots)
    {
        foreach (Slot_Class slot in slots)
        {
            //if not a squad, record its name with dashes and give it a unique ID if it doesn't already have one, then check its child slots
            if (!slot.squad)
            {
                string dashes = "";
                for (int i = 0; i < slot.slotHeight; i++)
                {
                    dashes += "-";
                }
                OptionData temp = new OptionData(dashes + slot.slotName);
                GetComponent<Dropdown>().options.Add(temp);
                if (!ids.Contains(slot.uID))
                {
                    ids.Add(slot.uID);
                }
                else
                {
                    Debug.Log("Not Unique ID");
                }
                AddNames(slot.containedSlots);
            }
        }
    }

    //Gathers names for squad dropdown
    private void AddNames2(List<Slot_Class> slots)
    {
        foreach (Slot_Class slot in slots)
        {
            //if a squad, record its name with dashes and give it a unique ID if it doesn't already have one
            if (slot.squad)
            {
                string dashes = "";
                for (int i = 0; i < slot.slotHeight; i++)
                {
                    dashes += "-";
                }
                OptionData temp = new OptionData(dashes + slot.slotName);
                GetComponent<Dropdown>().options.Add(temp);
                if (!ids.Contains(slot.uID))
                {
                    ids.Add(slot.uID);
                }
                else
                {
                    Debug.Log("Unique ID not Unique");
                }
            }
            else
            {
                AddNames2(slot.containedSlots);
            }
        }
    }
}
