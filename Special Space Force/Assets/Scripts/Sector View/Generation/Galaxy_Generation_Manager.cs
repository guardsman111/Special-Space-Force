using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Galaxy_Generation_Manager : MonoBehaviour
{
    /// <summary>
    /// This script manages the initiation of the generation sequence and the order in which elements are created
    /// </summary>
    
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
    public Manager_Script modManager;
    public Biome_Manager biomeManager;
    public Race_Manager raceManager;
    public Slot_Generator slotGenerator;
    public Fleet_Generator fleetGenerator;
    public Localisation_Manager localisationManager;
    public Voidcraft_Manager voidcraftManager;
    public Music_Sound_Script musicPlayer;

    public Save_Class save;

    [SerializeField]
    private List<string> starNames;

    private bool done;

    //Getters and Setters
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
        //Setup Biomes
        biomeManager.Run();

        //If loading, retrieve and go through loading system
        if (loading)
        {
            List<System_Class> loadSystems = new List<System_Class>();

            try
            {
                save = Serializer.Deserialize<Save_Class>(Scene_Manager.saveString);
                foreach (System_Class tempS in save.systems)
                {
                    loadSystems.Add(tempS);
                    //Debug.Log(tempB.biomeName);
                }
                Scene_Manager.saveString = null;
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

            systemGenerator.BeginGeneration(loadSystems, save);
            product = save.generatedProduct;
            modManager.GeneratedProduct = product;
            modManager.rankManager.Begin();
            modManager.traitManager.Run();
            modManager.equipmentManager.playerDefaultColours = product.playerColours;
            modManager.voidcraftManager.playerFleetColours = product.playerFleetColours;
            localisationManager.LoadStringListClass(product.chosenLocalisationList[0], "TrooperNames");
            localisationManager.LoadStringListClass(product.chosenLocalisationList[1], "HierachyNames");
            localisationManager.LoadStringListClass(product.chosenLocalisationList[2], "SlotNames");
            localisationManager.SeperateStringLists();
            slotGenerator.LoadSlots(save.topSlots);
            fleetGenerator.LoadFleets(save.fleets);
            modManager.forceManager.Load(product);
            SetCameraLimits(-save.height / 2, save.height / 2, -save.width / 2, save.width / 2);
            modManager.factionManager.Load(systemGenerator.GeneratedSystems, product.factions);
            modManager.turnManager.FirstTurn(product, save, true);
            musicPlayer.StartPlaying();;
        }
        else //Else generate everything from scratch using user inputed creation values
        {
            StarNames = localisationManager.FindStarNames();

            systemGenerator.BeginGeneration(product, starNames);
            modManager.rankManager.Begin();

            save = new Save_Class();
            save.saveName = product.regimentName;
            save.height = product.height;
            save.width = product.width;
            save.systems = systemGenerator.SystemsList;
            save.generatedProduct = product;
            modManager.GeneratedProduct = product;
            save.topSlots = slotGenerator.FindDefaultSlots();
            save.fleets = fleetGenerator.FindDefaultFleets();
            Serializer.Serialize(save, Application.dataPath + "/Resources/Saves/" + save.saveName + ".Save.xml");
            SetCameraLimits(-save.height / 2, save.height / 2, -save.width / 2, save.width / 2);
            modManager.turnManager.FirstTurn(product, save, false);
            musicPlayer.StartPlaying();
        }

    }

    //Set Camera limits
    public void SetCameraLimits(int minX, int maxX, int minZ, int maxZ)
    {
        cameraTrolley.cameraMinX = minX;
        cameraTrolley.cameraMaxX = maxX;
        cameraTrolley.cameraMinZ = minZ;
        cameraTrolley.cameraMaxZ = maxZ;
    }
}
