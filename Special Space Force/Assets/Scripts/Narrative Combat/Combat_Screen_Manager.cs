using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Screen_Manager : MonoBehaviour
{
    public Manager_Script manager;

    public RawImage background;
    public TextMeshProUGUI title;
    public Text combatReadout;
    public Button nextStep;
    public Slider balance;
    public Combat_Setup_Manager setupManager;
    public List<Combat_Slot_Script> selectedSlots;
    public Encounter_Manager eManager;
    public Mission_Script currentMission;

    public void StartCombatScreen(Planet_Script planet, Mission_Script mission, List<Combat_Slot_Script> selecteds)
    {
        background.texture = planet.Stats.Biome.GetImageForBiome();
        title.text = mission.missionName.text + " on " + planet.planetName;
        selectedSlots = selecteds;
        currentMission = mission;
        eManager.CreateEncounters(selecteds, mission);
        DoFirstStep();
    }

    public void DoFirstStep()
    {
        combatReadout.text = "";
        Encounter_Class EC = eManager.encounters[Random.Range(0, eManager.encounters.Count)];
        manager.storyManager.Decode(currentMission.MissionC.introStories[Random.Range(0, currentMission.MissionC.introStories.Count)], EC);
    }

    public void DoCombatStep()
    {
        foreach(Encounter_Class ec in eManager.encounters)
        {
            if (ec.enemyUnits.Count > 0)
            {
                foreach (Enemy_Unit_Instance eu in ec.enemyUnits)
                {
                    if (eu.enemies.Count < manager.raceManager.FindUnitClass(currentMission.parentRace, eu.unitName).containedEnemies.Count / 2)
                    {
                        //Run!
                    }
                }
            }
        }
    }

    public void EncounterFight(Encounter_Class encounter)
    {
        
    }

    public void Close()
    {
        setupManager.CloseManager();
        gameObject.SetActive(false);
    }
}
