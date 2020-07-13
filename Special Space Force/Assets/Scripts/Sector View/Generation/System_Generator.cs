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

    public string[] colours;
    [SerializeField]
    public List<System_Class> systemsList;
    [SerializeField]
    public GameObject[] prefabs;
    public GameObject planetPrefab;

    public void BeginGeneration(int width, int height, int nSystems, int minP, int maxP, List<string> sNames)
    {
        systemsList = new List<System_Class>();
        for(int i = 0; i < nSystems; i++)
        {
            int posX = Random.Range(-(width / 2), (width / 2));
            int posZ = Random.Range(-(height / 2), (height / 2));
            string rColour = colours[Random.Range(0, colours.Length)];
            int randP = Random.Range(minP, maxP);
            int randN = Random.Range(0, sNames.Count);

            CreateStar(sNames[randN], rColour, posX, posZ, randP);
        }
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
                star.GetComponent<System_Script>().SystemGen(name, colour, x, z, nPlanets, planetPrefab, this);
                systemsList.Add(star.GetComponent<System_Script>().Star);
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
