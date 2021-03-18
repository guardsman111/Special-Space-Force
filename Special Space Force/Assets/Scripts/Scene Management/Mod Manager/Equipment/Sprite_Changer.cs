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


    private bool clicked = false;

    public void ChangePattern(Trooper_Script trooper)
    {
        if (dropdownChild.gameObject.name == "Helmet" || gameObject.name == "Helmet Dropdown")
        {
            for (int i = 0; i < dropdownChild.options.Count; i++)
            {
                if (dropdownChild.options[i].text == trooper.helmetPattern)
                {
                    dropdownChild.value = i;
                }
            }
        }

        if (dropdownChild.gameObject.name == "Armour" || gameObject.name == "Armour Dropdown")
        {
            for (int i = 0; i < dropdownChild.options.Count; i++)
            {
                if (dropdownChild.options[i].text == trooper.armourPattern)
                {
                    dropdownChild.value = i;
                }
            }
        }

        if (dropdownChild.gameObject.name == "Fatigues" || gameObject.name == "Fatigues Dropdown")
        {
            for (int i = 0; i < dropdownChild.options.Count; i++)
            {
                if (dropdownChild.options[i].text == trooper.fatiguesPattern)
                {
                    dropdownChild.value = i;
                }
            }
        }
    }

    //Registers a dropdown has been changed, takes a type and sends changes to currently selected troopers
    public void DropdownChanged(string type)
    {
        selectedTroopers = manager.selectedTroopers;
        if (clicked)
        {
            if (changeAll.isOn == true)
            {
                ExtendedChange(type);
            }
            else
            {
                if (type == "Equip")
                {
                    foreach (Trooper_Script ts in selectedTroopers)
                    {
                        ts.ChangeEquipment(dropdownMain);
                        if (dropdownChild != null)
                        {
                            ChangePattern(ts);
                        }
                    }
                }
                else if (type == "Patt")
                {
                    foreach (Trooper_Script ts in selectedTroopers)
                    {
                        ts.ChangePattern(dropdownChild);
                        ChangePattern(ts);
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
        else 
        {
            if (type == "Equip")
            {
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    ts.ChangeEquipment(dropdownMain);
                    if (dropdownChild != null)
                    {
                        ChangePattern(ts);
                    }
                }
            }
            else if (type == "Patt")
            {
                foreach (Trooper_Script ts in selectedTroopers)
                {
                    ts.ChangePattern(dropdownChild);
                    ChangePattern(ts);
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

    public void ExtendedChange(string type)
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

    public void Clicked()
    {
        clicked = true;
        CancelInvoke("Unclick");
        Invoke("Unclick", 4f);
    }

    public void Unclick()
    {
        clicked = false;
        CancelInvoke("Unclick");
    }
}
