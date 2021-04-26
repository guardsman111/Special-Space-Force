using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter_Manager : MonoBehaviour
{
    public Manager_Script manager;
    public int nSlots; //The number of slots required for the calculation to match slots against enemies instead of squads

    public List<Encounter_Class> encounters;

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
                bool topParent = true;
                foreach(Combat_Slot_Script css2 in selectedSlots) //Check if the slot is contained within another selected slot - if so, it is not added to slots, in order that only the top slot is added.
                {
                    if (css2.SlotClass.containedSlots.Contains(css.SlotClass))
                    {
                        topParent = false;
                        break;
                    }
                }
                if (topParent)
                {
                    slots.Add(css);
                }
            }
        }

        //This Automatically assigns squads, slots or enemy units to encounters - Is simple and not perfect, needs drawing out more post Comp3000
        if(slots.Count > nSlots) //If number of slots selected is greater than nSlots then allocate slots to encounters instead of squads
        {
            int enemyCount = 0;
            foreach (Unit_Description ud in mission.MissionC.enemyForce)
            {
                enemyCount += ud.number;
            }
            if (enemyCount < slots.Count)
            {
                foreach (Unit_Description ud in mission.MissionC.enemyForce)
                {
                    for (int i = 0; i < ud.number; i++)
                    {
                        Encounter_Class tempEC = new Encounter_Class();
                        tempEC.stepType = "Move";
                        tempEC.distance = 50;
                        tempEC.enemyUnits = new List<Enemy_Unit_Instance>();
                        tempEC.affectedTroopers = new List<Affected_Trooper_Class>();
                        tempEC.capableTroopers = new List<Affected_Trooper_Class>();
                        tempEC.incapacitatedTroopers = new List<Affected_Trooper_Class>();
                        tempEC.brokenTroopers = new List<Affected_Trooper_Class>();
                        tempEC.deadTroopers = new List<Affected_Trooper_Class>();
                        tempEC.slots = new List<Slot_Class>();
                        Unit_Class tempUC = manager.raceManager.FindUnitClass(mission.parentRace, ud.unitName);
                        CreateEnemyUnitInstance(tempEC, tempUC, mission.parentRace);
                        encounters.Add(tempEC);
                    }
                }
                AllocateSlots(slots);
            }
            else
            {
                foreach (Combat_Slot_Script css in slots)
                {
                    Encounter_Class tempEC = new Encounter_Class();
                    tempEC.stepType = "Move";
                    tempEC.distance = 50;
                    tempEC.enemyUnits = new List<Enemy_Unit_Instance>();
                    tempEC.affectedTroopers = new List<Affected_Trooper_Class>();
                    tempEC.capableTroopers = new List<Affected_Trooper_Class>();
                    tempEC.incapacitatedTroopers = new List<Affected_Trooper_Class>();
                    tempEC.brokenTroopers = new List<Affected_Trooper_Class>();
                    tempEC.deadTroopers = new List<Affected_Trooper_Class>();
                    tempEC.slots = new List<Slot_Class>();
                    tempEC.slots.Add(css.SlotClass);
                    tempEC.playerStrength += float.Parse(css.strengthText.text);
                    encounters.Add(tempEC);
                }
                AllocateEnemyUnits(mission.MissionC.enemyForce, mission);
            }
        } 
        else //Assign by squad instead
        {
            int enemyCount = 0;
            foreach (Unit_Description ud in mission.MissionC.enemyForce)
            {
                enemyCount += ud.number;
            }
            if (enemyCount < squads.Count)
            {
                foreach (Unit_Description ud in mission.MissionC.enemyForce)
                {
                    for (int i = 0; i < ud.number; i++)
                    {
                        Encounter_Class tempEC = new Encounter_Class();
                        tempEC.stepType = "Move";
                        tempEC.distance = 50;
                        tempEC.enemyUnits = new List<Enemy_Unit_Instance>();
                        tempEC.affectedTroopers = new List<Affected_Trooper_Class>();
                        tempEC.capableTroopers = new List<Affected_Trooper_Class>();
                        tempEC.incapacitatedTroopers = new List<Affected_Trooper_Class>();
                        tempEC.brokenTroopers = new List<Affected_Trooper_Class>();
                        tempEC.deadTroopers = new List<Affected_Trooper_Class>();
                        tempEC.slots = new List<Slot_Class>();
                        Unit_Class tempUC = manager.raceManager.FindUnitClass(mission.parentRace, ud.unitName);
                        CreateEnemyUnitInstance(tempEC, tempUC, mission.parentRace);
                        encounters.Add(tempEC);
                    }
                }

                AllocateSquads(squads);
            }
            else
            {
                foreach (Combat_Slot_Script css in squads)
                {
                    Encounter_Class tempEC = new Encounter_Class();
                    tempEC.stepType = "Move";
                    tempEC.distance = 50;
                    tempEC.enemyUnits = new List<Enemy_Unit_Instance>();
                    tempEC.affectedTroopers = new List<Affected_Trooper_Class>();
                    tempEC.capableTroopers = new List<Affected_Trooper_Class>();
                    tempEC.incapacitatedTroopers = new List<Affected_Trooper_Class>();
                    tempEC.brokenTroopers = new List<Affected_Trooper_Class>();
                    tempEC.deadTroopers = new List<Affected_Trooper_Class>();
                    tempEC.slots = new List<Slot_Class>();
                    tempEC.slots.Add(css.SlotClass);
                    tempEC.playerStrength += float.Parse(css.strengthText.text);
                    encounters.Add(tempEC);
                }
                AllocateEnemyUnits(mission.MissionC.enemyForce, mission);
            }
        }
    }

    //Creates an enemy unit and assigns it to the encounter
    public void CreateEnemyUnitInstance(Encounter_Class encounter, Unit_Class unit, string raceName)
    {
        Enemy_Unit_Instance tempEUI = new Enemy_Unit_Instance();
        tempEUI.enemyRefs = new List<Enemy_Class>();
        tempEUI.enemies = new List<Enemy_Instance>();
        tempEUI.movement = unit.speed;
        tempEUI.unitName = unit.unitName;

        encounter.nEnemies = 0;

        foreach (Enemy_Container ec in unit.containedEnemies)
        {
            Enemy_Class tempEnemy = manager.raceManager.FindEnemyClass(raceName, ec.enemyName);
            Race_Weapons_Class raceWeapons = manager.raceManager.FindRaceWeapons(raceName);
            tempEUI.enemyRefs.Add(tempEnemy);

            Enemy_Weapons_Class weap1 = new Enemy_Weapons_Class();
            Enemy_Weapons_Class weap2 = new Enemy_Weapons_Class();

            foreach (Enemy_Weapons_Class ewc in raceWeapons.enemyWeapons)
            {
                if(ewc.weaponName == tempEnemy.weapon1)
                {
                    weap1 = ewc;
                }
                if (ewc.weaponName == tempEnemy.weapon2)
                {
                    weap2 = ewc;
                }
            }

            for (int i = 0; i < ec.nOfEnemies; i++)
            {
                Enemy_Instance newInstance = new Enemy_Instance();
                newInstance.enemyClass = tempEnemy;
                newInstance.enemyName = tempEnemy.enemyName;
                newInstance.health = tempEnemy.health;
                newInstance.primaryWeapon = weap1;
                newInstance.secondaryWeapon = weap2;

                tempEUI.enemies.Add(newInstance);
            }
            encounter.nEnemies += ec.nOfEnemies;
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
                    AllocateTroopers(squads[0].SlotClass, encounter);
                    encounter.slots.Add(squads[0].SlotClass);
                    encounter.playerStrength += float.Parse(squads[0].strengthText.text);
                    squads.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void AllocateSlots(List<Combat_Slot_Script> slots)
    {
        while (slots.Count != 0)
        {
            foreach (Encounter_Class encounter in encounters) //Foreach encounter add a slot
            {
                if (slots.Count != 0)
                {
                    AllocateTroopers(slots[0].SlotClass, encounter);
                    encounter.slots.Add(slots[0].SlotClass);
                    encounter.playerStrength += float.Parse(slots[0].strengthText.text);
                    slots.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void AllocateTroopers(Slot_Class slot, Encounter_Class encounter)
    {
        if (slot.squad)
        {
            foreach(Trooper_Class tc in slot.containedTroopers)
            {
                Affected_Trooper_Class tempATC = new Affected_Trooper_Class();
                tempATC.squad = slot;
                tempATC.trooperClass = tc;

                //Bring accross the relevant files for gear
                foreach(Sprite_Pack sp in manager.equipmentManager.WeaponsPacks)
                {
                    if(tc.weapon1 == sp.packName)
                    {
                        tempATC.primaryWeapon = sp;
                    }
                    if (tc.weapon2 == sp.packName)
                    {
                        tempATC.secondaryWeapon = sp;
                    }
                }
                foreach (Sprite_Pack sp in manager.equipmentManager.EquipmentPacks)
                {
                    if (tc.equipment == sp.packName)
                    {
                        tempATC.equipment = sp;
                    }
                }
                foreach (Sprite_Pack sp in manager.equipmentManager.HelmetPacks)
                {
                    if (tc.equipment == sp.packName)
                    {
                        tempATC.helmet = sp;
                    }
                }
                foreach (Sprite_Pack sp in manager.equipmentManager.ArmourPacks)
                {
                    if (tc.equipment == sp.packName)
                    {
                        tempATC.armour = sp;
                    }
                }
                encounter.capableTroopers.Add(tempATC);
            }
        }
        else
        {
            foreach(Slot_Class sc in slot.containedSlots)
            {
                AllocateTroopers(sc, encounter);
            }
        }
    }

    public void AllocateEnemyUnits(List<Unit_Description> enemyForce, Mission_Script mission)
    {
        List<Unit_Class> units = new List<Unit_Class>();
        foreach (Unit_Description ud in enemyForce)
        {
            for (int i = 0; i < ud.number; i++)
            {
                Unit_Class tempUC = manager.raceManager.FindUnitClass(mission.parentRace, ud.unitName);
                units.Add(tempUC);
            }
        }
        while (units.Count > 0)
        {
            foreach (Encounter_Class encounter in encounters) //Foreach encounter add a slot
            {
                if (units.Count > 0)
                {
                    CreateEnemyUnitInstance(encounter, units[0], mission.parentRace);
                    units.RemoveAt(0);
                }
            }
        }
    }


}
