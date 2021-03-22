using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mission_Script : MonoBehaviour
{
    public Threat_Manager manager;
    public TextMeshProUGUI missionName;
    public TextMeshProUGUI missionDesc;
    public TextMeshProUGUI missionEnemies;

    private Mission_Class missionC;

    public Mission_Class MissionC
    {
        get { return missionC; }
        set
        {
            if (value != missionC)
            {
                missionC = value;
            }
        }
    }

    public void CreateMission(Mission_Class mission, Threat_Manager tmanager)
    {
        missionC = mission;
        missionName.text = missionC.missionName;
        missionDesc.text = missionC.missionDescription;

        string enemyText;

        enemyText = "Estimated enemy strength: \n";
        foreach (Unit_Description ud in mission.enemyForce)
        {
            enemyText += ud.number.ToString() + " x " + ud.unitName + " (";
            for(int i = 0; i < manager.modManager.raceManager.RaceUnits.Count; i++)
            {
                if (manager.modManager.raceManager.RaceUnits[i].raceName == tmanager.selectedThreat.ThreatC.threatFaction)
                {
                    foreach(Unit_Class uc in manager.modManager.raceManager.RaceUnits[i].units)
                    {
                        if(uc.unitName == ud.unitName)
                        {
                            foreach(Enemy_Container ec in uc.containedEnemies)
                            {
                                if (ec == uc.containedEnemies[0])
                                {
                                    enemyText += ec.nOfEnemies.ToString() + " " + ec.enemyName;
                                }
                                else
                                {
                                    enemyText += ", " + ec.nOfEnemies.ToString() + " " + ec.enemyName;
                                }
                            }
                        }
                    }
                }
            }
            enemyText += ") \n";
        }

        missionEnemies.text = enemyText;

        manager = tmanager;
    }

    //Tells the manager this mission has been selected
    public void SelectMission()
    {
        manager.selectedMission = this; 
    }
}
