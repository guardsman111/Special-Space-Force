using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour
{
    public Manager_Script manager;
    public Save_Class saveClass;
    public int turnNumber;

    //Carries out the first turn which is different if creating to when loading
    public void FirstTurn(Generation_Class product, Save_Class save, bool loading)
    {
        manager.GeneratedProduct = product;
        saveClass = save;
        if (loading)
        {
            manager.factionManager.SetupPlayerFaction(product, loading);
            foreach (Faction_Script faction in manager.factionManager.factionScripts)
            {
                faction.maxBuilding = ((float)faction.controlledPlanets.Count / 100) * (faction.faction.industrialism * 20);
            }
            manager.factionManager.CalculateIncome(loading);
            turnNumber = saveClass.turnNumber;
            manager.factionManager.multiplyer = saveClass.enemyMultiplyer;
        }
        else
        {
            manager.factionManager.SetupPlayerFaction(product, loading);
            foreach (Faction_Script faction in manager.factionManager.factionScripts)
            {
                faction.maxBuilding = ((float)faction.controlledPlanets.Count / 100) * (faction.faction.industrialism * 20);
            }
            manager.factionManager.CalculateIncome();
            manager.factionManager.forceManager.TurnEnd((float)manager.factionManager.Factions[0].factionIncome * ((float)manager.GeneratedProduct.funding / 100));
            manager.factionManager.PlanetScriptToClass();
            float strength = manager.forceManager.GetForceStrength();

            // Attempt to spawn up to 4 threats, up to half the player's strength in threats.
            manager.factionManager.SpawnThreats(strength / 2);
            manager.factionManager.SpawnThreats(strength / 2);
            manager.factionManager.SpawnThreats(strength / 2);
            manager.factionManager.SpawnThreats(strength / 2);
        }
        AutoSave();
    }

    //Performs end of turn calculations
    public void EndTurn()
    {
        float strength = manager.forceManager.GetForceStrength();

        manager.factionManager.CalculateIncome();

        manager.factionManager.forceManager.TurnEnd((float)manager.factionManager.Factions[0].factionIncome * ((float)manager.GeneratedProduct.funding / 100));

        manager.factionManager.FactionBuild();

        manager.factionManager.MoveCraft();

        manager.factionManager.GrowThreats();

        manager.factionManager.SpawnThreats(strength);

        turnNumber += 1;
        AutoSave();
    }

    //Saves all information to the saveClass variable
    public void AutoSave()
    {
        saveClass.turnNumber = turnNumber;
        saveClass.enemyMultiplyer = manager.factionManager.multiplyer;
        saveClass.systems = new List<System_Class>();
        for (int i = 0; i < manager.sectorManager.systems.Count; i++)
        {
            saveClass.systems.Add(manager.sectorManager.systems[i].Star);
        }
        saveClass.topSlots[0] = manager.sManager.slotN1.GetComponent<Slot_Script>().slotClass;
        saveClass.fleets = new List<Fleet_Class>();
        for (int i = 0; i < manager.fManager.FleetsS.Count; i++)
        {
            saveClass.fleets.Add(manager.fManager.FleetsS[i].fleetClass);
        }
        saveClass.generatedProduct.force = manager.forceManager.forceClass;
        Serializer.Serialize(saveClass, Application.dataPath + "/Resources/Saves/" + saveClass.saveName + ".Save.xml");
    }
}
