using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Promote_Script : MonoBehaviour
{
    public Slot_Manager manager;
    public Text rankDisplay;
    public Dropdown promoteDropdown;

    public void ChangeRank(Dropdown dropdown)
    {
        if (manager.selectedTroopers.Count > 0)
        {
            manager.selectedTroopers[0].trooperRank = dropdown.options[dropdown.value].text;
            rankDisplay.text = manager.selectedTroopers[0].trooperRank;
        }
    }

    public void ChangeRank(string newRank)
    {
        rankDisplay.text = newRank;
        for(int i = 0; i < promoteDropdown.options.Count; i++)
        {
            if (promoteDropdown.options[i].text == newRank) 
            {
                promoteDropdown.value = i;
            }
        }
    }
}
