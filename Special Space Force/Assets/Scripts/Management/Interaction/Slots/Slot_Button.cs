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
        AddNames(manager.slotN1.GetComponent<Slot_Script>());
    }

    //Sets the squad dropdown with names of all squads the trooper(s) could be moved too
    public void SetDropdownSquad()
    {
        this.GetComponent<Dropdown>().options.Clear();
        OptionData basic = new OptionData("Transfer Troopers");
        GetComponent<Dropdown>().options.Add(basic);
        ids = new List<int>();
        ids.Add(0);
        AddNames2(manager.slotN1.GetComponent<Slot_Script>());
    }

    //Gathers names for slot dropdown
    private void AddNames(Slot_Script slot)
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
            while (ids.Contains(slot.uID))
            {
                slot.RegenerateUID();
                Debug.Log("Unique ID not Unique");
            }
            ids.Add(slot.uID);
            foreach (Slot_Script sc in slot.containedSlots)
            {
                AddNames(sc);
            }
        }
    }

    //Gathers names for squad dropdown
    private void AddNames2(Slot_Script slot)
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
            while (ids.Contains(slot.uID))
            {
                slot.RegenerateUID();
                Debug.Log("Unique ID not Unique");
            }
            ids.Add(slot.uID);
        } 
        else
        {
            //else check its child slots
            foreach (Slot_Script sc in slot.containedSlots)
            {
                AddNames2(sc);
            }
        }
    }
}
