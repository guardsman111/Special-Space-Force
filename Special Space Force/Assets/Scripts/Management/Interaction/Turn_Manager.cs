using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour
{
    public Manager_Script manager;

    public void FirstTurn(Generation_Class product)
    {
        manager.GeneratedProduct = product;
        manager.factionManager.SetupPlayerFaction(product);
        foreach (Faction_Script faction in manager.factionManager.factionScripts)
        {
            faction.maxBuilding = ((float)faction.controlledPlanets.Count / 100) * (faction.faction.industrialism * 20);
        }
        manager.factionManager.CalculateIncome();
        manager.factionManager.forceManager.TurnEnd((float)manager.factionManager.Factions[0].factionIncome * ((float)manager.GeneratedProduct.funding / 100));
        manager.factionManager.PlanetScriptToClass();
    }

    public void EndTurn()
    {
        manager.factionManager.CalculateIncome();

        manager.factionManager.forceManager.TurnEnd((float)manager.factionManager.Factions[0].factionIncome * ((float)manager.GeneratedProduct.funding / 100));

        manager.factionManager.FactionBuild();
    }
}
