using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter_Manager : MonoBehaviour
{
    public Manager_Script manager;
    public int nSlots; //The number of slots required for the calculation to match slots against enemies instead of squads

    private List<Encounter_Class> encounters;

    public void CreateEncounters(List<Combat_Slot_Script> selectedSlots, Mission_Script mission)
    {
        List<Combat_Slot_Script> squads = new List<Combat_Slot_Script>();
        List<Combat_Slot_Script> slots = new List<Combat_Slot_Script>();
        encounters = new List<Encounter_Class>();

        foreach(Combat_Slot_Script css in selectedSlots) //Seperate Squads from Slots
        {
            if (css.SlotClass.squad)
            {
                squads.Add(css);
            }
            else
            {
                slots.Add(css);
            }
        }

        if(slots.Count > nSlots) //If number of slots selected is greater than nSlots then allocate slots to encounters instead of squads
        {
            if (mission.MissionC.enemyForce.Count < slots.Count)
            {
                foreach (Unit_Description ud in mission.MissionC.enemyForce)
                {

                }
            }
        } 
        else
        {
            foreach(Unit_Description ud in mission.MissionC.enemyForce)
            {
                for (int i = 0; i < ud.number; i++)
                {
                    Encounter_Class tempEC = new Encounter_Class();
                    tempEC.enemyUnits = new List<Enemy_Unit_Instance>();
                    tempEC.slots = new List<Slot_Class>();
                    Unit_Class tempUC = manager.raceManager.FindUnitClass(mission.parentRace, ud.unitName);
                    CreateEnemyUnitInstance(tempEC, tempUC, mission.parentRace);
                    encounters.Add(tempEC);
                }
            }

            AllocateSquads(squads);
        }
    }

    public void CreateEnemyUnitInstance(Encounter_Class encounter, Unit_Class unit, string raceName)
    {
        Enemy_Unit_Instance tempEUI = new Enemy_Unit_Instance();
        tempEUI.enemyRefs = new List<Enemy_Class>();
        tempEUI.enemies = new List<Enemy_Instance>();
        tempEUI.unitName = unit.unitName;

        foreach (Enemy_Container ec in unit.containedEnemies)
        {
            Enemy_Class tempEnemy = manager.raceManager.FindEnemyClass(raceName, ec.enemyName);
            tempEUI.enemyRefs.Add(tempEnemy);

            for(int i = 0; i < ec.nOfEnemies; i++)
            {
                Enemy_Instance newInstance = new Enemy_Instance();

                newInstance.enemyClass = tempEnemy;
                newInstance.enemyName = tempEnemy.enemyName;
                newInstance.health = tempEnemy.health;

                tempEUI.enemies.Add(newInstance);
            }
        }

        encounter.enemyStrength = unit.strength;
        encounter.enemyUnits.Add(tempEUI);
    }

    public void AllocateSquads(List<Combat_Slot_Script> squads)
    {
        while (squads.Count != 0)
        {
            foreach (Encounter_Class encounter in encounters) //Foreach encounter add a squad
            {
                if (squads.Count != 0)
                {
                    encounter.slots.Add(squads[0].SlotClass);
                    squads.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
