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

    private void Start()
    {
        //CoreBiomes();
        //SaveDefaults();
        biomeFiles = finder.Retrieve("Biomes.xml", ".meta");
        biomes = FindBiomeFiles();
        biomeArray = biomes.ToArray();
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
        var file = File.Create(finder.defaultPath + "/CoreBiomes.xml");
        file.Close();
        Serializer.Serialize(biomes, finder.defaultPath + "/CoreBiomes.xml");
        //Debug.Log("File written");
    }

    //Creates the default core biomes
    public void CoreBiomes()
    {
        biomes = new List<Biome_Class>();

        CreateBiome("Alpine", -0.30f, true, true);
        CreateBiome("Arctic", -0.40f, true, true);
        CreateBiome("Asteroid", -0.50f, false, false);
        CreateBiome("Barren", -0.60f, true, false);
        CreateBiome("Continental", 0.20f, true, true);
        CreateBiome("Desert", -0.45f, true, true);
        CreateBiome("Gas Giant", -0.50f, false, false);
        CreateBiome("Glacial", -0.50f, true, true);
        CreateBiome("Mediterranean", 0.30f, true, true);
        CreateBiome("Molten", -0.60f, true, true);
        CreateBiome("Moon", -0.50f, true, false);
        CreateBiome("Oasis", -0.20f, true, true);
        CreateBiome("Ocean", -0.30f, true, true);
        CreateBiome("Swamp", -0.55f, true, true);
        CreateBiome("Toxic", -0.60f, true, false);
        CreateBiome("Tropical", 0.10f, true, false);
        CreateBiome("Tundra", -0.35f, true, false);
    }

    //Creates a biome_class in code (only used for core biomes)
    public void CreateBiome(string name, float happinessMod, bool SurfacePop, bool Atmo)
    {
        var tempBiome = new Biome_Class();
        tempBiome.biomeName = name;
        tempBiome.happinessModifier = happinessMod;
        tempBiome.SurfacePop = SurfacePop;
        tempBiome.Atmo = Atmo;
        biomes.Add(tempBiome);
    }


}
