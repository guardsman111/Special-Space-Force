using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Generator : MonoBehaviour
{
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

    public string[] colours;
    [SerializeField]
    public List<System_Class> systemsList;
    [SerializeField]
    public List<GameObject> generatedSystems;
    [SerializeField]
    public GameObject[] prefabs;
    public GameObject planetPrefab;

    public void BeginGeneration(int width, int height, int nSystems, int minP, int maxP, List<string> sNames)
    {
        systemsList = new List<System_Class>();
        generatedSystems = new List<GameObject>();
        for(int i = 0; i < nSystems; i++)
        {
            int posX = Random.Range(-(width / 2), (width / 2));
            int posZ = Random.Range(-(height / 2), (height / 2));
            string rColour = colours[Random.Range(0, colours.Length)];
            int randP = Random.Range(minP, maxP + 1);
            int randN = Random.Range(0, sNames.Count);

            CreateStar(sNames[randN], rColour, posX, posZ, randP);
        }
        secHeight = height;
        secWidth = width;
    }

    public void BeginGeneration(List<System_Class> loadSystems)
    {
        systemsList = loadSystems;
        foreach(System_Class s in systemsList)
        {
            CreateStar(s);
        }
    }

    private void CreateStar(string name, string colour, int x, int z, int nPlanets)
    {
        for(int i = 0; i < colours.Length; i++)
        {
            if (colour == colours[i])
            {
                var star = Instantiate(prefabs[i], new Vector3(x, 0, z), this.transform.rotation);
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
                    if (!changed)
                    {
                        star.GetComponent<System_Script>().SystemGen(name, colour, (int)star.transform.position.x, (int)star.transform.position.z, nPlanets, planetPrefab, this);
                        generatedSystems.Add(star);
                        systemsList.Add(star.GetComponent<System_Script>().Star);
                        break;
                    }
                    if (j == 9)
                    {
                        Destroy(star);
                    }
                }
            }
        }
    }

    private void CreateStar(System_Class system)
    {
        for (int i = 0; i < colours.Length; i++)
        {
            if (system.colour == colours[i])
            {
                GameObject star = Instantiate(prefabs[i], new Vector3(system.posX, 0, system.posZ), this.transform.rotation);
                star.GetComponent<System_Script>().SystemGen(system, planetPrefab, this);
            }
        }
    }
}
