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
    [SerializeField]
    private int avgPlanetSize;

    public Camera_Movement cameraTrolley;

    public FileFinder fileFinder;
    public System_Generator systemGenerator;
    public Biome_Manager biomeManager;
    public Race_Manager raceManager;

    public ToggleVisiblePlanets planetToggle;

    public Save_Class save;

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
    public void Generate(bool loading, Generation_Class product)
    {
        //Start File Finder
        fileFinder.Run();

        //Setup Biomes
        biomeManager.Run();

        //Setup Biomes
        raceManager.Run();

        if (loading)
        {
            List<System_Class> loadSystems = new List<System_Class>();
            List<string> tempList = fileFinder.Retrieve("NewSave.xml", ".meta");

            try
            {
                foreach (string s in tempList)
                {
                    save = Serializer.Deserialize<Save_Class>(s);
                    foreach (System_Class tempS in save.systems)
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
            SetCameraLimits(-save.height / 2, save.height / 2, -save.width / 2, save.width / 2);

        }
        else 
        {

            systemGenerator.BeginGeneration(product, starNames);

            save = new Save_Class();
            save.saveName = "NewSave";
            save.height = sectorHeight;
            save.width = sectorWidth;
            save.systems = systemGenerator.systemsList;
            SetCameraLimits(-save.height / 2, save.height / 2, -save.width / 2, save.width / 2);
            Serializer.Serialize(save, Application.dataPath + "/Resources/" + save.saveName + ".xml");

        }
        planetToggle.Run();
    }

    public void SetCameraLimits(int minX, int maxX, int minZ, int maxZ)
    {
        cameraTrolley.cameraMinX = minX;
        cameraTrolley.cameraMaxX = maxX;
        cameraTrolley.cameraMinZ = minZ;
        cameraTrolley.cameraMaxZ = maxZ;
    }
}
