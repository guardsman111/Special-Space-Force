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
    public Dropdown roleDropdown;
    public Dropdown promoteDropdown;
    public Rank_Manager rankManager;

    public void SetupRoleDropdown(List<Squad_Role_Class> roles)
    {
        roleDropdown.options.Clear();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach (Squad_Role_Class r in roles)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(r.RoleName);
            options.Add(option);
        }

        roleDropdown.options = options;
    }

    public void SetupRankDropdown()
    {
        promoteDropdown.options.Clear();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach(Rank_Definition r in manager.viewedSlot.squadRole.RankDefs)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(r.RankName);
            options.Add(option);
        }

        promoteDropdown.options = options;
    }

    //Changes rank from a dropdown
    public void ChangeRank(Dropdown dropdown)
    {
        if (manager.selectedTroopers.Count > 0)
        {
            int count = 0;
            for (int i = 0; i < manager.viewedSlot.containedTroopers.Count; i++)
            {
                if(manager.viewedSlot.containedTroopers[i].trooperRank == dropdown.options[dropdown.value].text)
                {
                    count += 1;
                }
            }
            if (count < manager.viewedSlot.squadRole.RankDefs[dropdown.value].RankLimit || manager.viewedSlot.squadRole.RankDefs[dropdown.value].RankLimit == 0)
            {
                manager.selectedTroopers[0].trooperRank = dropdown.options[dropdown.value].text;
                rankDisplay.text = manager.selectedTroopers[0].trooperRank;
            }
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
