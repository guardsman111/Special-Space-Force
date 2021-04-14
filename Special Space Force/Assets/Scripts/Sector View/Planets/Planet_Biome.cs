using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Biome
{
    /// <summary>
    /// This script finds the biome and brings its stats to the front. This script, like the Player_Script, might be outdated.
    /// </summary>
    
    public string biomeName;
    float happinessModifier;
    float foodModifier;

    public bool surfacePop = false;
    public bool atmo = false;

    public Biome_Manager biomeManager;
    public Material biomeMaterial;

    public Texture2D biomeImage;

    //Linked to the planets mesh renderer so we can change the material, allowing for players to create their own.
    public MeshRenderer planetSkin;

    //Finds the biome manager, attatches it and grabs the list of biomes
    public Planet_Biome(string biome, MeshRenderer PlanetSkin)
    {
        biomeManager = GameObject.FindGameObjectWithTag("BiomeManager").GetComponent<Biome_Manager>();
        planetSkin = PlanetSkin;
        RunCheck(biome);
    }

    //Calculates the Biome from the given string
    private void RunCheck(string biome)
    {
        //if biome is not set, generate random
        if (biome != "")
        {
            //if biomename matches the biome class's name, use its stats.
            foreach (Biome_Class b in biomeManager.Biomes)
            {
                if (b.biomeName == biome)
                {
                    biomeName = b.biomeName;
                    happinessModifier = b.happinessModifier;
                    foodModifier = b.foodModifier;
                    surfacePop = b.SurfacePop;
                    atmo = b.Atmo;
                }
            }
        }
        else
        {
            int rand = Random.Range(0, biomeManager.Biomes.Count);
            biomeName = biomeManager.Biomes[rand].biomeName;
            happinessModifier = biomeManager.Biomes[rand].happinessModifier;
            foodModifier = biomeManager.Biomes[rand].foodModifier;
            surfacePop = biomeManager.Biomes[rand].SurfacePop;
            atmo = biomeManager.Biomes[rand].Atmo;
        }

        //change material to the correct biome
        foreach(Material m in biomeManager.BiomeMats)
        {
            if (m.name == biomeName)
            {
                planetSkin.material = m;
                break;
            }
        }
    }

    public Texture2D GetImageForBiome()
    {
        Texture2D returner = null;

        //if biomename matches the biome class's name, use its stats.
        foreach (Biome_Script b in biomeManager.BiomesS)
        {
            if (b.biomeName == biomeName)
            {
                returner = b.biomeImage;
            }
        }

        return returner;
    }

    public float GetHappinessModifier() { return (happinessModifier); }
}
