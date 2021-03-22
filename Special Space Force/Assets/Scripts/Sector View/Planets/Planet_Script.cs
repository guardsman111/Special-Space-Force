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
    public bool colonising;
    public int colonisingProgress;
    public bool inhabited;
    public bool building;
    public int buildingProgress;
    public Planet_Class planet;
    public System_Script parentSystem;
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
    private List<Defined_Threat_Class> threatsOnPlanet; // Only added after start-up, they are not stored in planet class

    public List<Defined_Threat_Class> ThreatsOnPlanet
    {
        get { return threatsOnPlanet; }
        set
        {
            if (value != threatsOnPlanet)
            {
                threatsOnPlanet = value;
            }
        }
    }

    public GameObject buildIcon;
    public GameObject threatIcon;


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

    //Generating the planetary stats
    public void PlanetGen(System_Script system, Planet_Class planetClass, Faction_Manager factionM)
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
        parentSystem = system;
        threatsOnPlanet = new List<Defined_Threat_Class>();
        planet.threatsOnPlanet = threatsOnPlanet;

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

        planet.moons = new List<Moon_Class>();

        if(planet.size < 200)
        {
            int random = Random.Range(0, 100);

            if(random < 66)
            {
                GameObject tempMoon = Instantiate(MoonPrefab, transform);
                tempMoon.GetComponent<MoonController>().target = transform;
                tempMoon.GetComponent<MoonController>().yRotSpd = Random.Range(-10f, 10f);
                tempMoon.GetComponent<MoonController>().zRotSpd = Random.Range(-10f, 10f);
                moons.Add(tempMoon);
                tempMoon.SetActive(false);
                random = Random.Range(0, 100);
                Moon_Class moonT = new Moon_Class();
                moonT.moonNumber = 0;
                moonT.moonScale = tempMoon.transform.localScale;
                moonT.speeds = new Vector3(tempMoon.GetComponent<MoonController>().xRotSpd, tempMoon.GetComponent<MoonController>().yRotSpd, tempMoon.GetComponent<MoonController>().zRotSpd);
                foreach (GameObject go in tempMoon.GetComponent<MoonController>().items)
                {
                    go.transform.position += new Vector3(-0, 0, 0);
                    if (go.GetComponentInChildren<Light>())
                    {
                        go.GetComponentInChildren<Light>().intensity = 1f;
                        moonT.lightIntensity = 1f;
                    }
                    moonT.position = go.transform.position;
                }
                planet.moons.Add(moonT);

                if (random < 33)
                {
                    GameObject tempMoon2 = Instantiate(MoonPrefab, transform);
                    tempMoon2.GetComponent<MoonController>().target = transform;
                    tempMoon2.GetComponent<MoonController>().yRotSpd = Random.Range(-10f, 10f);
                    tempMoon2.GetComponent<MoonController>().zRotSpd = Random.Range(-10f, 10f);
                    moons.Add(tempMoon2);
                    tempMoon2.SetActive(false);
                    moonT = new Moon_Class();
                    moonT.moonNumber = 1;
                    moonT.moonScale = tempMoon2.transform.localScale;
                    moonT.speeds = new Vector3(tempMoon2.GetComponent<MoonController>().xRotSpd, tempMoon2.GetComponent<MoonController>().yRotSpd, tempMoon2.GetComponent<MoonController>().zRotSpd);
                    foreach (GameObject go in tempMoon2.GetComponent<MoonController>().items)
                    {
                        go.transform.position += new Vector3(-15, 0, 0);
                        if (go.GetComponentInChildren<Light>())
                        {
                            go.GetComponentInChildren<Light>().intensity = 1f;
                            moonT.lightIntensity = 1f;
                        }
                        moonT.position = go.transform.position;
                    }
                    planet.moons.Add(moonT);
                }
            }
        }

        if (planet.size > 200)
        {
            int random = Random.Range(0, 100);

            if (random < 80)
            {
                GameObject tempMoon = Instantiate(MoonPrefab, transform);
                tempMoon.GetComponent<MoonController>().target = transform;
                tempMoon.GetComponent<MoonController>().yRotSpd = 0;
                tempMoon.GetComponent<MoonController>().yRotSpd = Random.Range(-10f, 10f);
                tempMoon.GetComponent<MoonController>().zRotSpd = Random.Range(-10f, 10f);
                moons.Add(tempMoon);
                tempMoon.SetActive(false);
                random = Random.Range(0, 100);
                float scaleRandom = Random.Range(0.1f, 0.3f);
                tempMoon.transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
                float positionRandom = Random.Range(-50, -15);
                Moon_Class moonT = new Moon_Class();
                moonT.moonNumber = 0;
                moonT.moonScale = tempMoon.transform.localScale;
                moonT.speeds = new Vector3(tempMoon.GetComponent<MoonController>().xRotSpd, tempMoon.GetComponent<MoonController>().yRotSpd, tempMoon.GetComponent<MoonController>().zRotSpd);
                foreach (GameObject go in tempMoon.GetComponent<MoonController>().items)
                {
                    go.transform.position += new Vector3(positionRandom, 0, 0);
                    if (go.GetComponentInChildren<Light>())
                    {
                        go.GetComponentInChildren<Light>().intensity = 0.5f;
                        moonT.lightIntensity = 0.5f;
                    }
                    moonT.position = go.transform.position;
                }
                planet.moons.Add(moonT);

                if (random < 80)
                {
                    GameObject tempMoon2 = Instantiate(MoonPrefab, transform);
                    tempMoon2.GetComponent<MoonController>().target = transform;
                    tempMoon2.GetComponent<MoonController>().yRotSpd = Random.Range(-10f, 10f);
                    tempMoon2.GetComponent<MoonController>().zRotSpd = Random.Range(-10f, 10f);
                    moons.Add(tempMoon2);
                    tempMoon2.SetActive(false);
                    scaleRandom = Random.Range(0.1f, 0.4f);
                    tempMoon2.transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
                    positionRandom = Random.Range(-35, -25);
                    moonT = new Moon_Class();
                    moonT.moonNumber = planetClass.moons.Count;
                    moonT.moonScale = tempMoon2.transform.localScale;
                    moonT.speeds = new Vector3(tempMoon2.GetComponent<MoonController>().xRotSpd, tempMoon2.GetComponent<MoonController>().yRotSpd, tempMoon2.GetComponent<MoonController>().zRotSpd);
                    foreach (GameObject go in tempMoon2.GetComponent<MoonController>().items)
                    {
                        go.transform.position += new Vector3(positionRandom, 0, 0);
                        if (go.GetComponentInChildren<Light>())
                        {
                            go.GetComponentInChildren<Light>().intensity = 0.5f;
                        }
                        moonT.position = go.transform.position;
                    }
                    planet.moons.Add(moonT);
                }
                if (random < 60)
                {
                    GameObject tempMoon2 = Instantiate(MoonPrefab, transform);
                    tempMoon2.GetComponent<MoonController>().target = transform;
                    tempMoon2.GetComponent<MoonController>().yRotSpd = Random.Range(-10f, 10f);
                    tempMoon2.GetComponent<MoonController>().zRotSpd = Random.Range(-10f, 10f);
                    moons.Add(tempMoon2);
                    tempMoon2.SetActive(false);
                    scaleRandom = Random.Range(0.1f, 0.3f);
                    tempMoon2.transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
                    positionRandom = Random.Range(-45, -35);
                    moonT = new Moon_Class();
                    moonT.moonNumber = planetClass.moons.Count;
                    moonT.moonScale = tempMoon2.transform.localScale;
                    moonT.speeds = new Vector3(tempMoon2.GetComponent<MoonController>().xRotSpd, tempMoon2.GetComponent<MoonController>().yRotSpd, tempMoon2.GetComponent<MoonController>().zRotSpd);
                    foreach (GameObject go in tempMoon2.GetComponent<MoonController>().items)
                    {
                        go.transform.position += new Vector3(positionRandom, 0, 0);
                        if (go.GetComponentInChildren<Light>())
                        {
                            go.GetComponentInChildren<Light>().intensity = 0.5f;
                            moonT.lightIntensity = 0.5f;
                        }
                        moonT.position = go.transform.position;
                    }
                    planet.moons.Add(moonT);
                }
                if (random < 40)
                {
                    GameObject tempMoon2 = Instantiate(MoonPrefab, transform);
                    tempMoon2.GetComponent<MoonController>().target = transform;
                    tempMoon2.GetComponent<MoonController>().yRotSpd = Random.Range(-10f, 10f);
                    tempMoon2.GetComponent<MoonController>().zRotSpd = Random.Range(-10f, 10f);
                    moons.Add(tempMoon2);
                    tempMoon2.SetActive(false);
                    scaleRandom = Random.Range(0.1f, 0.3f);
                    tempMoon2.transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
                    positionRandom = Random.Range(-65, -50);
                    moonT = new Moon_Class();
                    moonT.moonNumber = planetClass.moons.Count;
                    moonT.moonScale = tempMoon2.transform.localScale;
                    moonT.speeds = new Vector3(tempMoon2.GetComponent<MoonController>().xRotSpd, tempMoon2.GetComponent<MoonController>().yRotSpd, tempMoon2.GetComponent<MoonController>().zRotSpd);
                    foreach (GameObject go in tempMoon2.GetComponent<MoonController>().items)
                    {
                        go.transform.position += new Vector3(positionRandom, 0, 0);
                        if (go.GetComponentInChildren<Light>())
                        {
                            go.GetComponentInChildren<Light>().intensity = 0.5f;
                            moonT.lightIntensity = 0.5f;
                        }
                        moonT.position = go.transform.position;
                    }
                    planet.moons.Add(moonT);
                }
                if (random < 20)
                {
                    GameObject tempMoon2 = Instantiate(MoonPrefab, transform);
                    tempMoon2.GetComponent<MoonController>().target = transform;
                    tempMoon2.GetComponent<MoonController>().yRotSpd = Random.Range(-10f, 10f);
                    tempMoon2.GetComponent<MoonController>().zRotSpd = Random.Range(-10f, 10f);
                    moons.Add(tempMoon2);
                    tempMoon2.SetActive(false);
                    scaleRandom = Random.Range(0.1f, 0.4f);
                    tempMoon2.transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
                    positionRandom = Random.Range(-20, -10);
                    moonT = new Moon_Class();
                    moonT.moonNumber = planetClass.moons.Count;
                    moonT.moonScale = tempMoon2.transform.localScale;
                    moonT.speeds = new Vector3(tempMoon2.GetComponent<MoonController>().xRotSpd, tempMoon2.GetComponent<MoonController>().yRotSpd, tempMoon2.GetComponent<MoonController>().zRotSpd);
                    foreach (GameObject go in tempMoon2.GetComponent<MoonController>().items)
                    {
                        go.transform.position += new Vector3(positionRandom, 0, 0);
                        if (go.GetComponentInChildren<Light>())
                        {
                            go.GetComponentInChildren<Light>().intensity = 0.5f;
                            moonT.lightIntensity = 0.5f;
                        }
                        moonT.position = go.transform.position;
                    }
                    planet.moons.Add(moonT);
                }
            }
        }

        if (Stats.Biome.surfacePop)
        {
            ChangeCities();
        }

    }

    //Loading the planetary stats
    public void PlanetLoad(System_Script system, Planet_Class planetClass, Faction_Manager factionM)
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
        parentSystem = system;
        building = planetClass.building;
        planet.moons = planetClass.moons;
        threatsOnPlanet = planetClass.threatsOnPlanet;
        if(threatsOnPlanet.Count > 0)
        {
            parentSystem.threatIcon.SetActive(true);
        }

        if (Stats.Population != 0)
        {
            tStats.text = "Population: " + Stats.Population + " k" + "\nPopulation Happiness: " + Stats.popHappiness;
        }
        else
        {
            tStats.text = "Uninhabited";
        }

        planet.size = planetClass.size;

        if (!Stats.Biome.surfacePop)
        {
            clouds.SetActive(false);
        }
         
        Stats.output = factionManager.CalculatePlanetOutput(this);
        output = Stats.output;
        industrial = planet.builtIndustry;

        if (planet.moons.Count > 0)
        {
            foreach (Moon_Class mc in planet.moons)
            {
                GameObject tempMoon = Instantiate(MoonPrefab, transform);
                tempMoon.GetComponent<MoonController>().target = transform;
                tempMoon.GetComponent<MoonController>().xRotSpd = mc.speeds.x;
                tempMoon.GetComponent<MoonController>().yRotSpd = mc.speeds.y;
                tempMoon.GetComponent<MoonController>().zRotSpd = mc.speeds.z;
                moons.Add(tempMoon);
                tempMoon.SetActive(false);
                tempMoon.transform.localScale = mc.moonScale;
                foreach (GameObject go in tempMoon.GetComponent<MoonController>().items)
                {
                    go.transform.position = mc.position;
                    if (go.GetComponentInChildren<Light>())
                    {
                        go.GetComponentInChildren<Light>().intensity = mc.lightIntensity;
                    }
                }
            }
        }

        if (Stats.Biome.surfacePop)
        {
            ChangeCities();
        }

    }

    public void Build()
    {
        if (building)
        {
            buildingProgress += 1;
            if(buildingProgress >= 10)
            {
                building = false;
                planet.building = false;
                buildIcon.SetActive(false);
                industrial += 1;
                planet.builtIndustry += 1;
            }
        }
        else
        {
            building = true;
            planet.building = true;
            buildIcon.SetActive(true);
        }
    }

    public void DestroyIndustry(int destruction)
    {
        industrial -= destruction;
        if(industrial < 0)
        {
            industrial = 0;
        }
        
        //Lowers population
        if (population > 100000)
        {
            population -= (population / 100) * destruction;
        }
        else if(population > 10000)
        {
            population -= destruction * 1000;
        }
        else if (population < 5000)
        {
            population -= destruction * 100;
        }
    }

    public void Colonize()
    {
        if (colonising)
        {
            colonisingProgress += 1;
            if (colonisingProgress >= 25)
            {
                colonising = false;
                industrial = 1;
                planet.builtIndustry = 1;

                float random = Random.Range(0f, 100f);
                random = Mathf.Round(random);
                population = random;
                Stats.Population = random;
                planet.population = random;

                factionManager.AddPlanetToFaction(this);

                if (Stats.Population != 0)
                {
                    tStats.text = "Population: " + Stats.Population.ToString("00,0") + "K" + "\nPopulation Happiness: " + Stats.popHappiness;
                }
                else
                {
                    tStats.text = "Uninhabited";
                }
            }
        }
        else
        {
            colonising = true;
        }
    }

    public void ChangeCities()
    {

        if(planet.biome == "Glass World")
        {

        }

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
