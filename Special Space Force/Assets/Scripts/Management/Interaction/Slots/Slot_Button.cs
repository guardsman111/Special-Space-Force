using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class Slot_Button : MonoBehaviour
{
    public Slot_Manager manager;
    public List<int> ids;

    public void SetDropdownSlot()
    {
        this.GetComponent<Dropdown>().options.Clear();
        ids = new List<int>();
        AddNames(manager.slotN1.GetComponent<Slot_Script>());
    }
    public void SetDropdownSquad()
    {
        this.GetComponent<Dropdown>().options.Clear();
        ids = new List<int>();
        AddNames2(manager.slotN1.GetComponent<Slot_Script>());
    }

    private void AddNames(Slot_Script slot)
    {
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

    private void AddNames2(Slot_Script slot)
    {
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

            foreach (Slot_Script sc in slot.containedSlots)
            {
                AddNames2(sc);
            }
        }
    }
}
