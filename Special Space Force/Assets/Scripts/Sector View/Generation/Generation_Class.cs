﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation_Class 
{
    /// <summary>
    /// All user selected information from the customisation screen is stored here for easy access by other scripts
    /// </summary>

    public string regimentName;
    public Force_Class force;
    public int height;
    public int width;
    public int numberofStars;
    public int minimumPlanets;
    public int maximumPlanets;
    public int averagePlanetSize;
    public int habitableChance;
    public int inhabitedChance;
    public int resourceAbundancy;
    public int playerStrength;
    public int xenophobia;
    public int militarism;
    public int expansionism;
    public int industrialism;
    public int funding;
    public int identifierLoc;
    public List<Faction_Class> factions;
    public List<Trait_Class> selectedTraits;
    public List<string> defaultEquipment;
    public List<string> defaultPatterns;
    public List<string> chosenLocalisationList;
    public List<Color32> playerColours;
    public List<Color32> playerFleetColours;

    public Generation_Class()
    {
    }
}
