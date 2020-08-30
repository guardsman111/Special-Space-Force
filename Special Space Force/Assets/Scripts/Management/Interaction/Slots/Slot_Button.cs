using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class Slot_Button : MonoBehaviour
{
    public Slot_Manager manager;
    public Slot_Script currentScript;

    public void SetDropdown()
    {
        this.GetComponent<Dropdown>().options.Clear();
        AddNames(manager.slotN1.GetComponent<Slot_Script>());
    }

    private void AddNames(Slot_Script slot)
    {
        string dashes = "";
        for (int i = 0; i < slot.slotHeight; i++)
        {
            dashes += "-";
        }
        OptionData temp = new OptionData(dashes + slot.slotName);
        GetComponent<Dropdown>().options.Add(temp);
        foreach (Slot_Script sc in slot.containedSlots)
        {
            AddNames(sc);
        }
    }
}
