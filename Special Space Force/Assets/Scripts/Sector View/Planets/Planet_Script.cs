using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Planet_Script : MonoBehaviour
{
    /// <summary>
    /// This script generates planetary stats and sets the UI text the user sees in the system view
    /// </summary>
    public string planetName;
    public string biome;
    public int population;
    public float useSpace;
    public float output;
    public float industrial;
    public bool inhabited;
    public Planet_Class planet;
    public TextMeshPro tName;
    public TextMeshPro tStats;
    public Faction_Manager factionManager;

    public Planet_Stats Stats;
    public GameObject clouds;
    public List<GameObject> moons;
    public GameObject MoonPrefab;

    //Linked to the planets mesh renderer so we can change the material, allowing for players to create their own.
    public MeshRenderer planetSkin;
    
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

        //Set the texts of the UI with Name, population and happiness
        tName.text = Stats.PName;
        if (Stats.Population != 0)
        {
            tStats.text = "Population: " + Stats.Population.ToString("00,0") + "\nPopulation Happiness: " + Stats.popHappiness;
        }
        else
        {
            tStats.text = "Uninhabited";
        }
    }

    //Loading the planetary stats
    public void PlanetGen(Planet_Class planetClass, Faction_Manager factionM)
    {
        //planetSkin = gameObject.transform.Find("PlanetSurface").GetComponent<MeshRenderer>();
        Stats = new Planet_Stats(planetClass, planetSkin);
        planet = planetClass;
        planetName = planetClass.planetName;
        biome = planetClass.biome;
        population = planetClass.population;
        useSpace = planetClass.usableSpace;
        tName.text = Stats.PName;
        factionManager = factionM;
        industrial = planet.builtIndustry;

        if (Stats.Population != 0)
        {
            tStats.text = "Population: " + Stats.Population + "\nPopulation Happiness: " + Stats.popHappiness;
        }
        else
        {
            tStats.text = "Uninhabited";
        }

        if (Stats.Biome.biomeName.Contains("Giant"))
        {
            if(planet.size < 200)
            {
                planet.size = Random.Range(500, 2000);
            }
        }

        if (!Stats.Biome.surfacePop)
        {
            clouds.SetActive(false);
        }
        //Debug.Log("Generated Planets");

        if (!Stats.Biome.surfacePop)
        {
            planet.popProduction *= 10;
        }

        Stats.output = factionManager.CalculatePlanetOutput(this);
        output = Stats.output;
        industrial = planet.builtIndustry;

        if(planet.size < 200)
        {
            int random = Random.Range(0, 100);

            if(random < 100)
            {
                GameObject tempMoon = Instantiate(MoonPrefab, transform);
                tempMoon.GetComponent<MoonController>().target = transform;
                moons.Add(tempMoon);
                tempMoon.SetActive(false);
            }
        }
    }
}
