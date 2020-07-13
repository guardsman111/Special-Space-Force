using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Biome_Manager : MonoBehaviour
{
    public FileFinder finder;
    private List<string> biomeFiles;
    private List<Biome_Class> biomes;
    [SerializeField]
    private List<Material> biomeMats;

    public Biome_Class[] biomeArray = new Biome_Class[] { };

    public bool Run()
    {
        bool done = false;
        //CoreBiomes();
        //SaveDefaults();
        biomeFiles = finder.Retrieve("Biomes.xml", ".meta");
        biomes = FindBiomeFiles();
        biomeArray = biomes.ToArray();
        CreateMaterials();
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
                foreach (Biome_Class tempB in temp)
                {
                    biomeList.Add(tempB);
                    //Debug.Log(tempB.biomeName);
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
        var file = File.Create(finder.defaultPath + "/Core/Biomes/CoreBiomes.xml");
        file.Close();
        Serializer.Serialize(biomes, finder.defaultPath + "/Core/Biomes/CoreBiomes.xml");
        //Debug.Log("File written");
    }

    //Creates the default core biomes
    public void CoreBiomes()
    {
        biomes = new List<Biome_Class>();

        CreateBiome("Alpine", -0.30f, true, true);
        CreateBiome("Arctic", -0.40f, true, true);
        CreateBiome("Arid", -0.20f, true, true);
        CreateBiome("Asteroid", -0.50f, false, false);
        CreateBiome("Barren", -0.60f, true, false);
        CreateBiome("Continental", 0.20f, true, true);
        CreateBiome("Desert", -0.45f, true, true);
        CreateBiome("Gas Giant", -0.50f, false, false);
        CreateBiome("Glacial", -0.50f, true, true);
        CreateBiome("Mediterranean", 0.30f, true, true);
        CreateBiome("Molten", -0.60f, true, true);
        CreateBiome("Moon", -0.50f, true, false);
        CreateBiome("Oasis", -0.10f, true, true);
        CreateBiome("Ocean", -0.20f, true, true);
        CreateBiome("Swamp", -0.55f, true, true);
        CreateBiome("Toxic", -0.60f, true, false);
        CreateBiome("Tropical", 0.10f, true, false);
        CreateBiome("Tundra", -0.25f, true, false);
    }

    //Creates a biome_class in code (only used for core biomes)
    public void CreateBiome(string name, float happinessMod, bool SurfacePop, bool Atmo)
    {
        var tempBiome = new Biome_Class();
        tempBiome.biomeName = name;
        tempBiome.happinessModifier = happinessMod;
        tempBiome.SurfacePop = SurfacePop;
        tempBiome.Atmo = Atmo;
        tempBiome.baseBiome = name;
        tempBiome.materialTexture = "Core";
        tempBiome.materialNormal = "Core";
        tempBiome.materialMask = "Core";
        biomes.Add(tempBiome);
    }

    public void CreateMaterials()
    {
        foreach(Biome_Class bc in biomeArray)
        {
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
                var bytes = System.IO.File.ReadAllBytes(bc.materialTexture);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(bytes);
                temp.SetTexture("_Albedo", tex);

                if (bc.materialNormal != "Core")
                {
                    bytes = System.IO.File.ReadAllBytes(bc.materialNormal);
                    Texture2D norm = new Texture2D(1, 1);
                    norm.LoadImage(bytes);
                    temp.SetTexture("_Normalmap", norm);
                }

                if (bc.materialMask != "Core")
                {
                    bytes = System.IO.File.ReadAllBytes(bc.materialMask);
                    Texture2D mask = new Texture2D(1, 1);
                    mask.LoadImage(bytes);
                    temp.SetTexture("_watermask", mask);
                }
                biomeMats.Add(temp);
            }
        }
    }

    public int CheckCount()
    {
        int count = biomes.Count();
        return count;
    }
}
