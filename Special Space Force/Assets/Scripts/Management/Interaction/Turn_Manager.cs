using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour
{
    public Manager_Script manager;
    public Save_Class saveClass;


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
        }
        AutoSave();
    }

    public void EndTurn()
    {
        manager.factionManager.CalculateIncome();

        manager.factionManager.forceManager.TurnEnd((float)manager.factionManager.Factions[0].factionIncome * ((float)manager.GeneratedProduct.funding / 100));

        manager.factionManager.FactionBuild();

        manager.factionManager.MoveCraft();

        AutoSave();
    }

    public void AutoSave()
    {
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
        Serializer.Serialize(saveClass, Application.dataPath + "/Resources/Saves/" + saveClass.saveName + ".Save.xml");
    }
}
