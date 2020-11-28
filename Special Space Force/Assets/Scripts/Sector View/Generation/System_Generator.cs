using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Generator : MonoBehaviour
{
    /// <summary>
    /// This initializes all of the systems one by one, then generates the star Game object for the System_Script to do its thang
    /// </summary>
    [SerializeField]
    private Biome_Manager biomeManager;

    public Biome_Manager BiomeManager
    {
        get { return biomeManager; }

        set
        {
            if
              (value != biomeManager)
            {
                biomeManager = value;
            }
        }
    }

    private int secHeight;
    private int secWidth;
    private int avgPlanetSize;

    public string[] colours;
    [SerializeField]
    public List<System_Class> systemsList;
    [SerializeField]
    public List<GameObject> generatedSystems;
    [SerializeField]
    public GameObject[] prefabs;
    public GameObject planetPrefab;
    public Generation_Class generatedProduct;
    [SerializeField]
    public Color32[] AIColors;

    public int AvgPlanetSize
    {
        get { return avgPlanetSize; }

        set
        {
            if
              (value != avgPlanetSize)
            {
                avgPlanetSize = value;
            }
        }
    }

    //Begins the Genreation of the stars
    public void BeginGeneration(Generation_Class product, List<string> sNames)
    {
        systemsList = new List<System_Class>();
        generatedSystems = new List<GameObject>();
        avgPlanetSize = product.averagePlanetSize;
        generatedProduct = product;
        
        //For each star generate position, colour, #planets and name
        for(int i = 0; i < product.numberofStars; i++)
        {
            int posX = Random.Range(-(product.width / 2), (product.width / 2));
            int posZ = Random.Range(-(product.height / 2), (product.height / 2));
            string rColour = colours[Random.Range(0, colours.Length)];
            int randP = Random.Range(product.minimumPlanets, product.maximumPlanets + 1);
            int randN = Random.Range(0, sNames.Count);

            CreateStar(sNames[randN], rColour, posX, posZ, randP);
        }
        secHeight = product.height;
        secWidth = product.width;
    }

    //Load and then generate
    public void BeginGeneration(List<System_Class> loadSystems, Save_Class save)
    {
        systemsList = loadSystems;
        foreach(System_Class s in systemsList)
        {
            CreateStar(s, save);
        }
    }

    //Create Star from given values
    private void CreateStar(string name, string colour, int x, int z, int nPlanets)
    {
        //Find colour of star (Going through array of all the star colours)
        for(int i = 0; i < colours.Length; i++)
        {
            if (colour == colours[i])
            {
                var star = Instantiate(prefabs[i], new Vector3(x, 0, z), this.transform.rotation);
                //Check and re-check (up the 10 times) the position of the system and make sure no systems are within 3000
                for (int j = 0; j < 10; j++)
                {
                    bool changed = false;
                    foreach (GameObject s in generatedSystems)
                    {
                        if (Vector3.Distance(s.transform.position, star.transform.position) < 3000)
                        {
                            star.transform.position = new Vector3(Random.Range(-(secWidth / 2), (secWidth / 2)), 0, Random.Range(-(secHeight / 2), (secHeight / 2)));
                            changed = true;
                        }
                    }
                    //If star was able to be created generate the system script and add it to the lists
                    if (!changed)
                    {
                        star.GetComponent<System_Script>().SystemGen(name, colour, (int)star.transform.position.x, (int)star.transform.position.z, nPlanets, planetPrefab, this);
                        generatedSystems.Add(star);
                        systemsList.Add(star.GetComponent<System_Script>().Star);
                        break;
                    }
                    //If it cannot create the star (after 10 attempts) delete it and move onto the next star
                    if (j == 9)
                    {
                        Destroy(star);
                    }
                }
            }
        }
    }

    //Loading Star
    private void CreateStar(System_Class system, Save_Class save)
    {
        for (int i = 0; i < colours.Length; i++)
        {
            if (system.colour == colours[i])
            {
                GameObject star = Instantiate(prefabs[i], new Vector3(system.posX, 0, system.posZ), this.transform.rotation);
                star.GetComponent<System_Script>().SystemGen(system, planetPrefab, this, save);
                break;
            }
        }
    }
}
