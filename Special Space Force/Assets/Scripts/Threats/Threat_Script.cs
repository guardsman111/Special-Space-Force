using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Threat_Script : MonoBehaviour
{
    /// <summary>
    /// Operates similar to troopers and slot scripts - holds relevant game information and is saved to a DFC on a planet upon game close
    /// </summary>
    public Threat_Manager manager;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI factionText;



    private List<Mission_Class> containedMissions;
    private Threat_Class threatC;

    public List<Mission_Class> ContainedMissions
    {
        get {return containedMissions; }
        set 
        { 
            if(value != containedMissions)
            {
                containedMissions = value;
            }
        }
    }
    public Threat_Class ThreatC
    {
        get { return threatC; }
        set
        {
            if(value != threatC)
            {
                threatC = value;
            }
        }
    }

    //Creates the threat from a DFC
    public void CreateThreat(Defined_Threat_Class newThreat)
    {
        nameText.text = newThreat.threatName;
        levelText.text = newThreat.levelDesc;
        factionText.text = newThreat.threatFaction;

        //Finds the relevant threat from master threats
        foreach(Threat_Class tc in manager.modManager.threatManager.Threats)
        {
            if(tc.threatName == newThreat.threatName && tc.threatFaction == newThreat.threatFaction)
            {
                threatC = tc;
                threatC.currentLevel = newThreat.level;
                ContainedMissions = manager.GetMissions(tc);
                Debug.Log("Found Threat and added correctly");
            }
        }

        if(threatC == null)
        {
            Debug.Log("Threat not Found - " + newThreat.threatName + " of " + newThreat.threatFaction);
        }
    }

    //Indicates threat has been selected
    public void SelectThreat()
    {
        manager.SetupMissions(this);
    }
}
