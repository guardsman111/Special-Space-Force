using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Script : MonoBehaviour
{
    ///Testing Scripts
    public string planetName;
    public string biome;
    public int population;
    public float useSpace;
    public Planet_Class planet;

    public Planet_Stats Stats;

    //Linked to the planets mesh renderer so we can change the material, allowing for players to create their own.
    public MeshRenderer planetSkin;

    //Generates then sets planet stats
    private void Start()
    {
        //Put Generation code here
        Invoke("PlanetGen", 2.0f);
    }

    //Displays Planet Stats in Debug Log
    public void ShowMe()
    {
        Debug.Log("Name: " + Stats.PName);
        Debug.Log("Biome: " + Stats.Biome.biomeName);
        Debug.Log("Atmosphere (T/F):" + Stats.Biome.atmo);
        Debug.Log("Population: " + Stats.Population.ToString( "n0" ));
        Debug.Log("Population Happiness: " + Stats.popHappiness);
    }

    //Generates the planetary stats
    public void PlanetGen()
    {
        Planet_Class temp = new Planet_Class();
        Stats = new Planet_Stats(temp, planetSkin);
    }

    //Generates the planetary stats
    public void PlanetGen(Planet_Class planetClass)
    {
        //planetSkin = gameObject.transform.Find("PlanetSurface").GetComponent<MeshRenderer>();
        Stats = new Planet_Stats(planetClass, planetSkin);
        planet = planetClass;
        planetName = planetClass.planetName;
        biome = planetClass.biome;
        population = planetClass.population;
        useSpace = planetClass.usableSpace;
    }
}
