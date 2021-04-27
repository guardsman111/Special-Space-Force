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
        Story_Class randomIntro = currentMission.MissionC.introStories[Random.Range(0, currentMission.MissionC.introStories.Count)];
        EC.environment = randomIntro.storyEnvironment;
        manager.storyManager.Decode(randomIntro, EC);
    }

    public void StepButtonPress()
    {
        DoCombatStep();
        CalculateStorySection();
    }

    private void DoCombatStep()
    {
        List<Enemy_Unit_Instance> fleeing = new List<Enemy_Unit_Instance>();

        foreach(Encounter_Class ec in eManager.encounters)
        {
            if (ec.complete == false)
            {
                ec.stepInjuredTroopers = new List<Affected_Trooper_Class>();
                ec.stepIncapacitatedTroopers = new List<Affected_Trooper_Class>();
                ec.stepBrokenTroopers = new List<Affected_Trooper_Class>();
                ec.stepDeadTroopers = new List<Affected_Trooper_Class>();
                ec.nDeadEnemies = 0;
                ec.nInjuredEnemies = 0;
                if (ec.enemyUnits.Count > 0)
                {
                    foreach (Enemy_Unit_Instance eu in ec.enemyUnits) //Check to see if the enemy units run away
                    {
                        if (eu.enemies.Count < manager.raceManager.FindUnitClass(currentMission.parentRace, eu.unitName).containedEnemies.Count / 2)
                        {
                            fleeing.Add(eu);
                            //Run!
                        }
                    }
                    foreach (Enemy_Unit_Instance flee in fleeing) //Remove all fleeing units from the encounter
                    {
                        ec.enemyUnits.Remove(flee);
                    }
                    foreach (Slot_Class sc in ec.slots) //Check to see if the player's troopers run away
                    {
                        CheckTroopersFlee(sc, ec);
                    }
                }
                if (ec.enemyUnits.Count > 0 && ec.capableTroopers.Count > 0)
                {
                    EncounterFight(ec);
                }
                else
                {
                    if (ec.capableTroopers.Count > 0)
                    {
                        ec.complete = true;
                        //Player's Force wins this encounter
                    }
                    else if (ec.enemyUnits.Count > 0)
                    {
                        ec.complete = true;
                        //Enemy Force wins this encounter
                    }
                    else
                    {
                        ec.complete = true;
                        //No one wins the encounter
                    }
                }
            }
        }
    }

    //Encounter steps closer and fights with any weapons that are in range
    public void EncounterFight(Encounter_Class encounter) 
    {
        if (encounter.distance > 0)
        {
            float movement = 2;

            foreach (Enemy_Unit_Instance eui in encounter.enemyUnits)
            {
                if (eui.movement < movement)
                {
                    movement = eui.movement;
                }
            }

            movement += (movement + 1) * 3;

            movement += 6;

            encounter.distance -= movement;
            
            if(encounter.distance <= 0)
            {
                encounter.distance = 0;
            }

        }

        float distance = encounter.distance;

        foreach (Affected_Trooper_Class tc in encounter.capableTroopers) //Trooper Attacking
        {
            if (distance > 1) //if distance isn't melee ranged then do shooting attack, else use secondary - This can be expanded later with ammo n stuff
            { 
                if (tc.primaryWeapon.rangeV <= encounter.distance)
                {
                    int hit = Random.Range(0, 100); 

                    //Insert ranged modifiers here (being wounded, enemy dodges, etc)

                    if(hit < tc.trooperClass.ranged) // Successful hit
                    {
                        if(encounter.enemyUnits.Count > 1)
                        {
                            int randomUnit = Random.Range(0, encounter.enemyUnits.Count);

                            int randomEnemy = Random.Range(0, encounter.enemyUnits[randomUnit].enemies.Count);

                            Enemy_Instance tempEI = encounter.enemyUnits[randomUnit].enemies[randomEnemy];

                            if (tc.primaryWeapon.damageV - tempEI.enemyClass.armour > 0)
                            {
                                tempEI.health = tempEI.health - (tc.primaryWeapon.damageV - tempEI.enemyClass.armour);
                                if(tempEI.health < 0)
                                {
                                    encounter.nDeadEnemies += 1;
                                    encounter.nDeadEnemiesTotal += 1;
                                    encounter.enemyUnits[randomUnit].enemies.Remove(encounter.enemyUnits[randomUnit].enemies[randomEnemy]);
                                }
                                else
                                {
                                    encounter.nInjuredEnemies += 1;
                                }
                            }
                        }
                        else
                        {
                            int randomEnemy = Random.Range(0, encounter.enemyUnits[0].enemies.Count);

                            Enemy_Instance tempEI = encounter.enemyUnits[0].enemies[randomEnemy];

                            if (tc.primaryWeapon.damageV - tempEI.enemyClass.armour > 0)
                            {
                                tempEI.health = tempEI.health - (tc.primaryWeapon.damageV - tempEI.enemyClass.armour);
                                if (tempEI.health < 0)
                                {
                                    encounter.nDeadEnemies += 1;
                                    encounter.nDeadEnemiesTotal += 1;
                                    encounter.enemyUnits[0].enemies.Remove(encounter.enemyUnits[0].enemies[randomEnemy]);
                                }
                                else
                                {
                                    encounter.nInjuredEnemies += 1;
                                }
                            }
                        }
                    }
                } 
            }
            else
            {
                int hit = Random.Range(0, 100);

                //Insert ranged modifiers here (being wounded, enemy dodges, etc)

                if (hit < tc.trooperClass.melee) // Successful hit
                {
                    if (encounter.enemyUnits.Count > 1)
                    {
                        int randomUnit = Random.Range(0, encounter.enemyUnits.Count);

                        int randomEnemy = Random.Range(0, encounter.enemyUnits[randomUnit].enemies.Count);

                        Enemy_Instance tempEI = encounter.enemyUnits[randomUnit].enemies[randomEnemy];

                        if (tc.secondaryWeapon.damageV - tempEI.enemyClass.armour > 0)
                        {
                            tempEI.health = tempEI.health - (tc.secondaryWeapon.damageV - tempEI.enemyClass.armour);
                            if (tempEI.health < 0)
                            {
                                encounter.nDeadEnemies += 1;
                                encounter.nDeadEnemiesTotal += 1;
                                encounter.enemyUnits[randomUnit].enemies.Remove(encounter.enemyUnits[randomUnit].enemies[randomEnemy]);
                            }
                            else
                            {
                                encounter.nInjuredEnemies += 1;
                            }
                        }
                    }
                    else
                    {
                        int randomEnemy = Random.Range(0, encounter.enemyUnits[0].enemies.Count);

                        Enemy_Instance tempEI = encounter.enemyUnits[0].enemies[randomEnemy];

                        if (tc.secondaryWeapon.damageV - tempEI.enemyClass.armour > 0)
                        {
                            tempEI.health = tempEI.health - (tc.secondaryWeapon.damageV - tempEI.enemyClass.armour);
                            if (tempEI.health < 0)
                            {
                                encounter.nDeadEnemies += 1;
                                encounter.nDeadEnemiesTotal += 1;
                                encounter.enemyUnits[0].enemies.Remove(encounter.enemyUnits[0].enemies[randomEnemy]);
                            }
                            else
                            {
                                encounter.nInjuredEnemies += 1;
                            }
                        }
                    }
                }
            }
        }

        foreach(Enemy_Unit_Instance eui in encounter.enemyUnits) //Enemy attacking
        {
            foreach (Enemy_Instance ei in eui.enemies)
            {
                if (distance > 1) //if distance isn't melee ranged then do shooting attack, else use secondary - This can be expanded later with ammo n stuff
                {
                    if (ei.primaryWeapon.range <= encounter.distance)
                    {
                        int hit = Random.Range(0, 100);

                        //Insert ranged modifiers here (being wounded, enemy dodges, etc)

                        if (hit < ei.enemyClass.ranged) // Successful hit
                        {
                            int randomTrooper = Random.Range(0, encounter.capableTroopers.Count);

                            Affected_Trooper_Class tempATC = encounter.capableTroopers[randomTrooper];

                            int hitLocation = Random.Range(0, 100);

                            if (hitLocation > 75) //Head hit
                            {
                                int armourHitChance = Random.Range(0, 100);

                                if (armourHitChance > tempATC.helmet.coverage) //Skips armour - can kill
                                {
                                    DealDamageHead(tempATC, encounter, ei, ei.primaryWeapon, true);
                                }
                                else
                                {
                                    if (ei.primaryWeapon.strength > tempATC.helmet.armourV) //Weapon strength higher than armour
                                    {
                                        float chance = 100 - ((ei.primaryWeapon.strength - tempATC.helmet.armourV) * 10);

                                        int random = Random.Range(0, 100);

                                        if (random > chance) //Defeats armour and does damage - can kill
                                        {
                                            DealDamageHead(tempATC, encounter, ei, ei.primaryWeapon, true);
                                        }
                                        else //Armour deflects or otherwise beats damage
                                        {
                                            //Nothing Happens
                                        }
                                    }
                                    else
                                    {
                                        float chance = 95;

                                        int random = Random.Range(0, 100);

                                        if (random > chance) //Defeats armour and does damage - cannot kill
                                        {
                                            DealDamageHead(tempATC, encounter, ei, ei.primaryWeapon, false);
                                        }
                                        else //Armour beats damage
                                        {

                                        }
                                    }
                                }
                            }
                            else //Hits body
                            {
                                int armourHitChance = Random.Range(0, 100);

                                if (armourHitChance > tempATC.armour.coverage) //Skips armour - can kill
                                {
                                    DealDamageBody(tempATC, encounter, ei, ei.primaryWeapon, true);
                                }
                                else
                                {
                                    if (ei.primaryWeapon.strength > tempATC.armour.armourV) //Weapon strength higher than armour
                                    {
                                        float chance = 100 - ((ei.primaryWeapon.strength - tempATC.armour.armourV) * 10);

                                        int random = Random.Range(0, 100);

                                        if (random > chance) //Defeats armour and does damage - can kill
                                        {
                                            DealDamageBody(tempATC, encounter, ei, ei.primaryWeapon, true);
                                        }
                                        else //Armour deflects or otherwise beats damage
                                        {
                                            //Nothing Happens
                                        }
                                    }
                                    else
                                    {
                                        float chance = 95;

                                        int random = Random.Range(0, 100);

                                        if (random > chance) //Defeats armour and does damage - cannot kill
                                        {
                                            DealDamageBody(tempATC, encounter, ei, ei.primaryWeapon, false);
                                        }
                                        else //Armour beats damage
                                        {

                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    int hit = Random.Range(0, 100);

                    //Insert ranged modifiers here (being wounded, enemy dodges, etc)

                    if (hit < ei.enemyClass.strength) // Successful hit
                    {
                        int randomTrooper = Random.Range(0, encounter.capableTroopers.Count);

                        Affected_Trooper_Class tempATC = encounter.capableTroopers[randomTrooper];

                        int hitLocation = Random.Range(0, 100);

                        if (hitLocation > 75) //Head hit
                        {
                            int armourHitChance = Random.Range(0, 100);

                            if (armourHitChance > tempATC.helmet.coverage) //Skips armour - can kill
                            {
                                DealDamageHead(tempATC, encounter, ei, ei.primaryWeapon, true);
                            }
                            else
                            {
                                if (ei.primaryWeapon.strength > tempATC.helmet.armourV) //Weapon strength higher than armour
                                {
                                    float chance = 100 - ((ei.primaryWeapon.strength - tempATC.helmet.armourV) * 10);

                                    int random = Random.Range(0, 100);

                                    if (random > chance) //Defeats armour and does damage - can kill
                                    {
                                        DealDamageHead(tempATC, encounter, ei, ei.primaryWeapon, true);
                                    }
                                    else //Armour deflects or otherwise beats damage
                                    {
                                        //Nothing Happens
                                    }
                                }
                                else
                                {
                                    float chance = 95;

                                    int random = Random.Range(0, 100);

                                    if (random > chance) //Defeats armour and does damage - cannot kill
                                    {
                                        DealDamageHead(tempATC, encounter, ei, ei.primaryWeapon, false);
                                    }
                                    else //Armour beats damage
                                    {

                                    }
                                }
                            }
                        }
                        else //Hits body
                        {
                            int armourHitChance = Random.Range(0, 100);

                            if (armourHitChance > tempATC.armour.coverage) //Skips armour - can kill
                            {
                                DealDamageBody(tempATC, encounter, ei, ei.primaryWeapon, true);
                            }
                            else
                            {
                                if (ei.primaryWeapon.strength > tempATC.armour.armourV) //Weapon strength higher than armour
                                {
                                    float chance = 100 - ((ei.primaryWeapon.strength - tempATC.armour.armourV) * 10);

                                    int random = Random.Range(0, 100);

                                    if (random > chance) //Defeats armour and does damage - can kill
                                    {
                                        DealDamageBody(tempATC, encounter, ei, ei.primaryWeapon, true);
                                    }
                                    else //Armour deflects or otherwise beats damage
                                    {
                                        //Nothing Happens
                                    }
                                }
                                else
                                {
                                    float chance = 95;

                                    int random = Random.Range(0, 100);

                                    if (random > chance) //Defeats armour and does damage - cannot kill
                                    {
                                        DealDamageBody(tempATC, encounter, ei, ei.primaryWeapon, false);
                                    }
                                    else //Armour beats damage
                                    {

                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
    }

    public void CheckTroopersFlee(Slot_Class slot, Encounter_Class encounter)
    {
        if (slot.squad)
        {
            int deadInSlot = 0; //Actually represents dead broken and incapacitated not just dead
            foreach (Trooper_Class tc in slot.containedTroopers)
            {
                foreach (Affected_Trooper_Class atc in encounter.deadTroopers)
                {
                    if (atc.trooperClass == tc)
                    {
                        deadInSlot += 1;
                    }
                }
                foreach (Affected_Trooper_Class atc in encounter.brokenTroopers)
                {
                    if (atc.trooperClass == tc)
                    {
                        deadInSlot += 1;
                    }
                }
                foreach (Affected_Trooper_Class atc in encounter.incapacitatedTroopers)
                {
                    if (atc.trooperClass == tc)
                    {
                        deadInSlot += 1;
                    }
                }
            }
            foreach (Trooper_Class tc in slot.containedTroopers)
            {
                foreach (Affected_Trooper_Class atc in encounter.capableTroopers)
                {
                    if (atc.trooperClass == tc)
                    {
                        if (tc.breakValue < ((slot.numberOfTroopers * 2) - deadInSlot * 4)) //Each dead trooper adds 5 to the break chance, once it gets below the troopers break value they flee
                        {
                            Affect_Class tempAC = new Affect_Class();
                            tempAC.effectType = "Broken";
                            tempAC.effectName = "Running Away!";
                            atc.effects.Add(tempAC);
                            encounter.stepBrokenTroopers.Add(atc);
                            encounter.brokenTroopers.Add(atc);
                            encounter.capableTroopers.Remove(atc);
                            encounter.capableTroopers.TrimExcess();
                        }
                    }
                }
            }
        }
        else
        {
            foreach(Slot_Class sc in slot.containedSlots)
            {
                CheckTroopersFlee(sc, encounter);
            }
        }
    }

    public void DealDamageHead(Affected_Trooper_Class trooper, Encounter_Class encounter, Enemy_Instance enemy, Enemy_Weapons_Class enemyWeapon, bool canKill)
    {
        int damageEffect = Random.Range(0, 80 + enemyWeapon.strength);

        if (damageEffect <= 20)
        {
            Affect_Class tempAffect = new Affect_Class();
            tempAffect.effectName = "Light Wound";
            tempAffect.effectType = "Injury";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            if(trooper.effects.Count > 1)
            {
                int count = 0;
                foreach(Affect_Class affect in trooper.effects)
                {
                    if(affect.effectName == "Light Wound" || affect.effectName == "Serious Wound Treated")
                    {
                        count += 1;
                    }
                }
                if(count > 4)
                {
                    Affect_Class tempAffect2 = new Affect_Class();
                    tempAffect.effectName = "Unconcious";
                    tempAffect.effectType = "Unconcious";
                    tempAffect.effectingEnemy = enemy.enemyClass;
                    tempAffect.effectingWeapon = enemyWeapon;
                    trooper.effects.Add(tempAffect2);
                    encounter.incapacitatedTroopers.Add(trooper);
                    encounter.capableTroopers.Remove(trooper);
                }
            }
            encounter.stepInjuredTroopers.Add(trooper);
            trooper.effects.Add(tempAffect);
        }
        else if (damageEffect <= 50)
        {
            Affect_Class tempAffect = new Affect_Class();
            tempAffect.effectName = "Serious Wound";
            tempAffect.effectType = "Injury";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            trooper.effects.Add(tempAffect);

            Affect_Class tempAffect2 = new Affect_Class();
            tempAffect.effectName = "Unconcious";
            tempAffect.effectType = "Unconcious";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            trooper.effects.Add(tempAffect2);
            encounter.incapacitatedTroopers.Add(trooper);
            encounter.capableTroopers.Remove(trooper);
        }
        else if (damageEffect > 50)
        {
            Affect_Class tempAffect = new Affect_Class();
            tempAffect.effectName = "Dead";
            tempAffect.effectType = "Dead";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            trooper.effects.Add(tempAffect);
            encounter.deadTroopers.Add(trooper);
            encounter.capableTroopers.Remove(trooper);
        }
    }

    public void DealDamageBody(Affected_Trooper_Class trooper, Encounter_Class encounter, Enemy_Instance enemy, Enemy_Weapons_Class enemyWeapon, bool canKill)
    {
        int damageEffect = Random.Range(0, 80 + enemyWeapon.strength);

        if (damageEffect <= 50)
        {
            Affect_Class tempAffect = new Affect_Class();
            tempAffect.effectName = "Light Wound";
            tempAffect.effectType = "Injury";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            if (trooper.effects.Count > 1)
            {
                int count = 0;
                foreach (Affect_Class affect in trooper.effects)
                {
                    if (affect.effectName == "Light Wound" || affect.effectName == "Serious Wound Treated")
                    {
                        count += 1;
                    }
                }
                if (count > 4)
                {
                    Affect_Class tempAffect2 = new Affect_Class();
                    tempAffect.effectName = "Unconcious";
                    tempAffect.effectType = "Unconcious";
                    tempAffect.effectingEnemy = enemy.enemyClass;
                    tempAffect.effectingWeapon = enemyWeapon;
                    trooper.effects.Add(tempAffect2);
                    encounter.incapacitatedTroopers.Add(trooper);
                    encounter.capableTroopers.Remove(trooper);
                }
            }
            trooper.effects.Add(tempAffect);
        }
        else if (damageEffect <= 80)
        {
            Affect_Class tempAffect = new Affect_Class();
            tempAffect.effectName = "Serious Wound";
            tempAffect.effectType = "Injury";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            trooper.effects.Add(tempAffect);

            Affect_Class tempAffect2 = new Affect_Class();
            tempAffect.effectName = "Unconcious";
            tempAffect.effectType = "Unconcious";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            trooper.effects.Add(tempAffect2);
            encounter.incapacitatedTroopers.Add(trooper);
            encounter.capableTroopers.Remove(trooper);
        }
        else if (damageEffect > 80)
        {
            Affect_Class tempAffect = new Affect_Class();
            tempAffect.effectName = "Dead";
            tempAffect.effectType = "Dead";
            tempAffect.effectingEnemy = enemy.enemyClass;
            tempAffect.effectingWeapon = enemyWeapon;
            trooper.effects.Add(tempAffect);
            encounter.deadTroopers.Add(trooper);
            encounter.capableTroopers.Remove(trooper);
        }
    }

    public void CalculateStorySection()
    {
        if(eManager.encounters.Count <= 2) //These values could be set in the preferences section
        {
            for(int i = 0; i < eManager.encounters.Count; i++)
            {
                Story_Class tempS = manager.storyManager.FindStory(eManager.encounters[i]);
                manager.storyManager.Decode(tempS, eManager.encounters[i]);
            }
            // number of stories as count
        }
        else if(eManager.encounters.Count < 4)
        {
            for (int i = 0; i < 2; i++)
            {
                int rand = Random.Range(0, eManager.encounters.Count);
                Story_Class tempS = manager.storyManager.FindStory(eManager.encounters[rand]);
                manager.storyManager.Decode(tempS, eManager.encounters[rand]);
            }
            // 2 stories
        }
        else if (eManager.encounters.Count < 8)
        {
            // 3 stories
        }
        else if (eManager.encounters.Count > 8)
        {
            // 4 stories
        }
    }

    public void Close()
    {
        setupManager.CloseManager();
        gameObject.SetActive(false);
    }
}
