using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation_Class 
{
    /// <summary>
    /// All user selected information from the customisation screen is stored here for easy access by other scripts
    /// </summary>
    
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
    public List<AI_Class> toggledAI;
    public string trooperNamesList;
    public List<Color32> playerColours;

    public Generation_Class()
    {
    }
}
