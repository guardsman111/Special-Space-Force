using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Biome
{
    public string biomeName;
    float happinessModifier;

    bool surfacePop = false;
    public bool atmo = false;

    public Biome_Manager Biomes;
    private List<Biome_Class> biomes;

    //Finds the biome manager, attatches it and grabs the list of biomes
    public Planet_Biome(string biome)
    {
        Biomes = GameObject.FindGameObjectWithTag("BiomeManager").GetComponent<Biome_Manager>();
        biomes = Biomes.Biomes;
        RunCheck(biome);
    }

    //Calculates the Biome from the given string
    private void RunCheck(string biome)
    {
        foreach(Biome_Class b in biomes)
        {
            if (b.biomeName == biome)
            {
                biomeName = b.biomeName;
                happinessModifier = b.happinessModifier;
                surfacePop = b.SurfacePop;
                atmo = b.Atmo;
            }
        }
        if(biomeName == "" || biomeName == null)
        {
            int rand = Random.Range(0, biomes.Count);
            biomeName = biomes[rand].biomeName;
            happinessModifier = biomes[rand].happinessModifier;
            surfacePop = biomes[rand].SurfacePop;
            atmo = biomes[rand].Atmo;
        }
    }

    public float GetHappinessModifier() { return (happinessModifier); }
}
