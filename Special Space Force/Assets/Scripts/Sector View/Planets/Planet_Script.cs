using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Script : MonoBehaviour
{
    ///Testing Scripts
    public string planetName;
    public string biome;
    public int population;
    ///

    public Planet_Stats Stats;
    
    //Generates then sets planet stats
    private void Start()
    {
        //Put Generation code here
        Invoke("PlanetGen", 2.0f);
    }

    //Displays Planet Stats on Debug Log
    public void ShowMe()
    {
        Debug.Log("Name: " + Stats.PName);
        Debug.Log("Biome: " + Stats.Biome.biomeName);
        Debug.Log("Atmosphere (T/F):" + Stats.Biome.atmo);
        Debug.Log("Population: " + Stats.Population.ToString( "n0" ));
        Debug.Log("Population Happiness: " + Stats.popHappiness);
    }

    public void PlanetGen()
    {
        Stats = new Planet_Stats(planetName, biome, population);
        ShowMe();
    }
}
