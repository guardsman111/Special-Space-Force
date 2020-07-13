using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Galaxy_Generation_Manager : MonoBehaviour
{
    [SerializeField]
    private int sectorWidth;
    [SerializeField]
    private int sectorHeight;
    [SerializeField]
    private int nSystems;
    [SerializeField]
    private int minPlanets;
    [SerializeField, Header("Maximum 7")]
    private int maxPlanets;

    public FileFinder fileFinder;
    public System_Generator systemGenerator;
    public Biome_Manager biomeManager;

    public ToggleVisiblePlanets planetToggle;

    [SerializeField]
    private List<string> starNames;


    public int SectorWidth
    {
        get { return sectorWidth; }

        set
        {
            if
              (value != sectorWidth)
            {
                sectorWidth = value;
            }
        }
    }

    public int SectorHeight
    {
        get { return sectorHeight; }

        set
        {
            if
              (value != sectorHeight)
            {
                sectorHeight = value;
            }
        }
    }

    public int NSystems
    {
        get { return nSystems; }

        set
        {
            if
              (value != nSystems)
            {
                nSystems = value;
            }
        }
    }

    public int MinPlanets
    {
        get { return minPlanets; }

        set
        {
            if
              (value != minPlanets)
            {
                minPlanets = value;
            }
        }
    }

    public int MaxPlanets
    {
        get { return maxPlanets; }

        set
        {
            if
              (value != maxPlanets)
            {
                maxPlanets = value;
            }
        }
    }

    public List<string> StarNames
    {
        get { return starNames; }

        set
        {
            if
              (value != starNames)
            {
                starNames = value;
            }
        }
    }

    //Kicks off generation of preselected values 
    void Start()
    {
        //Start File Finder
        fileFinder.Run();

        //Setup Biomes
        biomeManager.Run();

        if (fileFinder.foundSave)
        {
            List<System_Class> loadSystems = new List<System_Class>();
            List<string> tempList = fileFinder.Retrieve("Save.xml", ".meta");

            try
            {
                foreach (string s in tempList)
                {
                    List<System_Class> temp = Serializer.Deserialize<List<System_Class>>(s);
                    foreach (System_Class tempS in temp)
                    {
                        loadSystems.Add(tempS);
                        //Debug.Log(tempB.biomeName);
                    }
                }
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            catch (FileNotFoundException FileNull)
            {
                Console.WriteLine(FileNull.Message);
            }

            systemGenerator.BeginGeneration(loadSystems);
            planetToggle.Run();
        }
        else 
        {

            systemGenerator.BeginGeneration(sectorWidth, sectorHeight, nSystems, minPlanets, maxPlanets, starNames);
            Serializer.Serialize(systemGenerator.systemsList, Application.dataPath + "/Resources/Save.xml");

            planetToggle.Run();
        }
    }
}
