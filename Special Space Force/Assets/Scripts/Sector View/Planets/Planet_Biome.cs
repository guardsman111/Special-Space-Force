using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Biome
{
    public string biomeName;
    float happinessModifier;

    bool surfacePop = false;
    public bool atmo = false;

    public Biome_Manager biomeManager;
    private List<Biome_Class> biomes;
    private List<Material> biomeMats;

    //Linked to the planets mesh renderer so we can change the material, allowing for players to create their own.
    public MeshRenderer planetSkin;

    //Finds the biome manager, attatches it and grabs the list of biomes
    public Planet_Biome(string biome, MeshRenderer PlanetSkin)
    {
        biomeManager = GameObject.FindGameObjectWithTag("BiomeManager").GetComponent<Biome_Manager>();
        biomes = biomeManager.Biomes;
        biomeMats = biomeManager.BiomeMats;
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
            foreach (Biome_Class b in biomes)
            {
                if (b.biomeName == biome)
                {
                    biomeName = b.biomeName;
                    happinessModifier = b.happinessModifier;
                    surfacePop = b.SurfacePop;
                    atmo = b.Atmo;
                }
            }
        }
        else
        {
            int rand = Random.Range(0, biomes.Count);
            biomeName = biomes[rand].biomeName;
            happinessModifier = biomes[rand].happinessModifier;
            surfacePop = biomes[rand].SurfacePop;
            atmo = biomes[rand].Atmo;
        }

        //change material to the correct biome
        foreach(Material m in biomeMats)
        {
            if(m.name == biomeName)
            {
                planetSkin.material = m;
                break;
            }
        }
    }

    public float GetHappinessModifier() { return (happinessModifier); }
}
