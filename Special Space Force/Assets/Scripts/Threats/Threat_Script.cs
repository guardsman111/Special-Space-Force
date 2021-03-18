using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Threat_Script : MonoBehaviour
{
    public Threat_Manager manager;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI factionText;


    private Threat_Class threatC;

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

    public void CreateThreat(Defined_Threat_Class newThreat)
    {
        nameText.text = newThreat.threatName;
        levelText.text = newThreat.levelDesc;
        factionText.text = newThreat.threatFaction;

        foreach(Threat_Class tc in manager.modManager.threatManager.Threats)
        {
            if(tc.threatName == newThreat.threatName && tc.threatFaction == newThreat.threatFaction)
            {
                threatC = tc;
                Debug.Log("Found Threat and added correctly");
            }
        }

        if(threatC == null)
        {
            Debug.Log("Threat not Found - " + newThreat.threatName + " of " + newThreat.threatFaction);
        }
    }

    public void SelectThreat()
    {
        manager.SetupMissions(this);
    }
}
