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
    public float population;
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
    public GameObject storms;
    public List<GameObject> moons;
    public GameObject MoonPrefab;
    public List<Texture2D> cities;
    private int currentCityLevel;

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
            tStats.text = "Population: " + Stats.Population.ToString("00,0") + "K" + "\nPopulation Happiness: " + Stats.popHappiness;
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
        inhabited = planetClass.inhabited;
        biome = planetClass.biome;
        population = planetClass.population;
        useSpace = planetClass.usableSpace;
        tName.text = Stats.PName;
        factionManager = factionM;
        industrial = planet.builtIndustry;

        if (Stats.Population != 0)
        {
            tStats.text = "Population: " + Stats.Population + " k" + "\nPopulation Happiness: " + Stats.popHappiness;
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

            if(random < 40)
            {
                GameObject tempMoon = Instantiate(MoonPrefab, transform);
                tempMoon.GetComponent<MoonController>().target = transform;
                moons.Add(tempMoon);
                tempMoon.SetActive(false);
                random = Random.Range(0, 100);
                foreach (GameObject go in tempMoon.GetComponent<MoonController>().items)
                {
                    go.transform.position += new Vector3(-0, 0, 0);
                }

                if (random < 40)
                {
                    GameObject tempMoon2 = Instantiate(MoonPrefab, transform);
                    tempMoon2.GetComponent<MoonController>().target = transform;
                    moons.Add(tempMoon2);
                    tempMoon2.SetActive(false);
                    foreach(GameObject go in tempMoon2.GetComponent<MoonController>().items)
                    {
                        go.transform.position += new Vector3(-30, 0, 0);
                    }
                }
            }
        }

        if (Stats.Biome.surfacePop)
        {
            ChangeCities();
        }

    }

    public void ChangeCities()
    {

        if (inhabited)
        {
            if (population < 10000f && currentCityLevel != 1)
            {
                Material newMat = new Material(planetSkin.material);
                newMat.SetTexture("_Emissivecities", cities[0]);
                planetSkin.material = newMat;
                currentCityLevel = 1;
            }
            else if(population < 1000000f && currentCityLevel != 2)
            {
                Material newMat = new Material(planetSkin.material);
                newMat.SetTexture("_Emissivecities", cities[1]);
                planetSkin.material = newMat;
                currentCityLevel = 2;
            }
            else if (population < 5000000f && currentCityLevel != 3)
            {
                Material newMat = new Material(planetSkin.material);
                newMat.SetTexture("_Emissivecities", cities[2]);
                planetSkin.material = newMat;
                currentCityLevel = 3;
            }
            else if (population < 10000000f && currentCityLevel != 4)
            {
                Material newMat = new Material(planetSkin.material);
                newMat.SetTexture("_Emissivecities", cities[3]);
                planetSkin.material = newMat;
                currentCityLevel = 4;
            }
            else if (population < 100000000f && currentCityLevel != 5)
            {
                Material newMat = new Material(planetSkin.material);
                newMat.SetTexture("_Emissivecities", cities[4]);
                planetSkin.material = newMat;
                currentCityLevel = 5;
            }
            else if (population < 1000000000f && currentCityLevel != 6)
            {
                Material newMat = new Material(planetSkin.material);
                newMat.SetTexture("_Emissivecities", cities[5]);
                planetSkin.material = newMat;
                currentCityLevel = 6;
            }
        }
        else
        {
            if (currentCityLevel != 0)
            {
                Material newMat = new Material(planetSkin.material);
                newMat.SetTexture("_Emissivecities", null);
                planetSkin.material = newMat;
            }
        }
    }
}
