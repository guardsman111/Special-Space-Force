using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction_Manager : MonoBehaviour
{
    [SerializeField]
    private List<Faction_Class> factions;
    public List<Faction_Script> factionScripts;
    public Force_Manager forceManager;

    public List<Faction_Class> Factions
    {
        get { return factions; }

        set
        {
            if (value != factions)
            {
                factions = value;
            }
        }
    }

    public void Run(List<GameObject> systemsObjects)
    {
        foreach (Faction_Script fs in factionScripts)
        {
            foreach (System_Script sc in fs.controlledSystems)
            {
                foreach (Planet_Script pc in sc.SystemPlanets)
                {
                    if (pc.inhabited)
                    {
                        fs.controlledPlanets.Add(pc);
                    }
                }
            }
        }
    }

    public void Load(List<GameObject> systemsObjects, List<Faction_Class> factionsL)
    {
        factions = factionsL;
        for (int i = 0; i < factionsL.Count; i++)
        {
            factionScripts.Add(new Faction_Script());
            factionScripts[i].faction = factions[i];
            factionScripts[i].controlledSystems = new List<System_Script>();
            factionScripts[i].controlledPlanets = new List<Planet_Script>();
        }
        
        foreach (Faction_Script fs in factionScripts)
        {
            fs.faction.controlledSystems = new List<System_Class>();
            foreach (GameObject s in systemsObjects)
            {
                System_Script script = s.GetComponent<System_Script>();
                if (script.Star.allegiance == fs.faction.factionID)
                {
                    fs.controlledSystems.Add(script);
                    fs.faction.controlledSystems.Add(script.Star);
                    foreach (Planet_Script pc in script.SystemPlanets)
                    {
                        if (pc.inhabited)
                        {
                            fs.controlledPlanets.Add(pc);
                            fs.faction.controlledPlanets.Add(pc.planet);
                        }
                    }
                }
            }
        }
    }

    public void CalculateIncome()
    {


        foreach (Faction_Script fs in factionScripts)
        {
            fs.faction.factionIncome = 0;
            foreach (System_Script sc in fs.controlledSystems)
            {
                sc.combinedOutput = 0;
                foreach (Planet_Script pc in sc.SystemPlanets)
                {
                    if (pc.inhabited)
                    {
                        sc.combinedOutput += (int)pc.output;
                        fs.faction.factionIncome += (int)pc.output;
                    }
                }
            }

            fs.faction.factionResourcePile += fs.faction.factionIncome;

            Debug.Log(fs.faction.factionName + " gains " + fs.faction.factionIncome.ToString() + " resources added to it's stockpile; Stockpile total - " + fs.faction.factionResourcePile.ToString());
        }


    }

    public void PlanetScriptToClass()
    {
        foreach (Faction_Script fs in factionScripts)
        {
            foreach (System_Script sc in fs.controlledSystems)
            {
                foreach (Planet_Script pc in sc.SystemPlanets)
                {
                    fs.faction.controlledPlanets.Add(pc.planet);
                }
            }
        }
    } 

    public List<Faction_Class> GenerateFactions(List<AI_Class> AI)
    {

        factions = new List<Faction_Class>();
        factions.Add(new Faction_Class());
        factionScripts = new List<Faction_Script>();
        factionScripts.Add(new Faction_Script());
        factions[0].factionName = "Player Faction";
        factions[0].controlledSystems = new List<System_Class>();
        factions[0].controlledPlanets = new List<Planet_Class>();
        factionScripts[0].faction = factions[0];
        factionScripts[0].controlledSystems = new List<System_Script>();
        factionScripts[0].controlledPlanets = new List<Planet_Script>();

        for (int i = 1; i <= AI.Count; i++)
        {
            factions.Add(new Faction_Class());
            factionScripts.Add(new Faction_Script());
            factions[i].factionName = AI[i - 1].race.empireName;
            factions[i].factionID = i;
            factions[i].controlledSystems = new List<System_Class>();
            factions[i].controlledPlanets = new List<Planet_Class>();
            factions[i].AIRace = AI[i - 1];
            factionScripts[i].faction = factions[i];
            factionScripts[i].controlledSystems = new List<System_Script>();
            factionScripts[i].controlledPlanets = new List<Planet_Script>();
        }

        return factions;
    }

    public void LoadFactions(List<Faction_Class> factionsL)
    {
        factions = factionsL;
        for (int i = 0; i < factionsL.Count; i++)
        {
            factionScripts.Add(new Faction_Script());
            factionScripts[i].faction = factions[i];
            factionScripts[i].controlledSystems = new List<System_Script>();
            factionScripts[i].controlledPlanets = new List<Planet_Script>();
        }
    }

    public void SetupPlayerFaction(Generation_Class product)
    {
        factions[0].xenophobia = product.xenophobia;
        factions[0].militarism = product.militarism;
        factions[0].expansionism = product.expansionism;
        factions[0].industrialism = product.industrialism;

        foreach(Planet_Script planet in factionScripts[0].controlledPlanets)
        {
            planet.Stats.GenerateMilitary(product.militarism);
        }
    }

    public float CalculatePlanetOutput(Planet_Script planet)
    {
        float output = 0;

        float popFactor = 1;

        if(planet.population > 1000)
        {
            popFactor = 2;
        }
        if (planet.population > 10000)
        {
            popFactor = 3;
        }
        if (planet.population > 50000)
        {
            popFactor = 5;
        }
        if (planet.population > 100000)
        {
            popFactor = 10;
        }
        if (planet.population > 300000)
        {
            popFactor = 12;
        }
        if (planet.population > 600000)
        {
            popFactor = 16;
        }
        if (planet.population > 900000)
        {
            popFactor = 20;
        }

        if (planet.Stats.popHappiness == 0)
        {
            popFactor = popFactor / 3;
        } 
        else if(planet.Stats.popHappiness < 0.2)
        {
            popFactor = popFactor / 2;
        }
        else if (planet.Stats.popHappiness < 0.4)
        {
            popFactor = popFactor / 1.5f;
        }
        else if (planet.Stats.popHappiness < 0.5)
        {
            popFactor = popFactor / 1.3f;
        }

        float bMetalOutput = planet.planet.baseMetalsAmount * (planet.planet.builtIndustry * popFactor);
        float pMetalOutput = planet.planet.preciousMetalsAmount * (planet.planet.builtIndustry * popFactor);
        float aLandOutput = planet.planet.foodAvailability * (planet.planet.builtIndustry * popFactor);
        float popOutput = planet.planet.popProduction * (planet.population / 50);
        float popRequirement = planet.population / 10;
        planet.Stats.popConsumption = popRequirement;
        planet.Stats.resourceOutput = bMetalOutput + pMetalOutput + aLandOutput;
        planet.Stats.popOutput = popOutput;

        output = bMetalOutput + pMetalOutput + aLandOutput + popOutput - popRequirement;

        //Debug.Log("The Planet " + planet.planetName + " is able to output " + output);
        //Debug.Log("Sourced from " + planet.planet.baseMetalsAmount + "x" + planet.planet.builtIndustry + " base metals, " + planet.planet.preciousMetalsAmount + "x" + planet.planet.builtIndustry + " precious metals and " + planet.planet.foodAvailability + "x" + planet.planet.builtIndustry + " agricultural production.");

        planet.Stats.catagory = CatagorisePlanet(planet);

        return output;
    }

    public string CatagorisePlanet(Planet_Script planet)
    {
        string export = null;

        int bMetals = 0;
        int pMetals = 0;
        int agri = 0;

        if(planet.planet.baseMetalsAmount < 10)
        {
            bMetals = 0;
        }
        else if (planet.planet.baseMetalsAmount < 30)
        {
            bMetals = 1;
        }
        else if (planet.planet.baseMetalsAmount < 50)
        {
            bMetals = 2;
        }
        else if (planet.planet.baseMetalsAmount < 70)
        {
            bMetals = 3;
        }
        else if (planet.planet.baseMetalsAmount < 90)
        {
            bMetals = 4;
        }
        else
        {
            bMetals = 5;
        }

        if (planet.planet.preciousMetalsAmount < 10)
        {
            pMetals = 0;
        }
        else if (planet.planet.preciousMetalsAmount < 30)
        {
            pMetals = 1;
        }
        else if (planet.planet.preciousMetalsAmount < 50)
        {
            pMetals = 2;
        }
        else if (planet.planet.preciousMetalsAmount < 70)
        {
            pMetals = 3;
        }
        else if (planet.planet.preciousMetalsAmount < 90)
        {
            pMetals = 4;
        }
        else
        {
            pMetals = 5;
        }

        if (planet.planet.foodAvailability < 10)
        {
            agri = 0;
        }
        else if (planet.planet.foodAvailability < 30)
        {
            agri = 1;
        }
        else if (planet.planet.foodAvailability < 50)
        {
            agri = 2;
        }
        else if (planet.planet.foodAvailability < 70)
        {
            agri = 3;
        }
        else if (planet.planet.foodAvailability < 90)
        {
            agri = 4;
        }
        else
        {
            agri = 5;
        }

        if (bMetals == 2)
        {
            if (export == null)
            {
                export = "Refined Metals";
            }
        }
        else if (bMetals == 3)
        {
            if (export == null)
            {
                export = "Refined Metals, Contruction Materials";
            }
        }
        else if (bMetals == 4)
        {
            if (export == null)
            {
                export = "Construction Materials, Industrial Materials";
            }
        }
        else if (bMetals == 5)
        {
            if (export == null)
            {
                export = "Industrial Materials, Voidcraft Materials";
            }
        }
        
        if (pMetals == 2)
        {
            if (export == null)
            {
                export = "Refined Precious Metals";
            }
            else
            {
                export += ", Refined Precious Metals";
            }
        }
        else if (pMetals == 3)
        {
            if (export == null)
            {
                export = "Electronics";
            }
            else
            {
                export += ", Electronics";
            }
        }
        else if (pMetals == 4)
        {
            if (export == null)
            {
                export = "Electronics, Industrial Components";
            }
            else
            {
                export += ", Electronics, Industrial Components";
            }
        }
        else if (pMetals == 5)
        {
            if (export == null)
            {
                export = "Industrial Components, Voidcraft Components";
            }
            else
            {
                export += ", Industrial Components, Voidcraft Components";
            }
        }

        if (agri == 2)
        {
            if (export == null)
            {
                export = "Plant Foodstuffs";
            }
            else
            {
                export += ", Plant Foodstuffs";
            }
        }
        else if (agri == 3)
        {
            if (export == null)
            {
                export = "Livestock";
            }
            else
            {
                export += ", Livestock";
            }
        }
        else if (agri == 4)
        {
            if (export == null)
            {
                export = "Plant Foodstuffs, Livestock";
            }
            else
            {
                export += ", Plant Foodstuffs, Livestock";
            }
        }
        else if (agri == 5)
        {
            if (export == null)
            {
                export = "Exotic Foods, Mass Livestock";
            }
            else
            {
                export += ", Exotic Foods, Mass Livestock";
            }
        }

        if(agri == 0 && bMetals == 0 && pMetals == 0)
        {
            export = "Voidcraft and Voidcraft Fuels";
        }

        return export;
        
    }

    //Does AI Building for Player Faction
    public void FactionBuild()
    {
        Faction_Script playerFaction = factionScripts[0];

        List<Planet_Script> notBuildingPlanets = new List<Planet_Script>();

        int currentlyBuilding = 0;

        foreach(Planet_Script planet in playerFaction.controlledPlanets)
        {
            if (planet.building)
            {
                currentlyBuilding += 1;
                planet.Build();
            }
            else
            {
                notBuildingPlanets.Add(planet);
            }
            planet.Stats.GrowMilitary();
        }

        int buildingStarted = 0;

        while (currentlyBuilding < playerFaction.maxBuilding && buildingStarted <= (((float)playerFaction.controlledPlanets.Count / 100) * (playerFaction.faction.expansionism / 5)) && notBuildingPlanets.Count > 0)
        {
            int random = Random.Range(0, notBuildingPlanets.Count);

            if (notBuildingPlanets[random].planet.usableSpace > notBuildingPlanets[random].planet.builtIndustry / 100)
            {
                notBuildingPlanets[random].Build();
                currentlyBuilding += 1;
                buildingStarted += 1;
                Debug.Log("Planet " + notBuildingPlanets[random].planetName + " is now building.");
            }
            notBuildingPlanets.RemoveAt(random);
        }

        Debug.Log(currentlyBuilding + " planets constructing out of max " + playerFaction.maxBuilding);

        //Use again for not colonizing planets
        notBuildingPlanets.Clear();

        int nColonising = 0;
        float maxColonising = ((factionScripts[0].faction.expansionism / 10) * factionScripts[0].faction.controlledPlanets.Count) / 100;

        foreach (System_Script system in playerFaction.controlledSystems)
        {
            if (!system.colonising)
            {
                foreach (Planet_Script planet in system.SystemPlanets)
                {
                    if (planet.inhabited == false && planet.colonising == false)
                    {
                        notBuildingPlanets.Add(planet);
                    }
                    else if (planet.colonising)
                    {
                        planet.Colonize();
                        system.colonising = true;
                        nColonising += 1;
                    }
                }
            }
            else
            {
                system.colonising = false;
                foreach (Planet_Script planet in system.SystemPlanets)
                {
                    if (planet.colonising)
                    {
                        planet.Colonize();
                        system.colonising = true;
                        nColonising += 1;
                    }
                }
            }
        }

        if (notBuildingPlanets.Count > 0 && nColonising < maxColonising)
        {
            int newRandom = Random.Range(0, notBuildingPlanets.Count);
            notBuildingPlanets[newRandom].Colonize();
            notBuildingPlanets[newRandom].parentSystem.colonising = true;
            Debug.Log(notBuildingPlanets[newRandom].planetName + " is now being Colonised!");
        }
    }

    //Does AI Building for AI Faction
    public void FactionBuild(Faction_Class faction)
    {

    }

    public void AddPlanetToFaction(Planet_Script planet)
    {
        int allegiance = planet.parentSystem.Star.allegiance;
        factions[allegiance].controlledPlanets.Add(planet.planet);
        factionScripts[allegiance].controlledPlanets.Add(planet);
    }
}
