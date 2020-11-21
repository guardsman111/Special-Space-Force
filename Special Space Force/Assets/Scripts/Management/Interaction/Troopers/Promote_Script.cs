using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Promote_Script : MonoBehaviour
{
    /// <summary>
    /// Switches the visible rank strings around when selecting a new rank, and saves it to the trooper first in selected troopers
    /// </summary>
    public Slot_Manager manager;
    public Text rankDisplay;
    public Dropdown promoteDropdown;

    //Changes rank from a dropdown
    public void ChangeRank(Dropdown dropdown)
    {
        if (manager.selectedTroopers.Count > 0)
        {
            manager.selectedTroopers[0].trooperRank = dropdown.options[dropdown.value].text;
            rankDisplay.text = manager.selectedTroopers[0].trooperRank;
        }
    }

    //Changes dropdown to show string rank, used when selecting trooper to update UI
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
