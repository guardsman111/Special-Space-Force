using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Class
{
    /// <summary>
    /// This script holds planet information, which is then passed all over the place
    /// </summary>
    
    public string planetName;
    public string biome;
    public int biomeID;
    public bool inhabited;
    public float population;
    public int size;
    public float usableSpace;
    public float baseMetalsAmount;
    public float preciousMetalsAmount;
    public float foodAvailability;
    public float popProduction;

    public bool building;
    public int builtIndustry;

    public List<Moon_Class> moons;

    public Planet_Class()
    {
    }
}
