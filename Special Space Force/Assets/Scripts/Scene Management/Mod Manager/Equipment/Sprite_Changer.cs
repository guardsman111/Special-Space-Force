using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprite_Changer : MonoBehaviour
{
    /// <summary>
    /// Manager script for the dropdowns in the force organisation UI, Manages the changes to sprites for equipment and weapons
    /// </summary>
    public Dropdown dropdownMain;
    public Dropdown dropdownChild;
    public Toggle changeAll;
    public Slot_Manager manager;

    public List<Trooper_Script> selectedTroopers;

    //Registers a dropdown has been changed, takes a type and sends changes to currently selected troopers
    public void DropdownChanged(string type)
    {
        selectedTroopers = manager.selectedTroopers;
        if (changeAll.isOn == true)
        {
            if (type == "Equip")
            {
                foreach (Trooper_Script ts in manager.viewedSlot.containedTroopers)
                {
                    ts.ChangeEquipment(dropdownMain);
                }
            }
            else if (type == "Patt")
            {
                foreach (Trooper_Script ts in manager.viewedSlot.containedTroopers)
                {
                    ts.ChangePattern(dropdownChild);
                }

            }
            else if (type == "Weap")
            {
                foreach (Trooper_Script ts in manager.viewedSlot.containedTroopers)
                {
                    ts.ChangeEquipment(dropdownMain);
                }
            }
        }
        else
        {
            if (type == "Equip")
            {
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    ts.ChangeEquipment(dropdownMain);
                }
            }
            else if (type == "Patt")
            {
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    ts.ChangePattern(dropdownChild);
                }
            }
            else if (type == "Weap")
            {
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    ts.ChangeEquipment(dropdownMain);
                }
            }
        }
    }
}
