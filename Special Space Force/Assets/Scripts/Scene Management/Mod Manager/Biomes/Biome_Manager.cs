﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Biome_Manager : MonoBehaviour
{
    /// <summary>
    /// The Biome manager retrieves (and can save core) biomes and makes them easily available to the game
    /// </summary>
    public FileFinder finder;
    private List<string> biomeFiles;
    private List<Biome_Class> biomes;
    private List<Biome_Script> biomesS;
    [SerializeField]
    private List<Material> biomeMats;

    public List<Texture2D> biomeImages;
    
    //Finds files and saves core biomes. Ignores .meta files
    public bool Run()
    {
        bool done = false;
        //CoreBiomes();
        //SaveDefaults();
        biomeFiles = finder.Retrieve("Biomes.xml", ".meta");
        biomes = FindBiomeFiles();
        CreateMaterials();
        SetupScripts();
        return done;
    }

    public List<Biome_Class> Biomes
    {
        get { return biomes; }

        set { if
                (value != biomes)
                {
                    biomes = value;
                }
            }
    }

    public List<Biome_Script> BiomesS
    {
        get { return biomesS; }

        set
        {
            if
              (value != biomesS)
            {
                biomesS = value;
            }
        }
    }

    public List<Material> BiomeMats
    {
        get { return biomeMats; }

        set
        {
            if
              (value != biomeMats)
            {
                biomeMats = value;
            }
        }
    }

    //Send the Files over to the serializer to be converted into Biome_Classes, for use in game
    private List<Biome_Class> FindBiomeFiles()
    {
        List<Biome_Class> biomeList = new List<Biome_Class>();

        try
        {

            foreach (string s in biomeFiles)
            {
                List<Biome_Class> temp = Serializer.Deserialize<List<Biome_Class>>(s);
                //For each Biome_Class, check for duplicates. Delete non-core biome duplicates, and overwrite core biome with duplicates.
                foreach (Biome_Class tempB in temp)
                {
                    int counter = 0;
                    bool duplicate = false;
                    foreach (Biome_Class checkB in biomeList) {
                        if (tempB.biomeName == checkB.biomeName)
                        {
                            duplicate = true;
                            if(checkB.source == "Core")
                            {
                                //Debug.Log(counter);
                                biomeList[counter] = tempB;
                                Debug.Log(tempB.biomeName + " Replaced Vanilla");
                                BiomeMats.RemoveAt(counter);
                            }
                            else
                            {
                                biomeList.RemoveAt(counter);
                                //Debug.Log("Duplicates Removed");
                            }
                            break;
                        }
                        counter += 1;
                    }
                    if (!duplicate)
                    {

                        biomeList.Add(tempB);
                        //Debug.Log(tempB.biomeName);
                    }
                }
            }

            return biomeList;
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
        return null;
    }

    //Saves the default Biomes
    public void SaveDefaults()
    {
        var file = File.Create(finder.defaultPath + "/Biomes/CoreBiomes.xml");
        file.Close();
        Serializer.Serialize(biomes, finder.defaultPath + "/Biomes/CoreBiomes.xml");
        //Debug.Log("File written");
    }

    //Creates the default core biomes
    public void CoreBiomes()
    {
        biomes = new List<Biome_Class>();

        CreateBiome("Alpine", "Core", -0.30f, -0.20f, 0.2f, 0.7f, true, true);
        CreateBiome("Arctic", "Core", -0.40f, -0.50f, 0.4f, 0.9f, true, true);
        CreateBiome("Arid", "Core", -0.20f, -0.20f, 0.4f, 0.9f, true, true);
        CreateBiome("Asteroid", "Core", -0.50f, -1.0f, 0.2f, 0.5f, false, false);
        CreateBiome("Barren", "Core", -0.60f, -1.0f, 0.6f, 1.0f, true, false);
        CreateBiome("Continental", "Core", 0.20f, 0.20f, 0.3f, 0.8f, true, true);
        CreateBiome("Desert", "Core", -0.45f, -0.50f, 0.4f, 0.9f, true, true);
        CreateBiome("Gas Giant Helium", "Core", -0.50f, -1.0f, 0.0f, 0.0f, false, false);
        CreateBiome("Gas Giant Hydrogen", "Core", -0.50f, -1.0f, 0.0f, 0.0f, false, false);
        CreateBiome("Gas Giant Methane", "Core", -0.50f, -1.0f, 0.0f, 0.0f, false, false);
        CreateBiome("Glacial", "Core", -0.50f, -0.60f, 0.1f, 0.3f, true, true);
        CreateBiome("Mediterranean", "Core", 0.30f, 0.10f, 0.2f, 0.7f, true, true);
        CreateBiome("Molten", "Core", -0.60f, -1.0f, 0.2f, 0.5f, true, false);
        CreateBiome("Moon", "Core", -0.50f, -1.0f, 0.3f, 0.9f, false, false);
        CreateBiome("Oasis", "Core", -0.10f, -0.30f, 0.2f, 0.7f, true, true);
        CreateBiome("Ocean", "Core", -0.20f, 0.20f, 0.1f, 0.4f, true, true);
        CreateBiome("Swamp", "Core", -0.55f, 0.10f, 0.1f, 0.4f, true, true);
        CreateBiome("Toxic (Phosphor)", "Core", -0.60f, -1.0f, 0.2f, 0.7f, true, false);
        CreateBiome("Toxic (Sulphur)", "Core", -0.60f, -1.0f, 0.2f, 0.7f, true, false);
        CreateBiome("Tropical", "Core", 0.10f, 0.20f, 0.2f, 0.7f, true, true);
        CreateBiome("Tundra", "Core", -0.25f, -0.20f, 0.4f, 0.9f, true, true);
    }

    //Creates a biome_class in code (only used for core biomes)
    public void CreateBiome(string name, string sourceFile, float happinessMod, float foodMod, float minUS, float maxUS, bool SurfacePop, bool Atmo)
    {
        var tempBiome = new Biome_Class();
        tempBiome.biomeName = name;
        tempBiome.source = sourceFile;
        tempBiome.happinessModifier = happinessMod;
        tempBiome.foodModifier = foodMod;
        tempBiome.minSpace = minUS;
        tempBiome.maxSpace = maxUS;
        tempBiome.SurfacePop = SurfacePop;
        tempBiome.Atmo = Atmo;
        tempBiome.baseBiome = name;
        tempBiome.materialTexture = "Core";
        tempBiome.materialNormal = "Core";
        tempBiome.materialMask = "Core";
        biomes.Add(tempBiome);
    }

    //Creates the materials for any new biomes the player adds using the file names given in the new biome xml
    public void CreateMaterials()
    {
        foreach(Biome_Class bc in biomes)
        {
            //If not core biome, create new material. Core materials do not need to be created
            if(bc.materialTexture != "Core")
            {
                Material temp = null;
                foreach (Material m in biomeMats)
                {
                    if(m.name == bc.baseBiome)
                    {
                        temp = new Material(m);
                    }
                }
                temp.name = bc.biomeName;
                var bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + bc.materialTexture);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(bytes);
                temp.SetTexture("_Albedo", tex);

                if (bc.materialNormal != "Core")
                {
                    bytes = System.IO.File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + bc.materialNormal);
                    Texture2D norm = new Texture2D(1, 1);
                    norm.LoadImage(bytes);
                    temp.SetTexture("_Normalmap", norm);
                }

                if (bc.materialMask != "Core")
                {
                    bytes = System.IO.File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + bc.materialMask);
                    Texture2D mask = new Texture2D(1, 1);
                    mask.LoadImage(bytes);
                    temp.SetTexture("_watermask", mask);
                }
                biomeMats.Add(temp);
            }
        }
    }

    //Check number of Biomes
    public int CheckCount()
    {
        int count = biomes.Count();
        return count;
    }

    public void SetupScripts()
    {
        List<Biome_Script> biomeSList = new List<Biome_Script>();

        for (int i = 0; i < biomes.Count; i++)
        {
            Biome_Script tempBS = new Biome_Script();

            tempBS.biomeName = biomes[i].biomeName;
            tempBS.biomeC = biomes[i];
            tempBS.biomeMaterial = biomeMats[i];
            if (i >= biomeImages.Count)
            {
                try
                {
                    var bytes = System.IO.File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + biomes[i].imagePath);
                    Texture2D image = new Texture2D(1, 1);
                    image.LoadImage(bytes);
                    tempBS.biomeImage = image;
                }
                catch
                {
                    Debug.LogError("Couldn't Load image for " + tempBS.biomeName);
                }
            }
            else
            {
                tempBS.biomeImage = biomeImages[i];
            }
            biomeSList.Add(tempBS);
        }

        biomesS = biomeSList;
    }
}
