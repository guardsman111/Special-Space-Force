using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Stats
{
    /// <summary>
    /// This script holds information for the planet it will be attatched too - is slightly outdated, more information is pulled from 
    /// planet class now
    /// </summary>
    
    public string PName;
    public Planet_Biome Biome;
    public float Population;
    public float popHappiness = 0.8f;
    public float popConsumption;
    public string catagory;
    public float output;
    public float popOutput;
    public float resourceOutput;

    //Military Stats
    public int garrisonSize; // Up to 10? Each level takes 2* level number turns to generate, which would mean 20 turns to get from level 9-10, or 18 turns to go from level 1-4
    public int maxGarrison; //Set by the militarism of the faction, = militarism + 5;
    public string garrisonDesc;
    public int garrisonGrowth;

    public int garrisonFleetSize; // Up to 10? Each level takes 2* level number turns to generate, which would mean 20 turns to get from level 9-10, or 18 turns to go from level 1-4
    public int maxGarrisonFleet; //Set by the militarism of the faction, = militarism + 5;
    public string garrisonFleetDesc;
    public int garrisonFleetGrowth;

    //Constructor for Planet_Stats - takes input from planet_script and creates the Planet_Stats
    public Planet_Stats(Planet_Class pClass, MeshRenderer skin)
    {
        PName = pClass.planetName;
        Biome = new Planet_Biome(pClass.biome, skin);
        Population = pClass.population;
        BiomeEffect();
    }

    //Refreshes biome stats to default and recalculates
    public void BiomeRefreshEffect()
    {
            popHappiness = 0.8f;
            popHappiness += Biome.GetHappinessModifier();
            CheckHappinessTotal();
    }

    //Calculates biome stats
    public void BiomeEffect()
    {
        popHappiness += Biome.GetHappinessModifier();
        CheckHappinessTotal();
    }


    //Checks population happiness isnt above 1
    public void CheckHappinessTotal() { if (popHappiness > 1) popHappiness = 1; }

    public void GrowMilitary()
    {
        if(garrisonGrowth >= (garrisonSize + 1) * 2)
        {
            garrisonSize += 1;
            garrisonGrowth = 1;
            GetMilitaryDesc();
        }
        else
        {
            garrisonGrowth += 1;
        }

        if (garrisonFleetGrowth >= (garrisonFleetSize + 1) * 2)
        {
            garrisonFleetSize += 1;
            garrisonFleetGrowth = 1;
            GetMilitaryFleetDesc();
        }
        else
        {
            garrisonFleetGrowth += 1;
        }

        if(garrisonSize >= maxGarrison)
        {
            garrisonSize = maxGarrison;
        }
        if (garrisonFleetSize >= maxGarrisonFleet)
        {
            garrisonFleetSize = maxGarrisonFleet;
        }
    }

    public void GetMilitaryDesc()
    {
        switch (garrisonSize)
        {
            case 1:
                garrisonDesc = "Armed members of the local populace - No real military structure";
                break;
            case 2:
                garrisonDesc = "Armed members of the local populace brought together as Militia troops";
                break;
            case 3:
                garrisonDesc = "Volunteer Reserve Corps - Trained but low effectiveness";
                break;
            case 4:
                garrisonDesc = "Planetary Reserve Corps - Well trained, equipped with armoured vehicles";
                break;
            case 5:
                garrisonDesc = "Planetary Defence Army - Regular troops stationed on constant guard";
                break;
            case 6:
                garrisonDesc = "Planetary Defence Army - Quick Reaction Forces Planet-Wide";
                break;
            case 7:
                garrisonDesc = "Garrison World - Dedicated to the production of troops to fight wars around the galaxy";
                break;
            case 8:
                garrisonDesc = "Fortress World - Able to rapidly produce Armoured and Mechanised Forces";
                break;
            case 9:
                garrisonDesc = "Assembly World - An empire-widely renowned producer of all things military - Troops, Tanks, Voidcraft, Nukes - you name it, it's made here";
                break;
        }

    }

    public void GetMilitaryFleetDesc()
    {
        switch (garrisonFleetSize)
        {
            case 1:
                garrisonFleetDesc = "Two Picket Defence Voidcraft";
                break;
            case 2:
                garrisonFleetDesc = "Several Squadrons of Picket Defence Voidcraft";
                break;
            case 3:
                garrisonFleetDesc = "A Frigate and Squadrons of Picket Defence Voidcraft";
                break;
            case 4:
                garrisonFleetDesc = "Several Destroyers, Frigates and Picket Defence Squadrons";
                break;
            case 5:
                garrisonFleetDesc = "A Cruiser supported by Destroyers and Frigates, Orbital Defence Platforms in Geostationary Orbit";
                break;
            case 6:
                garrisonFleetDesc = "A pair of Battlecruisers, each leading a Battlefleet composed of Cruisers, Destroyers and Frigates, and Bolstered Orbital Defences in Low Orbit";
                break;
            case 7:
                garrisonFleetDesc = "Three Battlefleets, each led by a Battleship with a Battlecruiser as support, with Orbital Defences in Low and Geostationary Orbit";
                break;
            case 8:
                garrisonFleetDesc = "Orbital Super-Structures primarily for defence, with multiple Battlefleets using these as Anchorages, and a designated Defence Battlefleet led by a Battleship";
                break;
            case 9:
                garrisonFleetDesc = "Orbital Halo designated for Defence, Large Defensive Super-Structures providing defence for Orbital Shipyards. Defensive Fleets and Anchored Fleets provide exceptionally tough defences";
                break;
        }
    }

    public void GenerateMilitary(int militarism)
    {
        maxGarrison = militarism + 5;
        maxGarrisonFleet = militarism + 5;

        garrisonSize = Random.Range(0, militarism + 6);
        garrisonFleetSize = Random.Range(0, militarism + 6);

        GetMilitaryDesc();
        GetMilitaryFleetDesc();
    }
}
