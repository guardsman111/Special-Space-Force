using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction_Manager : MonoBehaviour
{
    [SerializeField]
    private List<Faction_Class> factions;


    public List<Faction_Class> Factions
    {
        get { return factions; }

        set
        {
            if(value != factions)
            {
                factions = value; 
            }
        }
    }

    public void Run(List<GameObject> systemsObjects)
    {
        foreach(Faction_Class fc in Factions)
        {
            foreach(System_Script sc in fc.controlledSystems)
            {
                foreach(Planet_Script pc in sc.SystemPlanets)
                {
                    fc.controlledPlanets.Add(pc);
                }
            }
        }
    }

    public void CalculateIncome()
    {


        foreach(Faction_Class fc in factions)
        {
            fc.factionIncome = 0;
            foreach (System_Script sc in fc.controlledSystems)
            {
                sc.combinedOutput = 0;
                foreach (Planet_Script pc in sc.SystemPlanets)
                {
                    sc.combinedOutput += (int)pc.output;
                    fc.factionIncome += (int)pc.output;
                }
            }

            fc.factionResourcePile += fc.factionIncome;

            Debug.Log(fc.factionName + " gains " + fc.factionIncome.ToString() + " resources added to it's stockpile; Stockpile total - " + fc.factionResourcePile.ToString());
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
}
